using BankMore.Application.Commands;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using MediatR;

namespace BankMore.Application.Handlers
{
	public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly IAccountWriteRepository _repository;

        public CreateAccountCommandHandler(IAccountWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

		
            var account = new AccountWriteModel
            {
                Nome = request.Nome,
                Senha = request.Senha,
                Ativo = true,
                Salt = request.Salt
            };

           return await _repository.InsertAccountAsync(account);

        }
    }
}
