using Microsoft.EntityFrameworkCore;
using repos.Data; 
using Scalar.AspNetCore;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS para liberar o acesso do HTML local
builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberarFront", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();  
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

// ==========================================
// GERADOR DO FRONT-END BRUTO (SINTAXE CORRIGIDA)
// ==========================================
string htmlContent = @"<!DOCTYPE html>
<html lang='pt-BR'>
<head>
    <meta charset='UTF-8'>
    <title>Dashboard de Cobrança & Metas</title>
    <style>
        :root { --bg: #0f172a; --card: #1e293b; --text: #f8fafc; --muted: #94a3b8; --primary: #6366f1; --success: #10b981; --danger: #ef4444; }
        body { font-family: sans-serif; background: var(--bg); color: var(--text); padding: 20px; margin: 0; }
        .grid { display: grid; grid-template-columns: 2fr 1fr; gap: 20px; margin-bottom: 20px; }
        .card { background: var(--card); padding: 20px; border-radius: 12px; border: 1px solid #334155; }
        .form-row { display: grid; grid-template-columns: repeat(3, 1fr); gap: 15px; margin-bottom: 15px; }
        .form-group { display: flex; flex-direction: column; gap: 5px; }
        select, input { padding: 10px; background: #0f172a; border: 1px solid #334155; border-radius: 6px; color: white; font-size: 16px; }
        button { background: var(--primary); color: white; border: none; padding: 12px; border-radius: 6px; font-weight: bold; cursor: pointer; width: 100%; }
        .ranking-item { display: flex; justify-content: space-between; padding: 10px 0; border-bottom: 1px solid #334155; }
        .flex-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(300px, 1fr)); gap: 20px; }
        .p-bg { background: #0f172a; border-radius: 8px; height: 16px; margin: 10px 0; overflow: hidden; position: relative; border: 1px solid #334155; }
        .p-bar { background: var(--success); height: 100%; width: 0%; transition: width 0.4s; }
    </style>
</head>
<body>
    <div style='max-width:1200px; margin:0 auto;'>
        <div style='display:flex; justify-content:space-between; align-items:center; margin-bottom:20px; padding-bottom:15px; border-bottom:1px solid #334155;'>
            <div>
                <h1>📊 Dashboard de Cobrança & Metas</h1>
                <p style='color:var(--muted); margin:5px 0 0 0;'>Vigência: 01/06/2026 a 30/06/2026</p>
            </div>
            <div style='text-align:right;'>
                <button style='width:auto; padding:8px 16px; border-radius:20px;' onclick='atualizar()'>🔄 AO VIVO</button>
                <div style='font-size:12px; color:var(--muted); margin-top:5px;'>Atualizado: <span id='hora'>--:--:--</span></div>
            </div>
        </div>

        <div class='grid'>
            <div class='card'>
                <h2>📥 Painel de Lançamento</h2>
                <form id='formLancar' onsubmit='enviar(event)'>
                    <div class='form-row'>
                        <div class='form-group'><label>Colaborador</label><select id='colab' required></select></div>
                        <div class='form-group'><label>Semana</label><select id='sem'><option value='S1'>S1</option><option value='S2'>S2</option><option value='S3'>S3</option><option value='S4'>S4</option></select></div>
                        <div class='form-group'><label>Valor</label><input type='number' id='val' step='0.01' min='0' required></div>
                    </div>
                    <button type='submit'>Lançar no Banco de Dados</button>
                </form>
            </div>
            <div class='card'>
                <h2>🏆 Ranking Top 3</h2>
                <div id='ranking'></div>
            </div>
        </div>

        <h2>👥 Desempenho da Equipe</h2>
        <div class='flex-grid' id='cards'></div>
    </div>

    <script>
        const URL = 'http://localhost:5292/api/metas/';

        async function atualizar() {
            try {
                const res = await fetch(URL);
                const dados = await res.json();
                
                const cards = document.getElementById('cards');
                const select = document.getElementById('colab');
                const selAnt = select.value;
                
                cards.innerHTML = '';
                select.innerHTML = '<option value="""">Selecione...</option>';
                
                dados.forEach(c => {
                    // Preenche o campo select
                    const opt = document.createElement('option');
                    opt.value = c.id;
                    opt.textContent = c.nomeColaborador;
                    select.appendChild(opt);

                    // Formata moedas de forma segura
                    const fMeta = c.metaMensal.toLocaleString('pt-BR', {style:'currency', currency:'BRL'});
                    const fRec = c.recuperadoMensal.toLocaleString('pt-BR', {style:'currency', currency:'BRL'});
                    const fFal = c.quantoFalta.toLocaleString('pt-BR', {style:'currency', currency:'BRL'});
                    const fS1 = c.recuperadoS1.toLocaleString('pt-BR', {style:'currency', currency:'BRL'});
                    
                    // Calcula largura máxima de segurança para a barra
                    const larguraBarra = Math.min(c.percentualMensal, 100).toFixed(1);

                    // Cria o HTML do card concatenando de forma limpa e segura
                    let htmlCard = '<div class=""card"">';
                    htmlCard += '<div style=""display:flex; justify-content:space-between;""><b>' + c.nomeColaborador + '</b><small style=""color:var(--primary)"">' + c.faixaAtingimento + '</small></div>';
                    htmlCard += '<div style=""margin-top:10px; font-size:14px;"">Progresso: ' + c.percentualMensal.toFixed(1) + '%</div>';
                    htmlCard += '<div class=""p-bg""><div class=""p-bar"" style=""width: ' + larguraBarra + '%;""></div></div>';
                    htmlCard += '<div style=""display:grid; grid-template-columns:1fr 1fr; gap:5px; font-size:12px; color:var(--muted);"">';
                    htmlCard += '<div>Meta: <span style=""color:#fff"">' + fMeta + '</span></div>';
                    htmlCard += '<div>Recuperado: <span style=""color:var(--success)"">' + fRec + '</span></div>';
                    htmlCard += '<div>Falta: <span style=""color:var(--danger)"">' + fFal + '</span></div>';
                    htmlCard += '<div>Semana 1: <span style=""color:#fff"">' + fS1 + '</span></div>';
                    htmlCard += '</div></div>';
                    
                    cards.insertAdjacentHTML('beforeend', htmlCard);
                });
                
                if (selAnt) select.value = selAnt;
                document.getElementById('hora').innerText = new Date().toLocaleTimeString('pt-BR');
            } catch(e) { console.error('Erro geral:', e); }
            
            try {
                const res = await fetch(URL + 'ranking-top3');
                const r = await res.json();
                const div = document.getElementById('ranking');
                div.innerHTML = '';
                r.forEach(item => {
                    const fTot = item.totalRecuperado.toLocaleString('pt-BR', {style:'currency', currency:'BRL'});
                    div.innerHTML += '<div class=""ranking-item""><span>' + item.posicao + 'º - ' + item.nomeColaborador + '</span><b style=""color:var(--success)"">' + fTot + '</b></div>';
                });
            } catch(e) { console.error('Erro ranking:', e); }
        }

        async function enviar(e) {
            e.preventDefault();
            const id = document.getElementById('colab').value;
            const sem = document.getElementById('sem').value;
            const val = document.getElementById('val').value;
            try {
                const response = await fetch(URL + id + '/lançar-recuperacao?semana=' + sem, {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(parseFloat(val))
                });
                if (response.ok) {
                    document.getElementById('formLancar').reset();
                    atualizar();
                }
            } catch(e) { console.error('Erro lançamento:', e); }
        }

        document.addEventListener('DOMContentLoaded', () => { 
            atualizar(); 
            setInterval(atualizar, 30000); 
        });
    </script>
</body>
</html>";

File.WriteAllText("C:\\repos\\index.html", htmlContent);

app.UseCors("LiberarFront");
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
