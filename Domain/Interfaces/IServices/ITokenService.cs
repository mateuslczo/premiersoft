namespace BankMore.Domain.Interfaces.IServices
{
	public interface ITokenService
	{
		string GenerateToken(string userId, string userPassword, string role);
		bool ValidateToken(string token);
	}
}