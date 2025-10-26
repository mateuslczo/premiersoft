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

        public async Task<IEnumerable<TransactionReadModel>> GetTransactionByAccountAsync(
            Guid contaId, DateTime? dataInicio, DateTime? dataFim)
        {
            var sql = @"
						  SELECT Id, ContaId, TipoTransacao, Valor, SaldoAnterior, SaldoAtual, Descricao, DataTransacao
						  FROM TransacoesReadModel 
						  WHERE ContaId = :ContaId";

            var parameters = new DynamicParameters();
            parameters.Add("ContaId", contaId);

            if (dataInicio.HasValue)
            {
                sql += " AND DataTransacao >= :DataInicio";
                parameters.Add("DataInicio", dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                sql += " AND DataTransacao <= :DataFim";
                parameters.Add("DataFim", dataFim.Value);
            }

            sql += " ORDER BY DataTransacao DESC";

            return await _context.Connection.QueryAsync<TransactionReadModel>(sql, parameters);
        }

    }
}