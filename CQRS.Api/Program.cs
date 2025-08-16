using CQRS.Api.Configuracao;
using CQRS.Aplicacao.Command;
using CQRS.Aplicacao.Interface;
using CQRS.Aplicacao.Query;
using CQRS.Aplicacao.Util;
using DinkToPdf;
using DinkToPdf.Contracts;
using Serilog;

namespace CQRS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
 
 
            // Dispatchers
            //builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            //builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            builder.Services.Scan(
                scan => scan
                    .FromAssemblyOf<CommandDispatcher>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

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


            // Configura o Serilog
            SerilogConfig.ConfigurarSerilog();
            builder.Host.UseSerilog();


            // Registrar o conversor do DinkToPdf

            // Caminho para a pasta onde est� o libwkhtmltox.dll
            var context = new CarregarDLL();
            context.Carregar(Path.Combine("C:\\Amauri\\GitHub\\CQRS\\CQRS\\CQRS.Aplicacao", "DinkToPdfLib", "libwkhtmltox.dll"));

            // Registrar DinkToPdf
            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            builder.Services.AddScoped<GeradorPdfDinkToPdf>();


            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
