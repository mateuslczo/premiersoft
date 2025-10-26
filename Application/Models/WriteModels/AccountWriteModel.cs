namespace BankMore.Application.Models.WriteModels
{

	public class AccountWriteModel
	{

		public Guid? IdContaCorrente { get; set; }

		public int Numero { get; set; }

		public string? Nome { get; set; }

		public bool Ativo { get; set; } = false;

		public string? Senha { get; set; }

		public string? Salt { get; set; }
	}
}