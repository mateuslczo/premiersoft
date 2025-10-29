using BankMore.Application.Models.ReadModels;
using MediatR;

namespace BankMore.Application.Queries
{
	public class GetTransactionAccountByIdAccountQuery :IRequest<TransactionReadModel?>
	{
		public int IdContaCorrente { get; set; }

		public GetTransactionAccountByIdAccountQuery(int idContaCorrente)
		{
			IdContaCorrente = idContaCorrente;
		}
	}
}