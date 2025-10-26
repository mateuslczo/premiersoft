using BankMore.Application.Commands;
using BankMore.Application.Models.Infrastructure.Repositories.WriteRepository;
using BankMore.Application.Models.WriteModels;
using MediatR;

namespace BankMore.Application.Handlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly AccountWriteRepository _repository;

        public CreateAccountCommandHandler(AccountWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            var accountId = Guid.NewGuid();
            //var (passwordHash, salt) = _passwordService.CreatePasswordHash(request.Senha);

            var account = new AccountWriteModel
            {
                IdContaCorrente = accountId,
                Nome = request.Nome,
                Senha = request.Senha,
                Ativo = true
            };

            await _repository.InsertAccountAsync(account);
            return accountId;
        }
    }
}
