using API_Cliente.Interface;
using API_Cliente.Repository;
using System.Data;
using Microsoft.OpenApi;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// 1. Serviços básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 2. Configuração ÚNICA do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Cliente API",
        Version = "v1",
        Description = "API para gestão de clientes e produção"
    });
});

// 3. Banco de Dados e Repositório
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));
builder.Services.AddScoped<ICliente, ClienteRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantém PascalCase original
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("PermitirTudo");

// 4. Configuração do Pipeline (Middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cliente API V1");
        // Isso aqui faz o Swagger ser a página inicial da API:
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();