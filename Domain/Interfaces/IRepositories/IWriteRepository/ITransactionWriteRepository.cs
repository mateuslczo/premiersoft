using BankMore.Application.Models.WriteModels;

namespace BankMore.Domain.Interfaces.IRepositories.IWriteRepository
{
	public interface ITransactionWriteRepository
	{

		/// <summary>
		/// Persiste uma transação
		/// </summary>
		/// <param name="transaction"></param>
		/// <returns></returns>
		Task AddTransactionByAccountAsync(TransactionWriteModel transaction);

	}
}
