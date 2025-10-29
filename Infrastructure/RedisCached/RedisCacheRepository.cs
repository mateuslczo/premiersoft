using BankMore.Application.Models.ReadModels;
using BankMore.Domain.Interfaces.IRedisCached;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace BankMore.Infrastructure.RedisCached
{

	public class RedisCacheRepository :IRedisCacheRepository
	{
		private readonly IDistributedCache _cache;
		public RedisCacheRepository(IDistributedCache cache) => _cache = cache;

		public async Task<AccountReadModel?> GetAsync(int accountId)
		{
			var json = await _cache.GetStringAsync($"Account:{accountId}");
			return json == null ? null : JsonSerializer.Deserialize<AccountReadModel>(json);
		}

		public async Task SetAsync(AccountReadModel account, TimeSpan? expiration = null)
		{
			var json = JsonSerializer.Serialize(account);
			await _cache.SetStringAsync($"Account:{account.IdContaCorrente}", json,
				new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5) });
		}
	}
}