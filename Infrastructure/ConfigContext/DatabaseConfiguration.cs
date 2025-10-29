using Dapper;
using System.Data;

namespace BankMore.Application.Models.Infrastructure.ConfigContext
{
	/// <summary>
	/// criar três tabelas no banco de dados Oracle
	/// </summary>
	public class DatabaseConfiguration
    {

		/// <summary>
		/// Abre uma conexão com o banco de Oracle usando a string de conexão fornecida
		/// </summary>
		/// <param name="connectionString"></param>
		public static void Initialize(IDbConnection connection)
		{
			// Event Store Table
			const string createEventStoreTable = @"
                 CREATE TABLE EventStore (
                                Id RAW(16) PRIMARY KEY,
                                AggregateId RAW(16) NOT NULL,
                                EventType VARCHAR2(100) NOT NULL,
                                EventData CLOB NOT NULL,
                                Version NUMBER NOT NULL,
                                OccurredOn TIMESTAMP NOT NULL
                            )";

            const string createCurrentAccountTable = @"
                CREATE TABLE contacorrente (
	                idcontacorrente INTEGER PRIMARY KEY AUTOINCREMENT, -- id da conta corrente
	                numero INTEGER NOT NULL UNIQUE, -- numero da conta corrente
	                nome TEXT(100) NOT NULL, -- nome do titular da conta corrente
	                ativo INTEGER(1) NOT NULL default 0, -- indicativo se a conta esta ativa. (0 = inativa, 1 = ativa).
	                senha TEXT(100) NOT NULL,
	                salt TEXT(100) NOT NULL,
                    saldo 
	                CHECK (ativo in (0,1))
                )";

			const string createMovimentTable = @"
			CREATE TABLE IF NOT EXISTS movimento(
	                idMovimento INTEGER PRIMARY KEY AUTOINCREMENT, --identificação única do movimento
	                idContaCorrente  INTEGER NOT NULL, --identificação única da conta corrente
	                descricao TEXT(100) NOT NULL, --identificação única da conta corrente
	                tipoMovimento TEXT(1) NOT NULL, --tipo do movimento: (C = Crédito, D = Débito)
	                dataMovimento TEXT(25) NOT NULL, --data do movimento no formato DD / MM / YYYY
	                valor TEXT NOT NULL, --valor do movimento(duas casas decimais)
	                saldoAnterior TEXT NOT NULL, 	               
                    saldoAtual TEXT NOT NULL, 
	                CHECK(tipoMovimento IN('C', 'D')),
                    FOREIGN KEY(idContaCorrente) REFERENCES contacorrente(idcontacorrente)
               )";

			const string createIdemPotencyTable = @"
                        CREATE TABLE idempotencia(
                                chave_idempotencia TEXT(37) PRIMARY KEY, -- identificacao chave de idempotencia
                                requisicao TEXT(1000), -- dados de requisicao
                                resultado TEXT(1000)   -- dados de retorno
                        )";

            const string createTransferTable = @"
            CREATE TABLE transferencia(
                        idtransferencia INTEGER PRIMARY KEY AUTOINCREMENT, -- identificacao unica da transferencia
                        idcontacorrente_origem INTEGER NOT NULL, -- identificacao unica da conta corrente de origem
                        idcontacorrente_destino INTEGER NOT NULL, -- identificacao unica da conta corrente de destino
                        datamovimento TEXT(25) NOT NULL,     -- data do transferencia no formato DD / MM / YYYY
                        valor REAL NOT NULL,        -- valor da transferencia. Usar duas casas decimais.
                        FOREIGN KEY(idtransferencia) REFERENCES transferencia(idtransferencia)";


			const string createTransacoesReadModelTable = @"

			CREATE TABLE TransacoesReadModel (
                Id RAW(16) PRIMARY KEY,
                ContaId RAW(16) NOT NULL,
                TipoTransacao VARCHAR2(20) NOT NULL,
                Valor NUMBER(18,2) NOT NULL,
                SaldoAnterior NUMBER(18,2) NOT NULL,
                SaldoAtual NUMBER(18,2) NOT NULL,
                Descricao VARCHAR2(200) NOT NULL,
                DataTransacao TIMESTAMP NOT NULL
            )";

            connection.Execute(createEventStoreTable);
            connection.Execute(createCurrentAccountTable);
            connection.Execute(createMovimentTable);
            connection.Execute(createIdemPotencyTable);
            //connection.Execute(createTransferTable);
        }
    }
}