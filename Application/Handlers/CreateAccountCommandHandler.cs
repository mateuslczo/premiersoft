using BankMore.Application.Commands;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Exceptions;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using BankMore.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;

namespace BankMore.Application.Handlers
{
	public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IAccountWriteRepository _repository;

        public CreateAccountCommandHandler(IAccountWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

			var cpfResult = CpfValidator.Validate(request.Cpf);
			if (cpfResult.IsFailure)
				throw new CustomExceptions("INVALID_DOCUMENT", cpfResult.Error);

			var accountId = Guid.NewGuid();

            var account = new AccountWriteModel
            {
                IdContaCorrente = accountId,
                Nome = request.Nome,
                Senha = request.Senha,
                Ativo = true,
                Salt = request.Salt
            };

            await _repository.InsertAccountAsync(account);
            return accountId;
        }
    }
}
