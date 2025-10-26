using BankMore.Application.Models.ReadModels;

namespace BankMore.Domain.Interfaces.IRepositories.IReadRepository
{
	public interface IAccountReadRepository
	{
		/// <summary>
		/// Recupera dados da conta por ID (idcontacorrente)
		/// </summary>
		/// <param name="idContaCorrente">O ID único da conta corrente</param>
		/// <returns>Dados da conta ou null se não encontrado</returns>
		Task<AccountReadModel?> GetAccountByIdAsync(Guid contaId);

	}
}