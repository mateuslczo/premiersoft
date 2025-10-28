namespace BankMore.Application.Models.Responses
{
	public class LoginRequest
	{
		public string Cpf { get; set; } = string.Empty;
		public string Senha { get; set; } = string.Empty;
	}

	public class LoginResponse
	{
		public string Token { get; set; } = string.Empty;
		public DateTime ExpiraEm { get; set; }
	}

}