using BankMore.Application.Exceptions;
using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.ReadModels;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using Dapper;

namespace BankMore.Application.Models.Infrastructure.Repositories.ReadRepository
{

    public class AccountReadRepository : IAccountReadRepository
    {
        private readonly DapperContext _context;

        public AccountReadRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<AccountReadModel> GetAccountByIdAsync(int idContaCorrente)
        {

            const string sql = @"
           SELECT idcontacorrente, numero, nome, ativo, saldo, senha, salt
           FROM contacorrente 
           WHERE idcontacorrente = @ContaId";

			var account = await _context.GetConnection().QueryFirstOrDefaultAsync<AccountReadModel>(
                sql, new { ContaId = idContaCorrente });

			if (account == null)
			{
				throw new CustomExceptions(
							errorCode: "ACCOUNT_NOT_FOUND",
							message: "Conta não encontrada.",
							innerException: null
						);

			}

			return account;
		}

    }
}