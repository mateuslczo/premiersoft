using Oracle.ManagedDataAccess.Client;
using Dapper;

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
        /// <remarks>Define tres tabelas relacionadas a eventos (necessária quando a aplicação usa padrão CQRS)</remarks>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            using var connection = new OracleConnection(connectionString);
            connection.Open();

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

            // Read Model Tables
            const string createContasReadModelTable = @"
            CREATE TABLE ContasReadModel (
                Id RAW(16) PRIMARY KEY,
                NumeroConta VARCHAR2(20) NOT NULL UNIQUE,
                Saldo NUMBER(18,2) NOT NULL,
                ClienteId RAW(16) NOT NULL,
                NomeCliente VARCHAR2(100) NOT NULL,
                DataAbertura TIMESTAMP NOT NULL,
                Ativa NUMBER(1) NOT NULL,
                UltimaAtualizacao TIMESTAMP NOT NULL
            )";

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
            connection.Execute(createContasReadModelTable);
            connection.Execute(createTransacoesReadModelTable);
        }
    }
}