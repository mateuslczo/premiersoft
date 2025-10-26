using BankMore.Application.Models.ReadModels;

namespace BankMore.Domain.Interfaces.IRepositories.IReadRepository
{
	public interface ITransactionReadRepository
	{
		Task<IEnumerable<TransactionReadModel>> GetTransactionByAccountAsync(Guid contaId, DateTime? dataInicio, DateTime? dataFim);

	}
}
