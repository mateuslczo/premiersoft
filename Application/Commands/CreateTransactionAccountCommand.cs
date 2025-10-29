using MediatR;

namespace BankMore.Application.Commands
{
	public class CreateTransactionAccountCommand :IRequest<int>
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