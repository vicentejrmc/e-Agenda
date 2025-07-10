using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCategoria;
using eAgenda.Infraestrutura.ModuloCompromisso;
using eAgenda.Infraestrutura.ModuloDespesa;
using eAgenda.Infraestrutura.ModuloTarefa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using eAgenda.Infraestrutura.Orm.ModuloContato;
using eAgenda.WebApp.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace eAgenda.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ContextoDeDados>((_) => new ContextoDeDados(true));
            builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
            builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmArquivo>();
            builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmOrm>();
            builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmArquivo>();
            builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmArquivo>();

            builder.Services.AddSerilogConfig(builder.Logging);

            builder.Services.AddDbContext<eAgendaDbContext>(options =>
            {
                var connectionString = builder.Configuration["SQL_CONNECTION_STRING"];
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddEntityFrameworkConfig(builder.Configuration);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/erro");
            else
                app.UseDeveloperExceptionPage();

            app.UseAntiforgery();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapDefaultControllerRoute();
            app.Run();
        }
    }
}
