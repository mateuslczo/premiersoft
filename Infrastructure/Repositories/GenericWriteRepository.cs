using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Domain.Interfaces.IRepositories;
using Dapper;
using System.Data;

namespace BankMore.Application.Models.Infrastructure.Repositories
{

    public abstract class GenericWriteRepository<TEntity, TKey> : IGenericWriteRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;
        protected readonly string _tableName;
        protected readonly string _keyColumn;

        protected GenericWriteRepository(DapperContext context, string tableName, string keyColumn = "Id")
        {
            _connection = context.GetConnection();
            _transaction = context.Transaction;
            _tableName = tableName;
            _keyColumn = keyColumn;
        }


        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var properties = typeof(TEntity).GetProperties()
                    .Where(p => p.Name != _keyColumn || !p.GetGetMethod().IsVirtual)
                    .ToArray();

            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var parameterNames = string.Join(", ", properties.Select(p => $":{p.Name}"));
            var sql = $@"
							INSERT INTO {_tableName} ({columnNames}) 
							VALUES ({parameterNames})
							RETURNING {_keyColumn} INTO :{_keyColumn}Output";

            var parameters = new DynamicParameters(entity);
            parameters.Add($"{_keyColumn}Output", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var insertedEntity = await _connection.QueryFirstOrDefaultAsync<TEntity>(sql, entity, _transaction);

            return insertedEntity ?? entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var properties = typeof(TEntity).GetProperties()
                .Where(p => p.Name != _keyColumn && !p.GetGetMethod().IsVirtual)
                .ToArray();

            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = :{p.Name}"));

            var sql = $"UPDATE {_tableName} SET {setClause} WHERE {_keyColumn} = :{_keyColumn}";

            var rowsAffected = await _connection.ExecuteAsync(sql, entity, _transaction);

            if (rowsAffected == 0)
            {

                return default;
            }

            return entity;
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            var sql = $"DELETE FROM {_tableName} WHERE {_keyColumn} = :id";
            var rowsAffected = await _connection.ExecuteAsync(sql, new { id }, _transaction);

            if (rowsAffected == 0)
                return false;
            return true;
        }
    }
}