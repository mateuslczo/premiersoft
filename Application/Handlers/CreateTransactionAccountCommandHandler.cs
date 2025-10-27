using BankMore.Application.Commands;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using MediatR;

namespace BankMore.Application.Handlers
{
	public class CreateTransactionAccountCommandHandler : IRequestHandler<CreateTransactionAccountCommand, Guid>
    {
        private readonly ITransactionWriteRepository _repository;

        public CreateTransactionAccountCommandHandler(ITransactionWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateTransactionAccountCommand request, CancellationToken cancellationToken)
        {

            var transactionAccountId = Guid.NewGuid();

            var transactionAccount = new TransactionWriteModel
            {
                IdMovimento = transactionAccountId,
                IdContaCorrente = request.IdContaCorrente,
                Descricao = request.Descricao,
                Valor = request.Valor,
                TipoMovimento = request.TipoMovimento,
                DataMovimento = DateTime.Now.ToShortDateString(),
         
            };

            await _repository.AddTransactionByAccountAsync(transactionAccount);
            return transactionAccountId;
        }
    }
}
