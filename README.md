Descrição do Projeto - CQRS API com Lista de Usuários em Memória

Este projeto é uma implementação simples de uma API REST utilizando o padrão CQRS (Command Query Responsibility Segregation) em .NET, com exemplos práticos de uso de injection de dependências, handlers para queries e commands, e logs estruturados.

Funcionalidades e Estrutura
1. Controller UsuarioController
Exemplo completo de um controller ASP.NET Core que usa os serviços injetados:
IQueryDispatcher para consultas (queries).
ICommandDispatcher para comandos (commands).
ILogger<UsuarioController> para logging estruturado.
Utiliza o recurso LoggerMessage do .NET para criar logs performáticos com mensagens.
Possui endpoints para:
Buscar usuário por ID via IQueryDispatcher.
Retornar uma lista fixa (em memória) de 20 usuários para teste e simulação.

2. DTO UsuarioDto
Define um modelo simples com Id e Nome.
Possui um método estático ObterUsuarios que retorna uma lista pré-definida de 20 usuários em memória.
Essa lista pode ser usada para simular um banco de dados ou servir como fonte para consultas rápidas.

3. Query Handler ObterPorIdQuery
Implementa o IQueryHandler para processar a query de buscar usuário por ID.
O método Handle busca na lista estática em memória os usuários definidos no DTO.
Respeita a assinatura assíncrona e simula uma operação real, retornando o nome do usuário encontrado ou null se não existir.

4. Logging
Demonstra o uso do logger padrão do ASP.NET Core e também o uso de source generators com [LoggerMessage] para criar métodos de log otimizados.
Inclui logs informativos sobre tentativas de busca e resultados (sucesso ou usuário não encontrado).

5. Registro e Injeção de Dependências
Mostra como injetar serviços no controller via construtor, incluindo os dispatchers e o logger.
Indica como registrar handlers de comandos e queries dinamicamente via reflexão (RegisterCommandHandlers e RegisterQueryHandlers).
Demonstra a importância da injeção para manter o código desacoplado, testável e organizado.
