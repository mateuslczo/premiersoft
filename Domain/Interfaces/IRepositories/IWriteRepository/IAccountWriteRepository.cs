using BankMore.Application.Models.WriteModels;

namespace BankMore.Domain.Interfaces.IRepositories.IWriteRepository
{
	/// <summary>
	/// Interface que define metodos de escrita contas bancarias
	/// </summary>
	public interface IAccountWriteRepository
	{

		/// <summary>
		/// Adiciona um registro na tabela.
		/// </summary>
		Task InsertAccountAsync(AccountWriteModel account);

		/// <summary>
		/// Atualiza um registro na tabela.
		/// </summary>
		Task UpdateBalanceAsync(Guid contaId, decimal novoSaldo);

	}
}