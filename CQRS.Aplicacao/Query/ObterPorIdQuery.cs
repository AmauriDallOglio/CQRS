using CQRS.Aplicacao.DTO;
using CQRS.Aplicacao.Interface;

namespace CQRS.Aplicacao.Query
{
    public class ObterPorIdRequest
    {
        public int? Id { get; set; }
    }

    public class ObterPorIdResponse
    {
        public string Nome { get; set; }
    }


    public class ObterPorIdQuery : IQueryHandler<ObterPorIdRequest, ObterPorIdResponse>
    {
        public ObterPorIdQuery() { }

        public async Task<ObterPorIdResponse?> Handle(ObterPorIdRequest query, CancellationToken cancellationToken)
        {
            // Simula uma operação assíncrona
            await Task.Delay(10, cancellationToken);

            if (!query.Id.HasValue)
                return null;

            var usuarios = UsuarioDto.ObterUsuarios();

            var usuario = usuarios.FirstOrDefault(u => u.Id == query.Id.Value);

            if (usuario == null)
                return null;

            return new ObterPorIdResponse
            {
                Nome = usuario.Nome
            };
        }
    }
}
