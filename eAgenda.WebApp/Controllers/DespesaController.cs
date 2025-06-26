using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCategoria;
using eAgenda.Infraestrutura.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static eAgenda.WebApp.Models.FormularioDespesaViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("despesas")]
    public class DespesaController : Controller
    {
        private readonly ContextoDeDados contextoDeDados;
        private readonly IRepositorioDespesa repositorioDespesa;
        private readonly IRepositorioCategoria repositorioCategoria;

        public DespesaController(
            ContextoDeDados contextoDeDados, 
            IRepositorioDespesa repositorioDespesa,
            IRepositorioCategoria repositorioCategoria
            )
        {
            this.contextoDeDados = contextoDeDados;
            this.repositorioDespesa = repositorioDespesa;
            this.repositorioCategoria = repositorioCategoria;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registros = repositorioDespesa.SelecionarRegistros();

            var visualizarVM = new VisualizarDespesaViewModel(registros);
            
            foreach (var item in registros)
            {
                item.categoriasTitulo = new List<string>();
                foreach (var idCategoria in item.categorias)
                {
                    
                    item.categoriasTitulo.Add(repositorioCategoria.SelecionarRegistroPorId(idCategoria).Titulo);
                }
                repositorioDespesa.EditarRegistro(item.Id, item);
            }

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarDespesaViewModel();
            cadastrarVM.CategoriasDisponiveis = repositorioCategoria.SelecionarRegistros();
            return View(cadastrarVM);
            
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
        {
            var registros = repositorioDespesa.SelecionarRegistros() ?? new List<Despesa>();
            var categorias = repositorioCategoria.SelecionarRegistros();
            if (cadastrarVM.categorias == null || !cadastrarVM.categorias.Any())
            {
                ModelState.AddModelError("categorias", "Selecione pelo menos uma categoria.");
                cadastrarVM.CategoriasDisponiveis = repositorioCategoria.SelecionarRegistros();
            }

            if (!ModelState.IsValid)
            return View(cadastrarVM);

            foreach (var item in cadastrarVM.categorias)
            {

                cadastrarVM.categoriasTitulo.Add(repositorioCategoria.SelecionarRegistroPorId(item).Titulo);

            }
            var entidade = cadastrarVM.ParaEntidade();    
            repositorioDespesa.CadastrarRegistro(entidade);
            foreach (var item in entidade.categorias)
            {

                foreach (var item2 in categorias)
                {
                    Categoria c = item2;
                    if (c.idDespesas == null) c.idDespesas = new List<Guid>();
                    if (c.Id == item)
                    {
                        c.idDespesas.Add(entidade.Id);
                        repositorioCategoria.EditarRegistro(item, c);
                    }
                }

            }

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
            editarVM.CategoriasDisponiveis = repositorioCategoria.SelecionarRegistros();

            return View(editarVM);
        }
        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
        {
            var categorias = repositorioCategoria.SelecionarRegistros();
            
            foreach (var item in editarVM.categorias)
            {
                if(!editarVM.categoriasTitulo.Contains(repositorioCategoria.SelecionarRegistroPorId(item).Titulo))
                {
                    editarVM.categoriasTitulo.Add(repositorioCategoria.SelecionarRegistroPorId(item).Titulo);
                }
            }

            var entidadeEditada = editarVM.ParaEntidade();
            entidadeEditada.Id = id;
            repositorioDespesa.EditarRegistro(id, entidadeEditada);

                foreach (var item2 in categorias)
                {
                    Categoria categoria = item2;
                    if (categoria.idDespesas == null) categoria.idDespesas = new List<Guid>();
                    if (categoria.idDespesas.Contains(entidadeEditada.Id) && !(entidadeEditada.categorias.Contains(categoria.Id)))
                    {
                        categoria.idDespesas.Remove(entidadeEditada.Id);
                        categoria.despesas.Remove(entidadeEditada);
                        repositorioCategoria.EditarRegistro(categoria.Id, categoria);
                    }
                    else if (entidadeEditada.categorias.Contains(categoria.Id))
                    {
                       if(!categoria.idDespesas.Contains(entidadeEditada.Id))
                        {
                            categoria.idDespesas.Add(entidadeEditada.Id);
                            repositorioCategoria.EditarRegistro(categoria.Id, categoria);
                        }
                    }
                }
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
            
            foreach (var item in repositorioCategoria.SelecionarRegistros())
            {
                List<Guid> listaAuxiliar = new List<Guid>();
                Categoria c = item;
                foreach (var item2 in c.idDespesas)
                { 
                    if (item2 == id)
                    {
                    listaAuxiliar.Add(item2);
                    
                    }
                }
                foreach (var item2 in listaAuxiliar)
                {
                    c.idDespesas.Remove(item2);
                    repositorioCategoria.EditarRegistro(item.Id, c);
                }
            }
            repositorioDespesa.ExcluirRegistro(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
