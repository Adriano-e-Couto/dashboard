using Microsoft.AspNetCore.Authorization; // Adicionado para o [Authorize]
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

    // 1. OBTÉM TODA A PLANILHA ATUALIZADA (Usado para listar os colaboradores no front!)
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
        var lista = await _context.ColaboradoresMetas // Note: ajuste para o nome correto do seu DbSet se der erro de digitação
            .OrderByDescending(c => c.RecuperadoMensal)
            .ToListAsync();
            
        return Ok(lista);
    }

    // 4. [PÚBLICO] COLABORADOR LANÇA O VALOR (Linkado ao botão "Users" do front)
    // O valor entra na hora para a TV, mas você sabe que precisa ser validado depois
    [HttpPost("lancar-recuperacao-colaborador")]
    public async Task<IActionResult> ColaboradorLancarValor([FromBody] SolicitacaoLancamentoDto dto)
    {
        var colaborador = await _context.ColaboradoresMetas.FindAsync(dto.ColaboradorId);
        if (colaborador == null) return NotFound("Colaborador não encontrado.");

        if (dto.Valor <= 0) return BadRequest("O valor deve ser maior que zero.");

        // Atualiza a semana escolhida na hora (para a TV atualizar instantaneamente)
        switch (dto.Semana.ToUpper())
        {
            case "S1": colaborador.RecuperadoS1 += dto.Valor; break; // Usando += para somar se ele lançar mais de uma vez
            case "S2": colaborador.RecuperadoS2 += dto.Valor; break;
            case "S3": colaborador.RecuperadoS3 += dto.Valor; break;
            case "S4": colaborador.RecuperadoS4 += dto.Valor; break;
            default: return BadRequest("Semana inválida. Use S1, S2, S3 ou S4.");
        }

        // DICA EXTRA: Se quiser salvar em algum lugar que esse valor ainda não foi "auditado" pela gestão,
        // você poderia criar um campo bool na sua tabela chamado "Validado" e colocar como false aqui.

        await _context.SaveChangesAsync();
        return Ok(new { mensagem = "Valor lançado com sucesso no ranking!", colaborador });
    }

    // 5. [PROTEGIDO] ATUALIZAR RECUPERAÇÃO DIRETAMENTE (Apenas a Gestão via Login)
    // Essa é a sua rota antiga (Item 4), agora protegida para que só o gestor mude ou corrija valores de comissão
    [Authorize] 
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

// DTO necessário para receber os dados do botão Users do front-end
public class SolicitacaoLancamentoDto
{
    public int ColaboradorId { get; set; }
    public string Semana { get; set; } // "S1", "S2", etc.
    public decimal Valor { get; set; }
}