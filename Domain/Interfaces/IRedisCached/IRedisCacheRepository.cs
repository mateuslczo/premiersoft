using BankMore.Application.Models.ReadModels;

namespace BankMore.Domain.Interfaces.IRedisCached
{

	/// <summary>
	/// Repositório de cache para operações com contas bancárias
	/// </summary>
	/// <remarks>
	/// Esta interface define as operações para gerenciar o cache de contas bancárias,
	/// melhorando a performance das consultas frequentes.
	/// </remarks>
	public interface IRedisCacheRepository
	{
		/// <summary>
		/// Obtém uma conta do cache pelo número da conta
		/// </summary>
		/// <param name="accountId">Número da conta</param>
		/// <returns>
		/// Dados da conta encontrada no cache ou null se não existir
		/// </returns>
		Task<AccountReadModel?> GetAsync(int accountId);

		/// <summary>
		/// Armazena uma conta no cache com tempo de expiração opcional
		/// </summary>
		/// <param name="account">Dados completos da conta a serem armazenados</param>
		/// <param name="expiration">
		/// Tempo de expiração do cache (padrão: 30 minutos)
		Task SetAsync(AccountReadModel account, TimeSpan? expiration = null);
	}
}