namespace BankMore.Application.Services
{
	public class RedisSettingsService
	{
		public bool Enabled { get; set; }
		public string ConnectionString { get; set; } = string.Empty;
	}
}