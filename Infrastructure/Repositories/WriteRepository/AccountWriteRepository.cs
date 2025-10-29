using BankMore.Application.Exceptions;
using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.WriteModels;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using Dapper;

namespace BankMore.Application.Models.Infrastructure.Repositories.WriteRepository
{

    public class AccountWriteRepository : IAccountWriteRepository
    {
        private readonly DapperContext _context;

        public AccountWriteRepository(DapperContext context)
        {
            _context = context;
        }


		public async Task<int> InsertAccountAsync(AccountWriteModel account)
		{
			try
			{
				const string sql = @"
							INSERT INTO contacorrente  (
															idcontacorrente
															, numero
															, nome
															, ativo
                                                            , senha
                                                            , salt
                                                            , saldo
							)
							VALUES 
							(
															:IdContaCorrente
                                                            , :Numero
                                                            , :Nome
                                                            , :Ativo
                                                            , :Senha
                                                            , :Salt
                                                            , :Saldo
							);
							SELECT last_insert_rowid();";



			var id =	await _context.GetConnection().ExecuteScalarAsync<int>(sql, account, _context.Transaction);
			return id;
			}
			catch (Exception ex)
			{

				if (ex.Message.Contains("unique constraint") || ex.Message.Contains("duplicate key"))
				{
					throw new CustomExceptions(
						errorCode: "DUPLICATE_ACCOUNT",
						message: "Já existe uma conta com estes dados.",
						innerException: ex
					);
				}
				else if (ex.Message.Contains("foreign key") || ex.Message.Contains("constraint"))
				{
					throw new CustomExceptions(
						errorCode: "REFERENCE_VIOLATION",
						message: "Violação de restrição de integridade referencial.",
						innerException: ex
					);
				}
				else
				{
					throw new CustomExceptions(
						errorCode: "DATABASE_ERROR",
						message: "Erro ao criar conta no banco de dados.",
						innerException: ex
					);
				}
			}
		}

        public async Task UpdateBalanceAsync(int contaId, decimal novoSaldo)
        {
			const string sql = @"
                                    UPDATE contacorrente 
                                    SET saldo = :NovoSaldo
                                    WHERE idcontacorrente = :ContaId";

			await _context.GetConnection().ExecuteAsync(sql, new
            {
                ContaId = contaId,
                NovoSaldo = novoSaldo,
            }, _context.Transaction);
        }

		public async Task DeactivateAccountAsync(int? contaId)
		{
			const string sql = @"
                                    UPDATE contacorrente 
                                    SET ativo = :Activate
                                    WHERE idcontacorrente = :ContaId";

			await _context.GetConnection().ExecuteAsync(sql, new
			{
				ContaId = contaId,
				Activate = 0,
			}, _context.Transaction);
		}
	}
}