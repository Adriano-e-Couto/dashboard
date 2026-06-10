using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace repos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidacaoController : ControllerBase
    {
        // Rota para a TV e precisa da bomba do JWT para acessar o ranking (mó role pra fazer isso)
        [HttpGet("ranking")]
        public IActionResult ObterRanking()
        {
            return Ok("Dados do ranking abertos para a TV");
        }

        // Rota BLOQUEADA. Só entra quem mandar o Token JWT válido no cabeçalho
        [Authorize] 
        [HttpPost("aprovar/{id}")]
        public IActionResult AprovarLancamento(int id)
        {
            // Seu código para mudar o status no banco de dados aqui...
            return Ok($"Lançamento {id} aprovado com sucesso pela gestão!");
        }

        // NOVO ENDPOINT: Recebe o cadastro de novos usuários da tela do Front-end
        [Authorize] 
        [HttpPost("cadastrar-usuario")]
        public IActionResult CadastrarUsuario([FromBody] CriarUsuarioDto dto)
        {
            // Validação simples para garantir que os dados obrigatórios vieram preenchidos
            if (dto == null || string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Nome))
            {
                return BadRequest(new { mensagem = "Nome e E-mail são obrigatórios." });
            }

            // Como o front não envia senha, definimos uma temporária padrão (ex: Mudar@123)
            string senhaPadrao = "Mudar@123";

            // TODO: No futuro, conectamos o seu DbContext aqui para salvar de verdade no banco:
            // var novoUsuario = new Colaborador { Nome = dto.Nome, Email = dto.Email, ... };
            // _context.Colaboradores.Add(novoUsuario);
            // _context.SaveChanges();

            return Ok(new { 
                mensagem = $"Usuário {dto.Nome} cadastrado com sucesso como {dto.Perfil}!",
                aviso = $"Senha provisória gerada para o primeiro login: {senhaPadrao}"
            });
        }
    }

    // O contrato (DTO) que vai receber o formato exato que a front estruturou no React
    public class CriarUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Perfil { get; set; } // Administrador, Supervisor ou Operador
    }
}