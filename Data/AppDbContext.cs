using Microsoft.EntityFrameworkCore;

namespace repos.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ColaboradorMeta> ColaboradoresMetas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Inicial com os nomes reais da planilha e metas semanais padrão de 6000 (Total 24000 mensal)
        modelBuilder.Entity<ColaboradorMeta>().HasData(
            new ColaboradorMeta { Id = 1, NomeColaborador = "BEN HUR MAIA DE ALMEIDA", MetaS1 = 6000, MetaS2 = 6000, MetaS3 = 6000, MetaS4 = 6000, RecuperadoS1 = 4208 },
            new ColaboradorMeta { Id = 2, NomeColaborador = "DAVI DE OLIVEIRA MELO", MetaS1 = 6000, MetaS2 = 6000, MetaS3 = 6000, MetaS4 = 6000, RecuperadoS1 = 3752 },
            new ColaboradorMeta { Id = 3, NomeColaborador = "GUILHERME NASCIMENTO", MetaS1 = 6000, MetaS2 = 6000, MetaS3 = 6000, MetaS4 = 6000, RecuperadoS1 = 7308 },
            new ColaboradorMeta { Id = 4, NomeColaborador = "ANA LUISA", MetaS1 = 6000, MetaS2 = 6000, MetaS3 = 6000, MetaS4 = 6000, RecuperadoS1 = 3666 },
            new ColaboradorMeta { Id = 5, NomeColaborador = "GUILHERME SOARES", MetaS1 = 6000, MetaS2 = 6000, MetaS3 = 6000, MetaS4 = 6000, RecuperadoS1 = 1079 },
            new ColaboradorMeta { Id = 6, NomeColaborador = "VICTORIA", MetaS1 = 6000, MetaS2 = 6000, MetaS3 = 6000, MetaS4 = 6000, RecuperadoS1 = 2111 }
            
        );
    }
}

public class ColaboradorMeta
{
    public int Id { get; set; }
    public string NomeColaborador { get; set; } = string.Empty;

    // --- REGRAS DE DATAS DE CONTRATO ---
    // Registra a vigência do período de cobrança (ex: 01/06/2026 até 30/06/2026)
    public DateTime DataInicialContrato { get; set; } = new DateTime(2026, 06, 01);
    public DateTime DataFinalContrato { get; set; } = new DateTime(2026, 06, 30);

    // --- METAS SEMANAIS ---
    public decimal MetaS1 { get; set; }
    public decimal MetaS2 { get; set; }
    public decimal MetaS3 { get; set; }
    public decimal MetaS4 { get; set; }
    public decimal MetaMensal => MetaS1 + MetaS2 + MetaS3 + MetaS4;

    // --- VALORES RECUPERADOS (MAIORES ENTRADAS) ---
    public decimal RecuperadoS1 { get; set; }
    public decimal RecuperadoS2 { get; set; }
    public decimal RecuperadoS3 { get; set; }
    public decimal RecuperadoS4 { get; set; }
    public decimal RecuperadoMensal => RecuperadoS1 + RecuperadoS2 + RecuperadoS3 + RecuperadoS4;

    // --- FAIXAS DE ATINGIMENTO SEMANAL ---
    public decimal PercentualS1 => MetaS1 > 0 ? (RecuperadoS1 / MetaS1) * 100 : 0;
    public decimal PercentualS2 => MetaS2 > 0 ? (RecuperadoS2 / MetaS2) * 100 : 0;
    public decimal PercentualS3 => MetaS3 > 0 ? (RecuperadoS3 / MetaS3) * 100 : 0;
    public decimal PercentualS4 => MetaS4 > 0 ? (RecuperadoS4 / MetaS4) * 100 : 0;
    public decimal PercentualMensal => MetaMensal > 0 ? (RecuperadoMensal / MetaMensal) * 100 : 0;

    // --- INCLUSÃO DE FAIXAS (REGRA DE NEGÓCIO: 4 FAIXAS DE COBRANÇA) ---
    // Retorna a classificação baseada no atingimento mensal do colaborador
    public string FaixaAtingimento
    {
        get
        {
            if (PercentualMensal >= 100) return "Faixa 4 - Excelente (Meta Atingida)";
            if (PercentualMensal >= 75)  return "Faixa 3 - Ótimo";
            if (PercentualMensal >= 50)  return "Faixa 2 - Regular";
            return "Faixa 1 - Crítico (Abaixo do esperado)";
        }
    }
}
