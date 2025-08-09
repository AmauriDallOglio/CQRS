using CQRS.Aplicacao.DTO;
using CQRS.Aplicacao.Interface;
using CQRS.Aplicacao.Query;
using CQRS.Aplicacao.Util;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public partial class UsuarioController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher,  ILogger<UsuarioController> logger)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _logger = logger;
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

    }
}
