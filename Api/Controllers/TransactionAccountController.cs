using BankMore.Application.Commands;
using BankMore.Application.Exceptions;
using BankMore.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "User")]
	public class TransactionAccountController :ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<TransactionAccountController> _logger;

		public TransactionAccountController(IMediator mediator, ILogger<TransactionAccountController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}


		/// <summary>
		/// Movimentar conta corrente
		/// </summary>
		/// <param name="command"></param>
		/// <returns>Numero da conta</returns>
		[HttpPut("CurrentAccountTransactions")]
		[ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> MoveAccountCurrent([FromBody] CreateTransactionAccountCommand command)
		{
			try
			{
				_logger.LogInformation("Iniciando transação na conta Número: {Numero}",
					command.IdMovimento);

				var transactionAccountId = await _mediator.Send(command);

				_logger.LogInformation("Transaçao realizada com sucesso. ID: {TransactionAccountId}", transactionAccountId);

				return CreatedAtAction(
					nameof(MoveAccountCurrent),
					new { id = transactionAccountId },
					new { Id = transactionAccountId, Message = "Transação finalizada com sucesso" });
			}
			catch (CustomExceptions ex)
			{
				return ex.ToActionResult();
			}
		}

	}
}
