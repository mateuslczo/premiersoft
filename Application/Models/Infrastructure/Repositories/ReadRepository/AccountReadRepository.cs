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
           SELECT Id, NumeroConta, Saldo, ClienteId, NomeCliente, DataAbertura, Ativa, UltimaAtualizacao
           FROM ContasReadModel 
           WHERE Id = :ContaId";

            return await _context.Connection.QueryFirstOrDefaultAsync<AccountReadModel>(
                sql, new { ContaId = contaId });
        }

    }
}