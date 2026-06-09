using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace repos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidacaoController : ControllerBase
    {
        // Rota que qualquer um (ou a TV) pode acessar para ver o ranking
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
    }
}