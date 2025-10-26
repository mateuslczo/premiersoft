using Oracle.ManagedDataAccess.Client;
using System.Text.Json;

namespace BankMore.Infrastructure.Data
{

	public class DapperContext :IDisposable
	{
		private readonly OracleConnection _connection;
		private readonly OracleTransaction _transaction;

		public DapperContext(string connectionString)
		{
			_connection = new OracleConnection(connectionString);
			_connection.Open();
			_transaction = _connection.BeginTransaction();
		}

		public OracleConnection Connection => _connection;
		public OracleTransaction Transaction => _transaction;

		public void Commit()
		{
			_transaction.Commit();
		}

		public void Rollback()
		{
			_transaction.Rollback();
		}

		public void Dispose()
		{
			_transaction?.Dispose();
			_connection?.Dispose();
		}
	}

	
	//// Infrastructure/Repositories/ReadRepositories.cs
	//public class ContaReadRepository :IContaReadRepository
	//{
	//	private readonly DapperContext _context;

	//	public ContaReadRepository(DapperContext context)
	//	{
	//		_context = context;
	//	}

	//	public async Task<ContaReadModel> ObterPorIdAsync(Guid contaId)
	//	{
	//		const string sql = @"
 //           SELECT Id, NumeroConta, Saldo, ClienteId, NomeCliente, DataAbertura, Ativa, UltimaAtualizacao
 //           FROM ContasReadModel 
 //           WHERE Id = :ContaId";

	//		return await _context.Connection.QueryFirstOrDefaultAsync<ContaReadModel>(
	//			sql, new { ContaId = contaId });
	//	}

	//	public async Task InserirAsync(ContaReadModel conta)
	//	{
	//		const string sql = @"
 //           INSERT INTO ContasReadModel 
 //           (Id, NumeroConta, Saldo, ClienteId, NomeCliente, DataAbertura, Ativa, UltimaAtualizacao)
 //           VALUES 
 //           (:Id, :NumeroConta, :Saldo, :ClienteId, :NomeCliente, :DataAbertura, :Ativa, :UltimaAtualizacao)";

	//		await _context.Connection.ExecuteAsync(sql, conta, _context.Transaction);
	//	}

	//	public async Task AtualizarSaldoAsync(Guid contaId, decimal novoSaldo)
	//	{
	//		const string sql = @"
 //           UPDATE ContasReadModel 
 //           SET Saldo = :NovoSaldo, UltimaAtualizacao = :UltimaAtualizacao
 //           WHERE Id = :ContaId";

	//		await _context.Connection.ExecuteAsync(sql, new
	//		{
	//			ContaId = contaId,
	//			NovoSaldo = novoSaldo,
	//			UltimaAtualizacao = DateTime.UtcNow
	//		}, _context.Transaction);
	//	}
	//}

	//public class TransacaoReadRepository :ITransacaoReadRepository
	//{
	//	private readonly DapperContext _context;

	//	public TransacaoReadRepository(DapperContext context)
	//	{
	//		_context = context;
	//	}

	//	public async Task<IEnumerable<TransacaoReadModel>> ObterTransacoesPorContaAsync(
	//		Guid contaId, DateTime? dataInicio, DateTime? dataFim)
	//	{
	//		var sql = @"
 //           SELECT Id, ContaId, TipoTransacao, Valor, SaldoAnterior, SaldoAtual, Descricao, DataTransacao
 //           FROM TransacoesReadModel 
 //           WHERE ContaId = :ContaId";

	//		var parameters = new DynamicParameters();
	//		parameters.Add("ContaId", contaId);

	//		if (dataInicio.HasValue)
	//		{
	//			sql += " AND DataTransacao >= :DataInicio";
	//			parameters.Add("DataInicio", dataInicio.Value);
	//		}

	//		if (dataFim.HasValue)
	//		{
	//			sql += " AND DataTransacao <= :DataFim";
	//			parameters.Add("DataFim", dataFim.Value);
	//		}

	//		sql += " ORDER BY DataTransacao DESC";

	//		return await _context.Connection.QueryAsync<TransacaoReadModel>(sql, parameters);
	//	}

	//	public async Task InserirAsync(TransacaoReadModel transacao)
	//	{
	//		const string sql = @"
 //           INSERT INTO TransacoesReadModel 
 //           (Id, ContaId, TipoTransacao, Valor, SaldoAnterior, SaldoAtual, Descricao, DataTransacao)
 //           VALUES 
 //           (:Id, :ContaId, :TipoTransacao, :Valor, :SaldoAnterior, :SaldoAtual, :Descricao, :DataTransacao)";

	//		await _context.Connection.ExecuteAsync(sql, transacao, _context.Transaction);
	//	}
	//}
}