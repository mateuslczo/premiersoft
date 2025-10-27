using BankMore.Application.Models.Infrastructure.Repositories.ReadRepository;
using BankMore.Application.Models.ReadModels;
using BankMore.Application.Queries;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using MediatR;

namespace BankMore.Application.Handlers
{

    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountReadModel?>
    {
        private readonly IAccountReadRepository _repository;
        private readonly ILogger<GetAccountByIdQueryHandler> _logger;

        public GetAccountByIdQueryHandler(
			IAccountReadRepository repository,
            ILogger<GetAccountByIdQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<AccountReadModel?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Buscando conta por ID: {AccountId}", request.IdContaCorrente);

                var account = await _repository.GetAccountByIdAsync(request.IdContaCorrente);

                if (account == null)
                {
                    _logger.LogWarning("Conta não encontrada para o ID: {AccountId}", request.IdContaCorrente);
                    return null;
                }

                _logger.LogInformation("Conta encontrada: {AccountNumber} - {AccountName}",
                    account.Numero, account.Nome);

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar conta por ID: {AccountId}", request.IdContaCorrente);
                throw;
            }
        }
    }
}