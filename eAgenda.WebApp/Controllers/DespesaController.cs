using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCategoria;
using eAgenda.Infraestrutura.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static eAgenda.WebApp.Models.FormularioDespesaViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("despesas")]
    public class DespesaController : Controller
    {
        private readonly ContextoDeDados contextoDados;
        private readonly IRepositorioDespesa repositorioDespesa;
        private readonly IRepositorioCategoria repositorioCategoria;

        public DespesaController()
        {
            contextoDados = new ContextoDeDados(true);
            repositorioDespesa = new RepositorioDespesaEmArquivo(contextoDados);
            repositorioCategoria = new RepositorioCategoriaEmArquivo(contextoDados);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registros = repositorioDespesa.SelecionarRegistros();

            var visualizarVM = new VisualizarDespesaViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarDespesaViewModel();
            var categorias = repositorioCategoria.SelecionarRegistros();
            cadastrarVM.CategoriasDisponiveis = repositorioCategoria.SelecionarRegistros();
            return View(cadastrarVM);
            
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
        {
            var registros = repositorioDespesa.SelecionarRegistros() ?? new List<Despesa>();

            if (!ModelState.IsValid)
                return View(cadastrarVM);

            foreach (var item in cadastrarVM.categorias)
            {
                cadastrarVM.categoriasTitulo.Add(repositorioCategoria.SelecionarRegistroPorId(item).Titulo);
            }
            var entidade = cadastrarVM.ParaEntidade();
            repositorioDespesa.CadastrarRegistro(entidade);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

            var editarVM = new EditarDespesaViewModel(
                id,
                registroSelecionado.descricao,
                registroSelecionado.dataOcorrencia,
                registroSelecionado.valor,
                registroSelecionado.formaDoPagamento,
                registroSelecionado.categorias,
                registroSelecionado.categoriasTitulo
            );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
        {
            var registros = repositorioDespesa.SelecionarRegistros();

            var entidadeEditada = editarVM.ParaEntidade();

            repositorioDespesa.EditarRegistro(id, entidadeEditada);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirDespesaViewModel(registroSelecionado.Id, registroSelecionado.descricao);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioDespesa.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
