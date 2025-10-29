using BankMore.Application.Exceptions;
using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using Dapper;
using System.ServiceModel.Channels;

namespace BankMore.Application.Models.Infrastructure.Repositories.WriteRepository
{


	public class TransactionWriteRepository :ITransactionWriteRepository
	{
		private readonly DapperContext _context;

		public TransactionWriteRepository(DapperContext context)
		{
			_context = context;
		}


		public async Task<int> AddTransactionByAccountAsync(TransactionWriteModel transaction)
		{
			try
			{
				const string sql = @"
								  INSERT INTO movimento 
								  (idMovimento, idContaCorrente, tipoMovimento, valor, saldoAnterior
								  , saldoAtual, descricao, dataMovimento)
								  VALUES 
								  (:IdMovimento, :IdContaCorrente, :TipoMovimento, :Valor, :SaldoAnterior
								  , :SaldoAtual, :Descricao, :DataMovimento)";

			return	await _context.GetConnection().ExecuteScalarAsync<int>(sql, transaction, _context.Transaction);
			}
			catch (Exception ex)
			{

				if (ex.Message.Contains("unique constraint") || ex.Message.Contains("duplicate key"))
				{
					throw new CustomExceptions(
						errorCode: "DUPLICATE_ACCOUNT",
						message: "Movimentação em duplicidade.",
						innerException: ex
					);
				}
				else if (ex.Message.Contains("foreign key") || ex.Message.Contains("constraint"))
				{
					throw new CustomExceptions(
						errorCode: "REFERENCE_VIOLATION",
						message: "Violação de restrição de integridade referencial.",
						innerException: ex
					);
				}
				else
				{
					throw new CustomExceptions(
						errorCode: "DATABASE_ERROR",
						message: "Erro ao movimentar conta no banco de dados.",
						innerException: ex
					);
				}
			}
		}
	}
}