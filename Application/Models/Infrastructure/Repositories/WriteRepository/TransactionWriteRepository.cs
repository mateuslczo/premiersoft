using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using Dapper;

namespace BankMore.Application.Models.Infrastructure.Repositories.WriteRepository
{


    public class TransactionWriteRepository : ITransactionWriteRepository
    {
        private readonly DapperContext _context;

        public TransactionWriteRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task AddTransactionByAccountAsync(TransactionWriteModel transacao)
        {
            const string sql = @"
								  INSERT INTO TransacoesReadModel 
								  (Id, ContaId, TipoTransacao, Valor, SaldoAnterior
								  , SaldoAtual, Descricao, DataTransacao)
								  VALUES 
								  (:Id, :ContaId, :TipoTransacao, :Valor, :SaldoAnterior
								  , :SaldoAtual, :Descricao, :DataTransacao)";

            await _context.Connection.ExecuteAsync(sql, transacao, _context.Transaction);
        }
    }
}