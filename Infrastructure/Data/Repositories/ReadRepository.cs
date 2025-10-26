using BankMore.Infrastructure.Interfaces.IRepositories;
using Dapper;
using System.Data;

namespace BankMore.Infrastructure.Data.Repositories
{

	public abstract class ReadRepository<TEntity, TKey> :IReadRepository<TEntity, TKey>
		where TEntity : class
	{
		protected readonly IDbConnection _connection;
		protected readonly IDbTransaction _transaction;
		protected readonly string _tableName;
		protected readonly string _keyColumn;

		protected ReadRepository(DapperContext context, string tableName, string keyColumn = "Id")
		{
			_connection = context.Connection;
			_transaction = context.Transaction;
			_tableName = tableName;
			_keyColumn = keyColumn;
		}


		public Task<TEntity> GetRecordByIdAsync(TKey id)
		{
			var sql = $"SELECT * FROM {_tableName} WHERE {_keyColumn} = :id";
			return _connection.QueryFirstOrDefaultAsync<TEntity>(sql, new { id }, _transaction);
		}

		public Task<IEnumerable<TEntity>> GetAllRecordAsync()
		{
			var sql = $"SELECT * FROM {_tableName}";
			return _connection.QueryAsync<TEntity>(sql, transaction: _transaction);
		}

		public async Task<bool> HasRecordAsync(TKey id)
		{
			var sql = $"SELECT COUNT(1) FROM {_tableName} WHERE {_keyColumn} = :id";
			var count = await _connection.ExecuteScalarAsync<int>(sql, new { id }, _transaction);
			return count > 0;
		}
	}
}