using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Infraestrutura.ModuloCategoria;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc;
using static eAgenda.WebApp.Models.FormularioCategoriaViewModel;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.ModuloContato;
using static eAgenda.WebApp.Models.FormularioContatoViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("categorias")]
    public class CategoriaController : Controller
    {
        private readonly ContextoDeDados contextoDados;
        private readonly IRepositorioCategoria repositorioCategoria;

        public CategoriaController()
        {
            contextoDados = new ContextoDeDados(true);
            repositorioCategoria = new RepositorioCategoriaEmArquivo(contextoDados);
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

            repositorioCategoria.CadastrarRegistro(entidade);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

            var editarVM = new EditarCategoriaViewModel(
                id,
                registroSelecionado.Titulo,
                registroSelecionado.despesas
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
                if (!item.Id.Equals(id) && item.Titulo.Equals(editarVM.Titulo))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe uma categoria registrada com este título.");
                    break;
                }
            }
                var entidadeEditada = editarVM.ParaEntidade();

            repositorioCategoria.EditarRegistro(id, entidadeEditada);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioCategoria.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirContatoViewModel(registroSelecionado.Id, registroSelecionado.Titulo);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            var registros = repositorioCategoria.SelecionarRegistros();

            foreach (var item in registros)
            {
                if (item.Id.Equals(id) && item.despesas.Count > 0)
                {
                    ModelState.AddModelError("ExclusaoProibida", "Não é possível excluir uma categoria que possui despesas associadas");
                    break;
                }
            }
            repositorioCategoria.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }
    }
}


}
