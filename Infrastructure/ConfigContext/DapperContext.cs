using System.Data;

namespace BankMore.Application.Models.Infrastructure.ConfigContext
{

	public class DapperContext : IDisposable
    {

		private readonly IDbConnection _connection;
		private readonly IDbTransaction _transaction;

        public DapperContext(IDbConnection connection)
        {
       
            _connection = connection;
            _transaction = _connection.BeginTransaction();
        }

		public IDbConnection GetConnection() => _connection;
        public IDbTransaction Transaction => _transaction;

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