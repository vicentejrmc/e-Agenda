using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCategoria;
using eAgenda.Infraestrutura.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using static eAgenda.WebApp.Models.FormularioDespesaViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("despesas")]
    public class DespesaController : Controller
    {
        private readonly IRepositorioDespesa repositorioDespesa;
        private readonly IRepositorioCategoria repositorioCategoria;

        public DespesaController(
            IRepositorioDespesa repositorioDespesa,
            IRepositorioCategoria repositorioCategoria
            )
        {
            this.repositorioDespesa = repositorioDespesa;
            this.repositorioCategoria = repositorioCategoria;
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
            var categoriasDisponiveis = repositorioCategoria.SelecionarRegistros();
            var cadastrarVM = new CadastrarDespesaViewModel(categoriasDisponiveis);
            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
        {
            var categoriasDisponiveis = repositorioCategoria.SelecionarRegistros();

            if (!ModelState.IsValid)
            {
                foreach (var cd in categoriasDisponiveis)
                {
                    var selecionarVM = new SelectListItem(cd.Titulo, cd.Id.ToString());

                    cadastrarVM.CategoriasDisponiveis?.Add(selecionarVM);
                }

                return View(cadastrarVM);
            }

            var despesa = cadastrarVM.ParaEntidade();

            var categoriasSelecionadas = cadastrarVM.CategoriaSelecionadas;

            if (categoriasSelecionadas is not null)
            {
                foreach (var cs in categoriasSelecionadas)
                {
                    foreach (var cd in categoriasDisponiveis)
                    {
                        if (cs.Equals(cd.Id))
                        {
                            despesa.RegistarCategoria(cd);
                            break;
                        }
                    }
                }
            }

            repositorioDespesa.CadastrarRegistro(despesa);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);
            var editarVM = new EditarDespesaViewModel(
                id,
                registroSelecionado.Descricao,
                registroSelecionado.DataOcorrencia,
                registroSelecionado.Valor,
                registroSelecionado.FormaDoPagamento,
                registroSelecionado.Categorias,
                registroSelecionado.CategoriasTitulo
            );
            editarVM.CategoriasDisponiveis = repositorioCategoria.SelecionarRegistros();

            return View(editarVM);
        }
        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
        {
            var categorias = repositorioCategoria.SelecionarRegistros();
            
            foreach (var item in editarVM.CategoriaSelecionadas)
            {
                if(!editarVM.CategoriasDisponiveis.Contains(repositorioCategoria.SelecionarRegistroPorId(item).Titulo))
                {
                    editarVM.CategoriasDisponiveis.Add(repositorioCategoria.SelecionarRegistroPorId(item).Titulo);
                }
            }

            var entidadeEditada = editarVM.ParaEntidade();
            entidadeEditada.Id = id;
            repositorioDespesa.EditarRegistro(id, entidadeEditada);

                foreach (var item2 in categorias)
                {
                    Categoria categoria = item2;
                    if (categoria.idDespesas == null) categoria.idDespesas = new List<Guid>();
                    if (categoria.idDespesas.Contains(entidadeEditada.Id) && !(entidadeEditada.Categorias.Contains(categoria.Id)))
                    {
                        categoria.idDespesas.Remove(entidadeEditada.Id);
                        categoria.despesas.Remove(entidadeEditada);
                        repositorioCategoria.EditarRegistro(categoria.Id, categoria);
                    }
                    else if (entidadeEditada.Categorias.Contains(categoria.Id))
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

            var excluirVM = new ExcluirDespesaViewModel(registroSelecionado.Id, registroSelecionado.Descricao);

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
