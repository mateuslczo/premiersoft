using MediatR;

namespace BankMore.Application.Commands
{
	public class CreateAccountCommand :IRequest<Guid>
	{
		public Guid IdContaCorrente { get; set; }
		public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Numero { get; set; }
		public string Senha { get; set; }
		public bool Ativo { get; set; } = false;
		public string Salt { get; set; } = string.Empty;
	}
}