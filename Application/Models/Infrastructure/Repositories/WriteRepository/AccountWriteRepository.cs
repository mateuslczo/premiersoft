using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using Dapper;

namespace BankMore.Application.Models.Infrastructure.Repositories.WriteRepository
{

    public class AccountWriteRepository : IAccountWriteRepository
    {
        private readonly DapperContext _context;

        public AccountWriteRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task InsertAccountAsync(AccountWriteModel account)
        {
            const string sql = @"
							INSERT INTO ContasWriteModel (
															Id
															, NumeroConta
															, Saldo
															, ClienteId
															, NomeCliente
															, DataAbertura
															, Ativa
															, UltimaAtualizacao
							)
							VALUES 
							(
															:Id
															, :NumeroConta
															, :Saldo
															, :ClienteId
															, :NomeCliente
															, :DataAbertura
															, :Ativa
															, :UltimaAtualizacao
							)";

            await _context.Connection.ExecuteAsync(sql, account, _context.Transaction);
        }

        public async Task UpdateBalanceAsync(Guid contaId, decimal novoSaldo)
        {
            const string sql = @"
								   UPDATE ContasWriteModel 
								   SET Saldo = :NovoSaldo, UltimaAtualizacao = :UltimaAtualizacao
								   WHERE Id = :ContaId";

            await _context.Connection.ExecuteAsync(sql, new
            {
                ContaId = contaId,
                NovoSaldo = novoSaldo,
                UltimaAtualizacao = DateTime.UtcNow
            }, _context.Transaction);
        }
    }
}