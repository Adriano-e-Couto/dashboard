using Microsoft.EntityFrameworkCore;
using repos.Data; 
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS para liberar o acesso do front-end dela
builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberarFront", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();  
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Configura o banco MySQL usando o Pomelo
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Ativa o CORS antes das rotas
app.UseCors("LiberarFront");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Ótimo para testar seus endpoints!
}

app.UseAuthorization();
app.MapControllers();

app.Run();