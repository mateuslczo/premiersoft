using BankMore.Application.Commands;
using BankMore.Application.Exceptions;
using BankMore.Application.Models.Responses;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using BankMore.Domain.Interfaces.IServices;
using BankMore.Domain.ValueObjects;
using MediatR;

namespace BankMore.Application.Handlers
{
	public class LoginCommandHandler :IRequestHandler<LoginCommand, LoginResponse>
	{
		private readonly IAccountReadRepository _repository;
		private readonly ITokenService _tokenService;
		private readonly ILogger<LoginCommandHandler> _logger;

		public LoginCommandHandler(
			IAccountReadRepository repository,
			ITokenService tokenService,
			ILogger<LoginCommandHandler> logger)
		{
			_repository = repository;
			_tokenService = tokenService;
			_logger = logger;
		}

		public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Tentando login para CPF: {Cpf}", request.Cpf);

			// Valida CPF
			var cpfResult = CpfValidator.Validate(request.Cpf);
			if (cpfResult.IsFailure)
				throw new CustomExceptions("INVALID_DOCUMENT", cpfResult.Error);

			
			// Gera token JWT
			var token = _tokenService.GenerateToken(request.Cpf, request.Senha, "User");

			return new LoginResponse
			{
				Token = token,
				ExpiraEm = DateTime.UtcNow.AddHours(2)
			};
		}
	}
}