using MediatR;

namespace BankMore.Application.Commands
{
	public class DeactivateAccountCommand :IRequest<int>
	{
        public int IdContaCorrente { get; set; }
        public string Cpf { get; set; }
		public int Numero { get; set; }
		public string Senha { get; set; }
		public bool Ativo { get; set; } = false;

	}
}