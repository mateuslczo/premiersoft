using BankMore.Application.Models.ReadModels;
using MediatR;

namespace BankMore.Application.Queries
{
	public class  GetAccountByIdQuery :IRequest<AccountReadModel?>
	{
		public int IdContaCorrente { get; set; }

		public GetAccountByIdQuery(int idContaCorrente)
		{
			IdContaCorrente = idContaCorrente;
		}
	}
}