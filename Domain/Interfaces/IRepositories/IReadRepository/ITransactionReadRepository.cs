using BankMore.Application.Models.ReadModels;

namespace BankMore.Domain.Interfaces.IRepositories.IReadRepository
{
	public interface ITransactionReadRepository
	{
		Task<TransactionReadModel> GetTransactionByIdAccountAsync(Guid contaId, DateTime? dataInicio=null, DateTime? dataFim= null);

	}
}
