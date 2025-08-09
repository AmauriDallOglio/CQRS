using CQRS.Aplicacao.DTO;
using CQRS.Aplicacao.Interface;
using CQRS.Aplicacao.Query;
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

        public UsuarioController(
            IQueryDispatcher queryDispatcher,
            ICommandDispatcher commandDispatcher,
            ILogger<UsuarioController> logger)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }


        [LoggerMessage(
            EventId = 1001,
            Level = LogLevel.Information,
            Message = "Buscando usuário com ID {UserId}"
        )]
        partial void LogBuscarUsuario(int? userId);


        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorId(int? id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Buscando usuário com ID {UserId}", id);

            var query = new ObterPorIdRequest { Id = id };
            var user = await _queryDispatcher.Dispatch<ObterPorIdRequest, ObterPorIdResponse>(query, cancellationToken);

            if (user == null)
            {
                _logger.LogWarning("Usuário com ID {UserId} não encontrado", id);
                return NotFound();
            }

            _logger.LogInformation("Usuário com ID {UserId} encontrado", id);
  
            return Ok(user);
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
