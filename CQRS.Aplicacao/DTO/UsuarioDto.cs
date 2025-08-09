namespace CQRS.Aplicacao.DTO
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public static List<UsuarioDto> ObterUsuarios()
        {
            return new List<UsuarioDto>
            {
                new UsuarioDto { Id = 1, Nome = "Usuário 1" },
                new UsuarioDto { Id = 2, Nome = "Usuário 2" },
                new UsuarioDto { Id = 3, Nome = "Usuário 3" },
                new UsuarioDto { Id = 4, Nome = "Usuário 4" },
                new UsuarioDto { Id = 5, Nome = "Usuário 5" },
                new UsuarioDto { Id = 6, Nome = "Usuário 6" },
                new UsuarioDto { Id = 7, Nome = "Usuário 7" },
                new UsuarioDto { Id = 8, Nome = "Usuário 8" },
                new UsuarioDto { Id = 9, Nome = "Usuário 9" },
                new UsuarioDto { Id = 10, Nome = "Usuário 10" },
                new UsuarioDto { Id = 11, Nome = "Usuário 11" },
                new UsuarioDto { Id = 12, Nome = "Usuário 12" },
                new UsuarioDto { Id = 13, Nome = "Usuário 13" },
                new UsuarioDto { Id = 14, Nome = "Usuário 14" },
                new UsuarioDto { Id = 15, Nome = "Usuário 15" },
                new UsuarioDto { Id = 16, Nome = "Usuário 16" },
                new UsuarioDto { Id = 17, Nome = "Usuário 17" },
                new UsuarioDto { Id = 18, Nome = "Usuário 18" },
                new UsuarioDto { Id = 19, Nome = "Usuário 19" },
                new UsuarioDto { Id = 20, Nome = "Usuário 20" }
            };
        }
    }
}
