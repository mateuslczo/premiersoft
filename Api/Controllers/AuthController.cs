using BankMore.Application.Commands;
using BankMore.Application.Exceptions;
using BankMore.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankMore.Api.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class AuthController :ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<AuthController> _logger;

		public AuthController(IMediator mediator, ILogger<AuthController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[HttpPost("login")]
		[AllowAnonymous]
		[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			try
			{
				var command = new LoginCommand
				{
					Cpf = request.Cpf,
					Senha = request.Senha
				};

				var response = await _mediator.Send(command);
				return Ok(response);
			}
			catch (CustomExceptions ex) when (ex.ErrorCode== "INVALID_DOCUMENT")
			{
				return BadRequest(new { Error = ex.Message, Type = ex.ErrorCode });
			}
			catch (CustomExceptions ex) when (ex.ErrorCode == "INVALID_CREDENTIALS" || ex.ErrorCode == "INVALID_ACCOUNT")
			{
				return Unauthorized(new { Error = ex.Message, Type = ex.ErrorCode });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro inesperado no login.");
				return StatusCode(500, new { Error = "Erro interno no servidor." });
			}
		}
	}
}