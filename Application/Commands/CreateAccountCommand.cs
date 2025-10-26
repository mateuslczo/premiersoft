using MediatR;

namespace BankMore.Application.Commands
{
	public class CreateAccountCommand :IRequest<Guid>
	{
		public string Nome { get; set; }
		public int Numero { get; set; }
		public string? Senha { get; set; }
		public bool Ativo { get; set; } = false;
	}
}