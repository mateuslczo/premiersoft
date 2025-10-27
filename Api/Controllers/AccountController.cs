using BankMore.Application.Commands;
using BankMore.Application.Models.ReadModels;
using BankMore.Application.Queries;
using BankMore.Domain.Entities;
using BankMore.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountsController :ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<AccountsController> _logger;

		public AccountsController(IMediator mediator, ILogger<AccountsController> logger)
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
		public async Task<ActionResult<Guid>> CreateAccount([FromBody] CreateAccountCommand command)
		{
			try
			{
				_logger.LogInformation("Criando conta para: {Nome} - Número: {Numero}",
					command.Nome, command.Numero);

				if (string.IsNullOrWhiteSpace(command.Cpf) || string.IsNullOrWhiteSpace(command.Senha))
					return BadRequest(new { Error = "CPF e senha são obrigatórios.", Tipo = "VALIDATION_ERROR" });

				var account = new CurrentAccount(command.Nome, command.Senha);

				command.IdContaCorrente = account.IdContaCorrente;
				command.Numero = account.Numero;
				command.Senha = account.Senha;
				command.Salt = account.Salt;
				command.Ativo = account.Ativo;

				var accountId = await _mediator.Send(command);

				_logger.LogInformation("Conta criada com sucesso. ID: {AccountId}", accountId);

				return CreatedAtAction(
					nameof(GetAccountById),
					new { id = accountId },
					new { Id = accountId, Message = "Conta criada com sucesso" });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message+" {CPF}", command.Cpf);
				return StatusCode(400, new { Error = ex.Message });
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
		public async Task<ActionResult<AccountReadModel>> GetAccountById(Guid id)
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
