using BankMore.Domain.Interfaces.IServices;
using Carbon.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BankMore.Application.Services
{
	public class RedisCacheService :IRedisCacheService
	{
		private readonly IDistributedCache? _redisCache;
		private readonly ILogger<RedisCacheService> _logger;
		private readonly bool _redisEnabled;

		public RedisCacheService(
			IDistributedCache? redisCache,
				IOptions<RedisSettings> redisSettings,
			ILogger<RedisCacheService> logger)
		{
			_logger = logger;
			_redisEnabled = redisSettings.Value.Enabled;

			if (_redisEnabled && redisCache != null)
			{
				_redisCache = redisCache;

				try
				{
					_redisCache.GetString("teste-conexao");
				}
				catch (Exception ex)
				{
					_logger.LogWarning(ex, "Não foi possível conectar ao Redis.");
					_redisCache = null;
				}
			}
		}

		public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
		{
			var json = JsonSerializer.Serialize(value);

			if (_redisCache != null)
			{
				try
				{
					await _redisCache.SetStringAsync(key, json, new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
					});
					return;
				}
				catch (Exception ex)
				{
					_logger.LogWarning(ex, "Falha ao salvar no Redis.");
				}
			}

		}

		public async Task<T?> GetAsync<T>(string key)
		{

			try
			{
				var json = await _redisCache.GetStringAsync(key);
				return JsonSerializer.Deserialize<T>(json);

			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex, "Falha ao ler do Redis.");
			}
			return default(T?);
		}

	}
}