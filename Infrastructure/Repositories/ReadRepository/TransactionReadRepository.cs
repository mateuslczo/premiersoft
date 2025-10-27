using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.ReadModels;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using Dapper;

namespace BankMore.Application.Models.Infrastructure.Repositories.ReadRepository
{


    public class TransactionReadRepository : ITransactionReadRepository
    {
        private readonly DapperContext _context;

        public TransactionReadRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<TransactionReadModel> GetTransactionByIdAccountAsync(
            Guid contaId, DateTime? dataInicio, DateTime? dataFim)
        {
            var sql = @"
						  SELECT IdMovimento, idContaCorrente, TipoMovimento, Valor, SaldoAnterior, SaldoAtual, Descricao, DataMovimento
						  FROM movimento 
						  WHERE idContaCorrente = :IdContaCorrente";

			var parameters = new DynamicParameters();
            parameters.Add("idContaCorrente", contaId);

            if (dataInicio.HasValue)
            {
                sql += " AND DataMovimento >= :DataInicio";
                parameters.Add("DataInicio", dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                sql += " AND DataMovimento <= :DataFim";
                parameters.Add("DataFim", dataFim.Value);
            }

            sql += " ORDER BY DataMovimento DESC";

            return await _context.GetConnection().QueryFirstOrDefaultAsync<TransactionReadModel>(sql, parameters);
        }

    }
}