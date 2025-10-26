using Oracle.ManagedDataAccess.Client;
using System.Text.Json;

namespace BankMore.Application.Models.Infrastructure.ConfigContext
{

    public class DapperContext : IDisposable
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
}