using System.Security.Cryptography;

namespace BankMore.Domain.ValueObjects
{
	public sealed class PasswordValidator
	{
		public static string Senha { get; private set; } 
		public static string Salt { get; private set; }

		private static void GenerateHashPassword(string senha)
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

		public static bool ValidatePassword(string _senha, string _salt)
		{
			var saltBytes = Convert.FromBase64String(_salt);

			using var pbkdf2 = new Rfc2898DeriveBytes(
				password: _senha,
				salt: saltBytes,
				iterations: 100_000,
				hashAlgorithm: HashAlgorithmName.SHA256);

			var hashBytes = pbkdf2.GetBytes(32);
			var senhaVerificada = Convert.ToBase64String(hashBytes);

			GenerateHashPassword(_senha);

			return senhaVerificada == Senha;
		}
	}
}