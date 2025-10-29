using BankMore.Application.Models.Infrastructure.Repositories.ReadRepository;
using BankMore.Application.Models.ReadModels;
using BankMore.Application.Queries;
using BankMore.Domain.Interfaces.IRedisCached;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using BankMore.Domain.Interfaces.IServices;
using MediatR;
using Microsoft.Identity.Client;

namespace BankMore.Application.Handlers
{

    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountReadModel?>
    {
        private readonly IAccountReadRepository _repository;
        private readonly ILogger<GetAccountByIdQueryHandler> _logger;
		private readonly IRedisCacheService _redisCacheService;
        private readonly IRedisCacheRepository _redisCacheRepository;

		public GetAccountByIdQueryHandler(
			IAccountReadRepository repository,
			ILogger<GetAccountByIdQueryHandler> logger,
			IRedisCacheRepository redisCacheRepository,
			IRedisCacheService redisCacheService)
		{
			_repository = repository;
			_logger = logger;
			_redisCacheRepository = redisCacheRepository;
			_redisCacheService = redisCacheService;
		}


		public async Task<AccountReadModel?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				_logger.LogInformation("Buscando conta por ID: {AccountId}", request.IdContaCorrente);

				var cacheKey = $"Account:{request.IdContaCorrente}";
				AccountReadModel? account = null;

				// Tentar buscar do cache (Redis)
				try
				{
					account = await _redisCacheService.GetAsync<AccountReadModel>(cacheKey);
					if (account != null)
					{
						_logger.LogInformation("Conta encontrada no cache: {AccountId}", request.IdContaCorrente);
						return account;
					}
				}
				catch (Exception ex)
				{
					_logger.LogWarning(ex, "Falha ao acessar cache para a conta {AccountId}. Continuando sem cache.", request.IdContaCorrente);
				}

				// Buscar no banco de dados
				account = await _repository.GetAccountByIdAsync(request.IdContaCorrente);
				if (account == null)
				{
					_logger.LogWarning("Conta não encontrada para o ID: {AccountId}", request.IdContaCorrente);
					return null;
				}

				// Salvar no cache (Redis)
				try
				{
					await _redisCacheService.SetAsync(cacheKey, account, TimeSpan.FromMinutes(5));
				}
				catch (Exception ex)
				{
					_logger.LogWarning(ex, "Falha ao salvar a conta {AccountId} no cache. Ignorando.", request.IdContaCorrente);
				}

				_logger.LogInformation("Conta encontrada: {AccountNumber} - {AccountName}", account.Numero, account.Nome);
				return account;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao buscar conta por ID: {AccountId}", request.IdContaCorrente);
				throw;
			}
		}

	
    }
}