using CQRS.Api.Configuracao;
using CQRS.Aplicacao.Command;
using CQRS.Aplicacao.Interface;
using CQRS.Aplicacao.Query;
using Serilog;


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


            // Configura o Serilog
            SerilogConfig.ConfigurarSerilog();
            builder.Host.UseSerilog();

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
