using BankMore.Application.Handlers;
using BankMore.Application.Models.Infrastructure.ConfigContext;
using BankMore.Application.Models.Infrastructure.Repositories.ReadRepository;
using BankMore.Application.Models.Infrastructure.Repositories.WriteRepository;
using BankMore.Application.Services;
using BankMore.Domain.Interfaces.IRepositories.IReadRepository;
using BankMore.Domain.Interfaces.IRepositories.IWriteRepository;
using BankMore.Domain.Interfaces.IServices;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SQLitePCL;
using System.Data;
using System.Text;


namespace BankMore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// JWT Token
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
	});

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
			builder.Services.AddScoped<ITokenService, TokenService>();

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

			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new() { Title = "Bank More API", Version = "v1" });

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Autenticação JWT via header Authorization. Exemplo: Bearer {token}",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
