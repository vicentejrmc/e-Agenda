using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCompromisso;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

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
            cadastrarVM.Contatos = contextoDados.Contatos ?? new List<Contato>();

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarCompromissoViewModel cadastrarVM)
        {
            var registros = repositorioCompromisso.SelecionarRegistros() ?? new List<Compromisso>();

            var novoCompromisso = cadastrarVM.ParaEntidade();

            bool conflito = false;

            foreach (var c in registros)
            {
                if (c.DataOcorrencia.Date == novoCompromisso.DataOcorrencia.Date)
                {
                    if (novoCompromisso.HoraInicio < c.HoraTermino && novoCompromisso.HoraTermino > c.HoraInicio)
                    {
                        conflito = true;
                        break;
                    }
                }
            }

            if (conflito)
            {
                ModelState.AddModelError("ConflitoHorario", "Já existe um compromisso neste horário.");
                return View(cadastrarVM);
            }

            repositorioCompromisso.CadastrarRegistro(novoCompromisso);

            return RedirectToAction(nameof(Index));
        }
    }
}
