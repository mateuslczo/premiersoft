using System.ComponentModel.DataAnnotations;

namespace BankMore.Application.Models.ReadModels
{

	/// <summary>
	/// Classe de modelo de entidade (tabela ContaCorrente)
	/// </summary>
	public class AccountReadModel
	{

		public Guid? IdContaCorrente { get; set; }

		public int Numero { get; set; }

		public string? Nome { get; set; }

		public bool Ativo { get; set; }

		public decimal Saldo { get; set; }
	}
}