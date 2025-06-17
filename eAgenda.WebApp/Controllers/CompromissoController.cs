using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCompromisso;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static eAgenda.WebApp.Models.FormularioContatoViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("compromissos")]
    public class CompromissoController : Controller
    {
        private readonly ContextoDeDados contextoDados;
        private readonly IRepositorioCompromisso repositorioCompromisso;

        public CompromissoController()
        {
            contextoDados = new ContextoDeDados(true);
            repositorioCompromisso = new RepositorioCompromissoEmArquivo(contextoDados);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registros = repositorioCompromisso.SelecionarRegistros();

            var visualizarVM = new VisualizarCompromissoViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarCompromissoViewModel();
            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarCompromissoViewModel cadastrarVM)
        {

            contextoDados.Salvar();
            return RedirectToAction("Index");
        }
    }
}
