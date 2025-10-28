using BankMore.Application.Commands;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using BankMore.Domain.ValueObjects;
using MediatR;

namespace BankMore.Application.Handlers
{

	public class DeactivateAccountCommandHandler :IRequestHandler<DeactivateAccountCommand, Guid>
	{
		private readonly IAccountWriteRepository _writeRepository;
		private readonly IAccountReadRepository _readRepository;
		private readonly ILogger<DeactivateAccountCommandHandler> _logger;

		public DeactivateAccountCommandHandler(
			IAccountWriteRepository writeRepository,
			IAccountReadRepository readRepository,
			ILogger<DeactivateAccountCommandHandler> logger)
		{
			_writeRepository = writeRepository;
			_readRepository = readRepository;
			_logger = logger;
		}

		public async Task<Guid> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Iniciando desativação da conta para CPF: {Cpf} e Número: {Numero}",
				request.Cpf, request.Numero);

			var account = await _readRepository.GetAccountByIdAsync(request.IdContaCorrente);
			if (account == null)
			{
				throw new KeyNotFoundException($"Conta não encontrada para CPF {request.Cpf} e número {request.Numero}");
			}

			if (!PasswordValidator.ValidatePassword(request.Senha, account.Salt))
			{
				throw new UnauthorizedAccessException("Senha incorreta");
			}

			if (!account.Ativo)
			{
				throw new InvalidOperationException($"A conta {account.Numero} já está desativada");
			}

			await _writeRepository.DeactivateAccountAsync(account.IdContaCorrente);

			_logger.LogInformation("Conta {AccountId} desativada com sucesso", account.IdContaCorrente);

			return request.IdContaCorrente;
		}
	}
}