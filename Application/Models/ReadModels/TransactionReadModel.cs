namespace BankMore.Application.Models.ReadModels
{

	public class TransactionReadModel
	{
		public Guid Id { get; set; }

		public Guid ContaId { get; set; }

		public string TipoTransacao { get; set; } = string.Empty;

		public decimal Valor { get; set; }

		public decimal SaldoAnterior { get; set; }

		public decimal SaldoAtual { get; set; }

		public string Descricao { get; set; } = string.Empty;

		public DateTime DataTransacao { get; set; }
	}

}
