using CQRS.Aplicacao.DTO;
using CQRS.Aplicacao.Interface;
using CQRS.Aplicacao.Query;
using CQRS.Aplicacao.Util;
using Microsoft.AspNetCore.Mvc;
using IronPdf;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace CQRS.Api.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public partial class UsuarioController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger<UsuarioController> _logger;
        private readonly GeradorPdfDinkToPdf _GeradorPdfDinkToPdf;
        private readonly IConverter _converter;

        public UsuarioController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher,  ILogger<UsuarioController> logger, GeradorPdfDinkToPdf geradorPdfDinkToPdf, IConverter converter)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _logger = logger;
            _GeradorPdfDinkToPdf = geradorPdfDinkToPdf;
            _converter = converter;
        }


        [LoggerMessage(EventId = 1001, Level = LogLevel.Information, Message = "Buscando usuário com id {id}")]
        partial void LogBuscarUsuario(int? id);


        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int? id, CancellationToken cancellationToken)
        {
            Logger.Info("Iniciando execução...");
            _logger.LogTrace("Iniciando busca de usuário. Parâmetro id recebido: {id}", id);
            _logger.LogDebug("Preparando consulta para o id {id} no método {Method}", id, nameof(ObterPorId));

            _logger.LogInformation("Iniciando busca de usuário...");
            _logger.LogInformation("Buscando usuário com id {id}!", id);
            LogBuscarUsuario(id);

            var query = new ObterPorIdRequest { Id = id };
            ObterPorIdResponse usuario = await _queryDispatcher.Dispatch<ObterPorIdRequest, ObterPorIdResponse>(query, cancellationToken);


            if (usuario == null)
            {
                _logger.LogWarning("Nenhum usuário encontrado com id {id}", id);
                _logger.LogError("Erro: Falha ao localizar o usuário no banco de dados para o id {id}", id);

                // Exemplo de uso do Critical (falha grave)
                if (id > 20) // Simulação de erro crítico
                {
                    _logger.LogCritical("Erro crítico: Usuário com id {id} causou falha no sistema", id);
                }
                Logger.Error("Processo finalizado!");
                return NotFound();
            }



            _logger.LogInformation("Usuário {nome} encontrado!", usuario.Nome);
            _logger.LogInformation("Processo finalizado com sucesso!");
            Logger.Success("Processo finalizado com sucesso!");
            return Ok(usuario);
        }

        // Novo endpoint para retornar a lista fixa em memória
        [HttpGet]
        public IActionResult ObterTodos()
        {
            var usuarios = UsuarioDto.ObterUsuarios();
            return Ok(usuarios);
        }


        [HttpGet("GeradorPdfDinkToPdfUsuarios")]
        public IActionResult GerarPdfUsuarios()
        {
            var usuarios = UsuarioDto.ObterUsuarios();

            var html = "<h1>Relatório de Usuários</h1><ul>";
            foreach (var usuario in usuarios)
            {
                html += $"<li>{usuario.Nome} (ID: {usuario.Id})</li>";
            }
            html += "</ul>";

            // Criar instância do renderizador
            var renderer = new ChromePdfRenderer();

            // Gerar o PDF a partir do HTML
            var pdf = renderer.RenderHtmlAsPdf(html);

            // Retornar o PDF para o cliente
            return File(pdf.BinaryData, "application/pdf", "GeradorPdfDinkToPdfUsuarios.pdf");
        }


        [HttpGet("GeradorPdfDinkToPdf")]
        public IActionResult GeradorPdfDinkToPdf()
        {
            var html = @"
                <html>
                    <head>
                        <meta charset='utf-8'>
                        <style>
                            body { font-family: Arial; }
                            h1 { color: navy; }
                        </style>
                    </head>
                    <body>
                        <h1>Relatório de Teste</h1>
                        <p>Gerado com DinkToPdf no .NET</p>
                    </body>
                </html>";

            var pdfBytes = _GeradorPdfDinkToPdf.GerarPdf(html);

            return File(pdfBytes, "application/pdf", "GeradorPdfDinkToPdf.pdf");
        }



        [HttpGet("DinkToPdfUsuarios")]
        public IActionResult DinkToPdfUsuarios()
        {
            var usuarios = UsuarioDto.ObterUsuarios();

            var html = "<h1>Relatório de Usuários</h1><ul>";
            foreach (var usuario in usuarios)
            {
                html += $"<li>{usuario.Nome} (ID: {usuario.Id})</li>";
            }
            html += "</ul>";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
                Margins = new MarginSettings { Top = 10 }
            },
                Objects = {  new ObjectSettings() {
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            var pdf = _converter.Convert(doc);

            return File(pdf, "application/pdf", "DinkToPdfUsuarios.pdf");
        }


       
    }
}
