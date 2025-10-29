using BankMore.Application.Models.ReadModels;

namespace BankMore.Domain.Interfaces.IRepositories.IReadRepository
{
	public interface ITransactionReadRepository
	{
		Task<TransactionReadModel> GetTransactionByIdAccountAsync(int contaId, DateTime? dataInicio=null, DateTime? dataFim= null);

	}
}
