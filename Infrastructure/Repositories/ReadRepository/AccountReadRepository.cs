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

        public async Task<AccountReadModel> GetAccountByIdAsync(Guid contaId)
        {
            const string sql = @"
           SELECT idcontacorrente, numero, nome, ativo, saldo
           FROM contacorrente 
           WHERE idcontacorrente = :ContaId";

			var account = await _context.GetConnection().QueryFirstOrDefaultAsync<AccountReadModel>(
                sql, new { ContaId = contaId });

			if (account == null)
			{
				throw new KeyNotFoundException($"Conta com ID {contaId} não encontrada.");
			}

			return account;
		}

    }
}