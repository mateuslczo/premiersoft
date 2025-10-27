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


        public async Task InsertAccountAsync(AccountWriteModel account)
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
							)";

            await _context.GetConnection().ExecuteAsync(sql, account, _context.Transaction);
        }


        public async Task UpdateBalanceAsync(Guid contaId, decimal novoSaldo)
        {
			const string sql = @"
                                    UPDATE contacorrente 
                                    SET saldo = :NovoSaldo
                                    WHERE idcontacorrente = :ContaId";

			await _context.GetConnection().ExecuteAsync(sql, new
            {
                ContaId = contaId,
                NovoSaldo = novoSaldo,
                UltimaAtualizacao = DateTime.UtcNow
            }, _context.Transaction);
        }
    }
}