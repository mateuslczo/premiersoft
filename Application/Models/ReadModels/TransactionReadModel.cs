namespace BankMore.Application.Models.ReadModels
{

	/// <summary>
	/// Classe de modelo de entidade (Tabela de Movimento)
	/// </summary>
	public class TransactionReadModel
	{
		public int IdMovimento { get; set; }

		public int IdContaCorrente { get; set; }

		public string TipoMovimento { get; set; } = string.Empty;

		public decimal Valor { get; set; }

		public decimal SaldoAnterior { get; set; }

		public decimal SaldoAtual { get; set; }

		public string Descricao { get; set; } = string.Empty;

		public DateTime DataMovimento { get; set; }
	}

}
