using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCategoria;
using eAgenda.Infraestrutura.ModuloDespesa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static eAgenda.WebApp.Models.FormularioCategoriaViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("categorias")]
    public class CategoriaController : Controller
    {
        private readonly eAgendaDbContext contexto;
        private readonly IRepositorioCategoria repositorioCategoria;
        private readonly IRepositorioDespesa repositorioDespesa;

        public CategoriaController(
            eAgendaDbContext contexto,
            IRepositorioCategoria repositorioCategoria,
            IRepositorioDespesa repositorioDespesa
            )
        {
            this.contexto = contexto;
            this.repositorioCategoria = repositorioCategoria;
            this.repositorioDespesa = repositorioDespesa;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registros = repositorioCategoria.SelecionarRegistros();

            var visualizarVM = new VisualizarCategoriaViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarCategoriaViewModel();

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarCategoriaViewModel cadastrarVM)
        {
            var registros = repositorioCategoria.SelecionarRegistros() ?? new List<Categoria>();

            foreach (var item in registros)
            {
                if (item.Titulo.Equals(cadastrarVM.Titulo))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma Categoria registrada com este Título.");
                    break;
                }
            }
            if (!ModelState.IsValid)
                return View(cadastrarVM);

            var entidade = cadastrarVM.ParaEntidade();
            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioCategoria.CadastrarRegistro(entidade);
                contexto.SaveChanges();
                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

            var editarVM = new EditarCategoriaViewModel(
                id,
                registroSelecionado.Titulo
            );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarCategoriaViewModel editarVM)
        {
            var registros = repositorioCategoria.SelecionarRegistros();

            foreach (var item in registros)
            {
                if (item.Titulo.Equals(editarVM.Titulo) && !item.Id.Equals(id))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma Categoria registrada com este Título.");
                    break;
                }
            }

            var entidadeEditada = editarVM.ParaEntidade();
            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioCategoria.EditarRegistro(id, entidadeEditada);
                contexto.SaveChanges();
                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirCategoriaViewModel(
                id,
                registroSelecionado.Titulo
                );

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id, ExcluirCategoriaViewModel excluirVM)
        {
            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioCategoria.ExcluirRegistro(id);
                contexto.SaveChanges();
                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet("detalhes/{id:guid}")]
        public IActionResult Detalhes(Guid id)
        {
            var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

            var detalhesVM = new DetalhesCategoriaViewModel(
                registroSelecionado.Id,
                registroSelecionado.Titulo,
                registroSelecionado.Despesas
            );

            return View(detalhesVM);
        }
    }
}
