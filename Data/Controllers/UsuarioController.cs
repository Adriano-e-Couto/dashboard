using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using repos.Data;

namespace repos.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para o dropdown de login
        [HttpGet("colaboradores")]
        public async Task<ActionResult<IEnumerable<object>>> GetColaboradoresParaLogin()
        {
            try
            {
                var colaboradores = await _context.ColaboradoresMetas
                    .AsNoTracking()
                    .Select(c => new 
                    {
                        id = c.Id,
                        nome = c.NomeColaborador
                    })
                    .OrderBy(c => c.nome)
                    .ToListAsync();

                return Ok(colaboradores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message });
            }
        }
    }
}