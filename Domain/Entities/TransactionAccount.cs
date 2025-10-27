using System.Security.Cryptography;

namespace BankMore.Domain.Entities
{

	/// <summary>
	/// Classe de entidade (tabela ContaCorrenteMovimento)
	/// </summary>
	public class TransactionAccount
	{

		public Guid IdMovimento { get; private set; }
		public Guid IdContaCorrente { get; private set; }
		public string TipoMovimento { get; set; } = string.Empty;
		public string Descricao { get; set; } = string.Empty;
		public decimal Valor { get; set; } = 0M;
		public decimal SaldoAnterior { get; set; } = 0M;
		public decimal SaldoAtual { get; set; } = 0M;
		public DateTime DataMovimento { get; set; }

	}
}
