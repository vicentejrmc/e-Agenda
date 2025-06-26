using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCompromisso;
using eAgenda.Infraestrutura.ModuloContato;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Controllers
{
    [Route("compromissos")]
    public class CompromissoController : Controller
    {
        private readonly ContextoDeDados contextoDeDados;
        private readonly IRepositorioCompromisso repositorioCompromisso;
        private readonly IRepositorioContato repositorioContato;

        public CompromissoController(
            ContextoDeDados contextoDeDados,
            IRepositorioCompromisso repositorioCompromisso,
            IRepositorioContato repositorioContato
            )
        {
            this.contextoDeDados = contextoDeDados;
            this.repositorioCompromisso = repositorioCompromisso;
            this.repositorioContato = repositorioContato;
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
            cadastrarVM.Contatos = contextoDeDados.Contatos ?? new List<Contato>();

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

            Contato? contatoSelecionado = null;
            if (cadastrarVM.ContatoId.HasValue)
                contatoSelecionado = repositorioContato.SelecionarRegistroPorId(cadastrarVM.ContatoId.Value);

            novoCompromisso = new Compromisso(
                cadastrarVM.Assunto,
                cadastrarVM.DataOcorrencia,
                cadastrarVM.HoraInicio,
                cadastrarVM.HoraTermino,
                cadastrarVM.TipoCompromisso,
                cadastrarVM.Local,
                cadastrarVM.Link,
                contatoSelecionado 
            );

            repositorioCompromisso.CadastrarRegistro(novoCompromisso);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var compromissoSelecionado = repositorioCompromisso.SelecionarRegistroPorId(id);

            var editarVM = new EditarCompromissoViewModel
            (
                compromissoSelecionado.Id,
                compromissoSelecionado.Assunto,
                compromissoSelecionado.DataOcorrencia,
                compromissoSelecionado.HoraInicio,
                compromissoSelecionado.HoraTermino,
                compromissoSelecionado.TipoCompromisso,
                compromissoSelecionado.Local,
                compromissoSelecionado.Link,
                compromissoSelecionado.Contato
            );

            editarVM.Contatos = contextoDeDados.Contatos ?? new List<Contato>();
            editarVM.ContatoId = compromissoSelecionado.Contato?.Id;

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        public IActionResult Editar(Guid id, EditarCompromissoViewModel editarVM)
        {
            var registros = repositorioCompromisso.SelecionarRegistros();

            var compromissoEditado = editarVM.ParaEntidade();

            Contato? contatoSelecionado = null;
            if (editarVM.ContatoId.HasValue)
                contatoSelecionado = repositorioContato.SelecionarRegistroPorId(editarVM.ContatoId.Value);

            compromissoEditado = new Compromisso(
                editarVM.Assunto,
                editarVM.DataOcorrencia,
                editarVM.HoraInicio,
                editarVM.HoraTermino,
                editarVM.TipoCompromisso,
                editarVM.Local,
                editarVM.Link,
                contatoSelecionado
            );

            repositorioCompromisso.EditarRegistro(id, compromissoEditado);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioCompromisso.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirCompromissoViewModel(registroSelecionado.Id, registroSelecionado.Assunto);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            repositorioCompromisso.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
