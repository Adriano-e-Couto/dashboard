using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace repos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // 1. Criamos uma variável privada para guardar as configurações
        private readonly IConfiguration _configuration;

        // 2. O Construtor recebe o IConfiguration automaticamente do .NET
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Substitua isso pela sua lógica de busca no Banco de Dados (AppDbContext)
            if (model.Usuario == "admin" && model.Senha == "gestao123")
            {
                var token = GerarTokenJwt(model.Usuario);
                return Ok(new { token = token });
            }

            return Unauthorized(new { mensagem = "Usuário ou senha inválidos" });
        }

        private string GerarTokenJwt(string usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            // 3. AGORA LÊ DA CONFIGURAÇÃO! Igualzinho fizemos no Program.cs
            var chaveSecreta = _configuration["Jwt:ChaveSecreta"];

            if (string.IsNullOrEmpty(chaveSecreta) || chaveSecreta.Length < 32)
            {
                throw new Exception("A chave secreta do JWT não foi configurada corretamente no arquivo de configuração!");
            }

            var key = Encoding.ASCII.GetBytes(chaveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario),
                    new Claim(ClaimTypes.Role, "Gestor") 
                }),
                Expires = DateTime.UtcNow.AddHours(4), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}