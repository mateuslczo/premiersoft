using BankMore.Application.Models.ReadModels;
using MediatR;

namespace BankMore.Application.Queries
{
	public class GetTransactionAccountByIdAccountQuery :IRequest<TransactionReadModel?>
	{
		public Guid IdContaCorrente { get; set; }

		public GetTransactionAccountByIdAccountQuery(Guid idContaCorrente)
		{
			IdContaCorrente = idContaCorrente;
		}
	}
}