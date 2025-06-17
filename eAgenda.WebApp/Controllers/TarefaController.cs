using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloTarefa;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static eAgenda.WebApp.Models.FormularioTarefaViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("tarefas")]
    public class TarefaController : Controller
    {
        private readonly ContextoDeDados contextoDados;
        private readonly IRepositorioTarefa repositorioTarefa;

        public TarefaController()
        {
            contextoDados = new ContextoDeDados(true);
            repositorioTarefa = new RepositorioTarefaEmArquivo(contextoDados);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registros = repositorioTarefa.SelecionarRegistros();

            var visualizarVM = new VisualizarTarefaViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarTarefaViewModel();

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarTarefaViewModel cadastrarVM)
        {
            var registros = repositorioTarefa.SelecionarRegistros() ?? new List<Tarefa>();

            foreach (var item in registros)
            {
                if (item.Titulo.Equals(cadastrarVM.Titulo))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma Tarefa registrado com este Titulo.");
                    break;
                }
            }

            if (!ModelState.IsValid)
                return View(cadastrarVM);

            var entidade = cadastrarVM.ParaEntidade();

            repositorioTarefa.CadastrarRegistro(entidade);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioTarefa.SelecionarRegistroPorId(id);

            var editarVM = new EditarTarefaViewModel(
                id,
                registroSelecionado.Titulo,
                registroSelecionado.Prioridade,
                registroSelecionado.DataCriacao,
                registroSelecionado.StatusConcluida,
                registroSelecionado.PercentualConcluida,
                registroSelecionado.Items,
                registroSelecionado.DataConclusao
            );

            return View(editarVM);
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioTarefa.SelecionarRegistroPorId(id);

            if (registroSelecionado is not Tarefa tarefa)
            {
                return NotFound();
            }

            var excluirVM = new ExcluirTarefaViewModel(registroSelecionado.Id, registroSelecionado.Titulo);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioTarefa.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }
    }
}