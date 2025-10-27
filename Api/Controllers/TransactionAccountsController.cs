using BankMore.Application.Commands;
using BankMore.Application.Models.ReadModels;
using BankMore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionAccountsController :ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<TransactionAccountsController> _logger;

		public TransactionAccountsController(IMediator mediator, ILogger<TransactionAccountsController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}


		/// <summary>
		/// Criar conta corrente
		/// </summary>
		/// <param name="command"></param>
		/// <returns>Numero da conta</returns>
		[HttpPost]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Guid>> CreateTransaction([FromBody] CreateTransactionAccountCommand command)
		{
			try
			{
				_logger.LogInformation("Iniciando transação na conta Número: {Numero}",
					command.IdMovimento);

				var transactionAccountId = await _mediator.Send(command);

				_logger.LogInformation("Transaçao realizada com sucesso. ID: {TransactionAccountId}", transactionAccountId);

				return CreatedAtAction(
					nameof(GetTransactionAccountById),
					new { id = transactionAccountId },
					new { Id = transactionAccountId, Message = "Transação finalizada com sucesso" });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao iniciar transação para: {Descricao}", command.Descricao);
				return StatusCode(500, new { Error = "Erro interno do servidor" });
			}
		}


		/// <summary>
		/// Buscar conta corrente por id
		/// </summary>
		/// <param name="command"></param>
		/// <returns>Dados da conta</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(AccountReadModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<AccountReadModel>> GetTransactionAccountById(Guid id)
		{
			try
			{
				_logger.LogInformation("Buscando conta por ID: {AccountId}", id);

				var query = new GetAccountByIdQuery(id);
				var account = await _mediator.Send(query);

				if (account == null)
				{
					_logger.LogWarning("Conta não encontrada para o ID: {AccountId}", id);
					return NotFound(new { Message = $"Conta com ID {id} não encontrada" });
				}

				_logger.LogInformation("Conta encontrada: {AccountNumber} - {AccountName}",
					account.Numero, account.Nome);

				return Ok(account);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao buscar conta por ID: {AccountId}", id);
				return StatusCode(500, new { Error = "Erro interno do servidor" });
			}
		}

		/// <summary>
		/// Gerar lista de todas as contas
		/// </summary>
		/// <returns>Lista de contas</returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AccountReadModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<AccountReadModel>>> GetAllAccounts()
		{
			try
			{
				_logger.LogInformation("Buscando todas as contas");

				// Por enquanto, retornar não implementado
				return StatusCode(501, new { Message = "Endpoint em implementação" });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao buscar todas as contas");
				return StatusCode(500, new { Error = "Erro interno do servidor" });
			}
		}

	}
}
