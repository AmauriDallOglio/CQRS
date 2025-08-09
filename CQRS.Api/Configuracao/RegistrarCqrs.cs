using CQRS.Aplicacao.Interface;

namespace CQRS.Api.Configuracao
{
    public static class RegistrarCqrs
    {
        public static void RegistrarQueryHandlers(this WebApplicationBuilder builder)
        {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && !p.IsAbstract &&
                            p.GetInterfaces().Any(i => i.IsGenericType &&
                                                       i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))))
            {
                foreach (var interfaceType in type.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                {
                    builder.Services.AddScoped(interfaceType, type);
                }
            }
        }

        public static void RegistrarCommandHandlers(this WebApplicationBuilder builder)
        {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && !p.IsAbstract &&
                            p.GetInterfaces().Any(i => i.IsGenericType &&
                                                       i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))))
            {
                foreach (var interfaceType in type.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
                {
                    builder.Services.AddScoped(interfaceType, type);
                }
            }
        }
    }

}
