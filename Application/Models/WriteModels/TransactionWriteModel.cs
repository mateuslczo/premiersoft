namespace BankMore.Application.Models.WriteModels
{

	/// <summary>
	/// Classe de modelo de entidade (tabela ContaCorrente)
	/// </summary>
	public class TransactionWriteModel
	{
		public int IdMovimento { get; set; }

		public int IdContaCorrente { get; set; }

		public string TipoMovimento { get; set; } = string.Empty;

		public decimal Valor { get; set; }

		public decimal SaldoAnterior { get; set; }

		public decimal SaldoAtual { get; set; }

		public string Descricao { get; set; } = string.Empty;

		public string DataMovimento { get; set; }
	}

}
