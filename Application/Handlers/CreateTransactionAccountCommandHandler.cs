using BankMore.Application.Commands;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using MediatR;

namespace BankMore.Application.Handlers
{
	public class CreateTransactionAccountCommandHandler : IRequestHandler<CreateTransactionAccountCommand, int>
    {
        private readonly ITransactionWriteRepository _repository;

        public CreateTransactionAccountCommandHandler(ITransactionWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateTransactionAccountCommand request, CancellationToken cancellationToken)
        {

            var transactionAccount = new TransactionWriteModel
            {
                IdContaCorrente = request.IdContaCorrente,
                Descricao = request.Descricao,
                Valor = request.Valor,
                TipoMovimento = request.TipoMovimento,
                DataMovimento = DateTime.Now.ToShortDateString(),
         
            };

            return await _repository.AddTransactionByAccountAsync(transactionAccount);
      
        }
    }
}
