namespace BankMore.Domain.Exceptions
{
	public class CustomExceptions : Exception
	{
		public CustomExceptions(string code, string message):base(message)
		{
			Code = code;
		}

		public string Code { get; }
    }
}
