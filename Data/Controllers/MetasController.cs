using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using repos.Data;

namespace repos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MetasController(AppDbContext context)
    {
        _context = context;
    }

    // 1. OBTÉM TODA A PLANILHA ATUALIZADA
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ColaboradorMeta>>> ObterTodas()
    {
        return await _context.ColaboradoresMetas.ToListAsync();
    }

    // 2. RANKING MENSAL (RETORNA O TOP 3 BASEADO NO VALOR RECUPERADO MENSAL)
    [HttpGet("ranking-top3")]
    public async Task<ActionResult<IEnumerable<object>>> ObterTop3Mensal()
    {
        var colaboradores = await _context.ColaboradoresMetas.ToListAsync();
        
        var ranking = colaboradores
            .OrderByDescending(c => c.RecuperadoMensal)
            .Take(3)
            .Select((c, index) => new
            {
                Posicao = index + 1,
                c.NomeColaborador,
                TotalRecuperado = c.RecuperadoMensal,
                Atingimento = $"{c.PercentualMensal:F2}%"
            });

        return Ok(ranking);
    }

    // 3. MAIORES ENTRADAS (ORDENA TODOS DO MAIOR PARA O MENOR VALOR RECUPERADO)
    [HttpGet("maiores-entradas")]
    public async Task<ActionResult<IEnumerable<ColaboradorMeta>>> ObterMaioresEntradas()
    {
        var lista = await _context.ColaboradoresMetas
            .OrderByDescending(c => c.RecuperadoMensal)
            .ToListAsync();
            
        return Ok(lista);
    }

    // 4. ATUALIZAR RECUPERAÇÃO DE UMA SEMANA ESPECÍFICA (S1, S2, S3 ou S4)
    [HttpPut("{id}/lançar-recuperacao")]
    public async Task<IActionResult> LancarValorSemanal(int id, [FromQuery] string semana, [FromBody] decimal valor)
    {
        var colaborador = await _context.ColaboradoresMetas.FindAsync(id);
        if (colaborador == null) return NotFound("Colaborador não encontrado.");

        switch (semana.ToUpper())
        {
            case "S1": colaborador.RecuperadoS1 = valor; break;
            case "S2": colaborador.RecuperadoS2 = valor; break;
            case "S3": colaborador.RecuperadoS3 = valor; break;
            case "S4": colaborador.RecuperadoS4 = valor; break;
            default: return BadRequest("Semana inválida. Use S1, S2, S3 ou S4.");
        }

        await _context.SaveChangesAsync();
        return Ok(colaborador);
    }
}
