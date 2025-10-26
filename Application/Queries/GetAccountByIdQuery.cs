using BankMore.Application.Models.ReadModels;
using MediatR;

namespace BankMore.Application.Queries
{
	public class  GetAccountByIdQuery :IRequest<AccountReadModel?>
	{
		public Guid IdContaCorrente { get; set; }

		public GetAccountByIdQuery(Guid idContaCorrente)
		{
			IdContaCorrente = idContaCorrente;
		}
	}
}