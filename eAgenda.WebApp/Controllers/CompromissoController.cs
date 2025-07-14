using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Controllers
{
    [Route("compromissos")]
    public class CompromissoController : Controller
    {
        private readonly eAgendaDbContext contexto;
        private readonly IRepositorioCompromisso repositorioCompromisso;
        private readonly IRepositorioContato repositorioContato;

        public CompromissoController(
            eAgendaDbContext contextoDeDados,
            IRepositorioCompromisso repositorioCompromisso,
            IRepositorioContato repositorioContato
            )
        {
            this.contexto = contextoDeDados;
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

            cadastrarVM.Contatos = contexto.Contatos.ToList() ?? new List<Contato>();


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

            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioCompromisso.CadastrarRegistro(novoCompromisso);
                contexto.SaveChanges();
                transacao.Commit();
                // Commit da transação para salvar as alterações no banco de dados
            }
            catch
            {
                transacao.Rollback(); // Em caso de erro, desfaz as alterações
                throw;
            }

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

            editarVM.Contatos = contexto.Contatos.ToList() ?? new List<Contato>();
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

            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioCompromisso.EditarRegistro(id, compromissoEditado);
                contexto.SaveChanges();
                transacao.Commit();
            }
            catch (Exception)
            {
                transacao.Rollback();
                throw;
            }

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
            var transacao = contexto.Database.BeginTransaction();
            try
            {
                repositorioCompromisso.ExcluirRegistro(id);
                contexto.SaveChanges();
                transacao.Commit();
            }
            catch (Exception)
            {
                // Em caso de erro, desfaz as alterações
                transacao.Rollback();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
