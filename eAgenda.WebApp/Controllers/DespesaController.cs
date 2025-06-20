using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloTarefa;
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
            cadastrarVM.CategoriasDisponiveis = repositorioCategoria.SelecionarRegistros();
            return View(cadastrarVM);
            
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarDespesaViewModel cadastrarVM)
        {
            var registros = repositorioDespesa.SelecionarRegistros() ?? new List<Despesa>();
            var categorias = repositorioCategoria.SelecionarRegistros();
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
                    if (c.despesas == null) c.despesas = new List<Guid>();
                    if (c.Id == item)
                    {
                        c.despesas.Add(entidade.Id);
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
            editarVM.categoriasTitulo = new List<string>();
            var categorias = repositorioCategoria.SelecionarRegistros();
            foreach (var item in editarVM.categorias)
            {
                editarVM.categoriasTitulo.Add(repositorioCategoria.SelecionarRegistroPorId(item).Titulo);
            }

            var entidadeEditada = editarVM.ParaEntidade();
            repositorioDespesa.EditarRegistro(id, entidadeEditada);
            foreach (var item in entidadeEditada.categorias)
            {

                foreach (var item2 in categorias)
                {
                    Categoria c = item2;
                    bool jaTem = false;
                    if (c.despesas == null) c.despesas = new List<Guid>();
                    if (c.Id == item)
                    {
                        foreach(var item3 in c.despesas)
                        {
                            if(!(item3 == entidadeEditada.Id))
                            {
                                 jaTem = true;
                            }
                        }
                        if(!jaTem)
                        {
                            c.despesas.Add(entidadeEditada.Id);
                            repositorioCategoria.EditarRegistro(item, c);
                        }       
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
                foreach (var item2 in c.despesas)
                { 
                    if (item2 == id)
                    {
                    listaAuxiliar.Add(item2);
                    
                    }
                }
                foreach (var item2 in listaAuxiliar)
                {
                    c.despesas.Remove(item2);
                    repositorioCategoria.EditarRegistro(item.Id, c);
                }
            }
            repositorioDespesa.ExcluirRegistro(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
