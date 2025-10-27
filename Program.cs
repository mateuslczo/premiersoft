using BankMore.Application.Handlers;
using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.Infrastructure.Repositories.ReadRepository;
using BankMore.Application.Models.Infrastructure.Repositories.WriteRepository;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using Dapper;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Data;

namespace BankMore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			SqlMapper.AddTypeHandler(new DecimalStringHandler());

			// Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<IAccountWriteRepository, AccountWriteRepository>();
			builder.Services.AddScoped<ITransactionWriteRepository, TransactionWriteRepository>();
			builder.Services.AddScoped<IAccountReadRepository, AccountReadRepository>();
			builder.Services.AddScoped<ITransactionReadRepository, TransactionReadRepository>();

			builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateAccountCommandHandler).Assembly));

			//Config database SqLite em memória
			Batteries.Init();

			builder.Services.AddSingleton<IDbConnection>(provider =>
			{
				var connection = new SqliteConnection("Data Source=:memory:");
				connection.Open();

				// Criar tabelas de exemplo
				DatabaseConfiguration.Initialize(connection);

				return connection;
			});

			builder.Services.AddSingleton<DapperContext>(provider =>
			{
				var connection = provider.GetRequiredService<IDbConnection>();
				return new DapperContext(connection);
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
