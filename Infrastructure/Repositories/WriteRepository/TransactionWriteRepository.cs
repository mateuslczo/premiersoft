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


        public async Task AddTransactionByAccountAsync(TransactionWriteModel transaction)
        {
            const string sql = @"
								  INSERT INTO movimento 
								  (idMovimento, idContaCorrente, tipoMovimento, valor, saldoAnterior
								  , saldoAtual, descricao, dataMovimento)
								  VALUES 
								  (:IdMovimento, :IdContaCorrente, :TipoMovimento, :Valor, :SaldoAnterior
								  , :SaldoAtual, :Descricao, :DataMovimento)";

			await _context.GetConnection().ExecuteAsync(sql, transaction, _context.Transaction);
        }
    }
}