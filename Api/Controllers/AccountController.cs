using BankMore.Application.Commands;
using BankMore.Application.Exceptions;
using BankMore.Application.Extensions;
using BankMore.Application.Models.ReadModels;
using BankMore.Application.Models.Responses;
using BankMore.Application.Queries;
using BankMore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "User")]
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
		public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
		{
			try
			{
				_logger.LogInformation("Criando conta para: {Nome} - Número: {Numero}",
					command.Nome, command.Numero);

				if (string.IsNullOrWhiteSpace(command.Cpf) || string.IsNullOrWhiteSpace(command.Senha))
					return BadRequest(new CreateAccountResponse
					{
						Success = false,
						Message = "CPF e senha são obrigatórios.",
						Error = "CPF e senha são obrigatórios.",
						Tipo = "VALIDATION_ERROR"
					});

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
				new CreateAccountResponse
				{
					Success = true,
					Message = "Conta criada com sucesso",
					IdContaCorrente = accountId
				});
			}
			catch (CustomExceptions ex)
			{
				return ex.ToActionResult();
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
		public async Task<IActionResult> GetAccountById(Guid id)
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
			catch (CustomExceptions ex)
			{
				return ex.ToActionResult();
			}
		}

		/// <summary>
		/// Desativar conta 
		/// </summary>
		/// <returns></returns>
		[HttpPost("Deactivate")]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeactivateAccount([FromBody] DeactivateAccountCommand command)
		{
			try
			{
				_logger.LogInformation("Iniciando desativação da conta");

				var accountId = await _mediator.Send(command);
				return Ok(accountId);

			}
			catch (CustomExceptions ex)
			{
				return ex.ToActionResult();
			}
		}

	}
}
