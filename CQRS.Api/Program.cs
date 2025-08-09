using CQRS.Api.Configuracao;
using CQRS.Aplicacao.Command;
using CQRS.Aplicacao.Interface;
using CQRS.Aplicacao.Query;
using Serilog;
using Serilog.Events;


namespace CQRS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
 
 
            // Dispatchers
            builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

 
            builder.RegistrarCommandHandlers();
            builder.RegistrarQueryHandlers();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.DocumentFilter<RemoveObsoleteOperationsFilter>();
            //    c.DocumentFilter<AddGlobalMetadataDocumentFilter>();
            //    c.DocumentFilter<AddCustomHeaderToResponsesDocumentFilter>();
            //});




            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose() // captura todos os níveis

                // Apenas logs de Trace (nível mais baixo)
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Verbose)
                    .WriteTo.File(
                        path: @"C:\Amauri\GitHub\logs\trace-.log",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30))

                // Logs Debug
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)
                    .WriteTo.File(
                        path: @"C:\Amauri\GitHub\logs\debug-.log",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30))

                // Logs Information
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
                    .WriteTo.File(
                        path: @"C:\Amauri\GitHub\logs\information-.log",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30))

                // Logs Warning
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Warning)
                    .WriteTo.File(
                        path: @"C:\Amauri\GitHub\logs\warning-.log",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30))

                // Logs Error
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)
                    .WriteTo.File(
                        path: @"C:\Amauri\GitHub\logs\error-.log",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30))

                // Logs Critical
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Fatal)
                    .WriteTo.File(
                        path: @"C:\Amauri\GitHub\logs\critical-.log",
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30))

                // Console para todos os logs
                .WriteTo.Console()
                .CreateLogger();
            // No Program.cs:
            builder.Host.UseSerilog();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.MapGet("/", (ILogger<Program> logger) =>
            {
                logger.LogInformation("Teste de log no arquivo");
                return "Log gravado!";
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();




        }
    }
}
