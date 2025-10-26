using System.ComponentModel.DataAnnotations;

namespace BankMore.Application.Models.ReadModels
{

	public class AccountReadModel
	{

		public Guid? IdContaCorrente { get; set; }

		public int Numero { get; set; }

		public string? Nome { get; set; }

		public bool Ativo { get; set; }
	}
}