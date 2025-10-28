using BankMore.Application.Models.Responses;
using MediatR;

namespace BankMore.Application.Commands
{
	public class LoginCommand :IRequest<LoginResponse>
	{
		public string Cpf { get; set; } = string.Empty;
		public string Senha { get; set; } = string.Empty;
	}
}