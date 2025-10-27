using BankMore.Application.Models.ReadModels;
using BankMore.Application.Queries;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using MediatR;

namespace BankMore.Application.Handlers
{

	public class GetTransactionAccountByIdAccountQueryHandler : IRequestHandler<GetTransactionAccountByIdAccountQuery, TransactionReadModel?>
    {
        private readonly ITransactionReadRepository _repository;
        private readonly ILogger<GetTransactionAccountByIdAccountQueryHandler> _logger;

        public GetTransactionAccountByIdAccountQueryHandler(
			ITransactionReadRepository repository,
            ILogger<GetTransactionAccountByIdAccountQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<TransactionReadModel?> Handle(GetTransactionAccountByIdAccountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Buscando transação por ID: {AccountId}", request.IdContaCorrente);

                var transactionAccount = await _repository.GetTransactionByIdAccountAsync(request.IdContaCorrente);

                if (transactionAccount == null)
                {
                    _logger.LogWarning("Transação não encontrada para o ID: {AccountId}", request.IdContaCorrente);
                    return null;
                }

                _logger.LogInformation("Transação encontrada: {AccountNumber} - {AccountDescription}",
					transactionAccount.IdMovimento, transactionAccount.Descricao);

                return transactionAccount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar conta por ID: {AccountId}", request.IdContaCorrente);
                throw;
            }
        }
    }
}