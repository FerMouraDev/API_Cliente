
using API_Cliente.Interface;
using API_Cliente.Repository;
using System.Data;
using System.Data.SqlClient;

namespace API_Cliente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // No Program.cs
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Registra a conexão para o repositório usar
            builder.Services.AddScoped<IDbConnection>(sp =>
                new SqlConnection(connectionString));

            // Registra o seu repositório
            builder.Services.AddScoped<ICliente, ClienteRepository>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
