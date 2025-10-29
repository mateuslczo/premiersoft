namespace BankMore.Application.Models.WriteModels
{

	/// <summary>
	/// Classe de modelo de entidade (tabela ContaCorrente)
	/// </summary>
	public class AccountWriteModel
	{

		public int? IdContaCorrente { get; set; }

		public int Numero { get; set; }

		public string? Nome { get; set; }

		public bool Ativo { get; set; } = false;

		public string Senha { get; set; }

		public string? Salt { get; set; }

		public decimal Saldo { get; set; } = 0M;
	}
}