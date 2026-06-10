using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using repos.Data; 
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
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

// ==================== CONFIGURAÇÃO DO JWT ====================
// IMPORTANTE: Em produção, coloque essa chave no appsettings.json!
// Pega a chave que vai estar configurada no arquivo JSON ou na Hospedagem
var chaveSecreta = builder.Configuration["Jwt:ChaveSecreta"];

if (string.IsNullOrEmpty(chaveSecreta) || chaveSecreta.Length < 32)
{
    throw new Exception("A chave secreta do JWT não foi configurada corretamente ou é muito curta!");
}

var key = Encoding.ASCII.GetBytes(chaveSecreta);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Mude para true em produção
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// =============================================================

var app = builder.Build();

app.UseCors("LiberarFront");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}

// ATENÇÃO: O UseAuthentication DEVE vir antes do UseAuthorization
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();