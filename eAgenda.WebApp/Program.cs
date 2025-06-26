using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCategoria;
using eAgenda.Infraestrutura.ModuloCompromisso;
using eAgenda.Infraestrutura.ModuloContato;
using eAgenda.Infraestrutura.ModuloDespesa;
using eAgenda.Infraestrutura.ModuloTarefa;
using eAgenda.WebApp.DependencyInjection;


namespace eAgenda.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            //inje��o de dependencias
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ContextoDeDados>((_) => new ContextoDeDados(true));
            builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoriaEmArquivo>();
            builder.Services.AddScoped<IRepositorioCompromisso, RepositorioCompromissoEmArquivo>();
            builder.Services.AddScoped<IRepositorioContato, RepositorioContatoEmArquivo>();
            builder.Services.AddScoped<IRepositorioDespesa, RepositorioDespesaEmArquivo>();
            builder.Services.AddScoped<IRepositorioTarefa, RepositorioTarefaEmArquivo>();
            builder.Services.AddSerilogConfig(builder.Logging);
            
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
