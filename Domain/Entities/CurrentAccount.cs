using System.Security.Cryptography;

namespace BankMore.Domain.Entities
{

	/// <summary>
	/// Classe de entidade (tabela ContaCorrente)
	/// </summary>
	public class CurrentAccount
	{
		public Guid IdContaCorrente { get; private set; }
		public int Numero { get; private set; }
		public string Nome { get; private set; } = string.Empty;
		public bool Ativo { get; private set; }
		public string Senha { get; private set; } = string.Empty;
		public string Salt { get; private set; } = string.Empty;
		public decimal Saldo { get; set; } = 0M;

		protected CurrentAccount() { }

		public CurrentAccount(string nome, string senha)
		{
			IdContaCorrente = Guid.NewGuid();
			Nome = nome;
			Ativo = true;

			GenerateHashPassword(senha);
		}

		private long GeneratedAccountNumber()
		{
			var guidBytes = Guid.NewGuid().ToByteArray();
			var valor = BitConverter.ToInt64(guidBytes, 0);

			valor = Math.Abs(valor);

			// Limita o número a 10 dígitos 
			long accountNumber = valor % 1_000_000_0000; 

			// Garante que não comece com 0 
			if (accountNumber < 1_000_000_000)
				accountNumber += 1_000_000_000;

			return accountNumber;
		}

		private void GenerateHashPassword(string senha)
		{
			// Gera um salt
			var saltBytes = RandomNumberGenerator.GetBytes(16);
			Salt = Convert.ToBase64String(saltBytes);

			// Combina a senha + salt e gera o hash (PBKDF2)
			using var pbkdf2 = new Rfc2898DeriveBytes(
				password: senha,
				salt: saltBytes,
				iterations: 100_000,
				hashAlgorithm: HashAlgorithmName.SHA256);

			var hashBytes = pbkdf2.GetBytes(32);
			Senha = Convert.ToBase64String(hashBytes);
		}

		public bool ValidatePassword(string senhaInformada)
		{
			var saltBytes = Convert.FromBase64String(Salt);

			using var pbkdf2 = new Rfc2898DeriveBytes(
				password: senhaInformada,
				salt: saltBytes,
				iterations: 100_000,
				hashAlgorithm: HashAlgorithmName.SHA256);

			var hashBytes = pbkdf2.GetBytes(32);
			var senhaVerificada = Convert.ToBase64String(hashBytes);

			return senhaVerificada == Senha;
		}
	}
}
