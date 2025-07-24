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
            var categoriaDisponiveis = repositorioCategoria.SelecionarRegistros();
            var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

            if (registroSelecionado == null)
                return RedirectToAction(nameof(Index));

            var editarVM = new EditarDespesaViewModel(
                id,
                registroSelecionado.Descricao,
                registroSelecionado.DataOcorrencia,
                registroSelecionado.Valor,
                registroSelecionado.FormaDoPagamento,
                registroSelecionado.Categorias,
                categoriaDisponiveis
            );

            return View(editarVM);
        }
        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarDespesaViewModel editarVM)
        {
            var categoriasDisponiveis = repositorioCategoria.SelecionarRegistros();

            if (!ModelState.IsValid)
            {
                foreach (var c in categoriasDisponiveis)
                {
                    var selecionarVM = new SelectListItem(c.Titulo, c.Id.ToString());
                    editarVM.CategoriasDisponiveis?.Add(selecionarVM);
                }
                return View(editarVM);
            }

            var despesaEditada = editarVM.ParaEntidade();
            var categoriasSelecionadas = editarVM.CategoriaSelecionadas;

            if (categoriasSelecionadas is not null)
            {
                foreach (var idSelecionado in categoriasSelecionadas)
                {
                    foreach (var categoriaDisponivel in categoriasDisponiveis)
                    {
                        if (categoriaDisponivel.Id.Equals(idSelecionado))
                        {
                            despesaEditada.RegistarCategoria(categoriaDisponivel); break;
                        }
                    }
                }
            }

            repositorioDespesa.EditarRegistro(id, despesaEditada);

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
            var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

            if (registroSelecionado == null)
                return RedirectToAction(nameof(Index));

            foreach (var item in registroSelecionado.Categorias.ToList())
                registroSelecionado.RemoverCategoria(item);

            repositorioDespesa.ExcluirRegistro(id);
            return RedirectToAction(nameof(Index));
        }
    }

        public IActionResult Detalhes(Guid id)
        {
            var registroSelecionado = repositorioDespesa.SelecionarRegistroPorId(id);

            if (registroSelecionado is null)
                return RedirectToAction(nameof(Index));

            var detalhesVM = new DetalhesDespesaViewModel(
                id,
                registroSelecionado.Descricao,
                registroSelecionado.DataOcorrencia,
                registroSelecionado.Valor,
                registroSelecionado.FormaDoPagamento,
                registroSelecionado.Categorias
            );

            return View(detalhesVM);
        }
    }
