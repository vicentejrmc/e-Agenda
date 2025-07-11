using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloCompromisso;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static eAgenda.WebApp.Models.FormularioContatoViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("contatos")]
    public class ContatoController : Controller
    {
        private readonly eAgendaDbContext contexto;
        private readonly IRepositorioContato repositorioContato;
        private readonly IRepositorioCompromisso repoisitorioCompromisso;

        public ContatoController(
            IRepositorioContato repositorioContato,
            IRepositorioCompromisso repositorioCompromisso,
            eAgendaDbContext contexto)
        {
            this.repositorioContato = repositorioContato;
            this.repoisitorioCompromisso = repositorioCompromisso;
            this.contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var registros = repositorioContato.SelecionarRegistros();

            var visualizarVM = new VisualizarContatoViewModel(registros);

            return View(visualizarVM);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var cadastrarVM = new CadastrarContatoViewModel();

            return View(cadastrarVM);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(CadastrarContatoViewModel cadastrarVM)
        {
            var registros = repositorioContato.SelecionarRegistros() ?? new List<Contato>();

            foreach (var item in registros)
            {
                if (item.Email.Equals(cadastrarVM.Email))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um Contato registrado com este Email.");
                }

                if (item.Telefone.Equals(cadastrarVM.Telefone))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um Contato registrado com este Telefone.");
                }
            }

            if (!ModelState.IsValid)
                return View(cadastrarVM);

            var entidade = cadastrarVM.ParaEntidade();

            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioContato.CadastrarRegistro(entidade);
                contexto.SaveChanges();
                transacao.Commit();
                // Commit da transação para salvar as alterações no banco de dados
            }
            catch
            {
                transacao.Rollback(); // Em caso de erro, desfaz as alterações
                throw;
            }

            repositorioContato.CadastrarRegistro(entidade);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("editar/{id:guid}")]
        public IActionResult Editar(Guid id)
        {
            var registroSelecionado = repositorioContato.SelecionarRegistroPorId(id);

            var editarVM = new EditarContatoViewModel(
                id,
                registroSelecionado.Nome,
                registroSelecionado.Email,
                registroSelecionado.Telefone,
                registroSelecionado.Cargo,
                registroSelecionado.Empresa
            );

            return View(editarVM);
        }

        [HttpPost("editar/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Guid id, EditarContatoViewModel editarVM)
        {
            var registros = repositorioContato.SelecionarRegistros();

            var entidadeEditada = editarVM.ParaEntidade();

            var transacao = contexto.Database.BeginTransaction();

            try
            {
                repositorioContato.EditarRegistro(id, entidadeEditada);
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

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioContato.SelecionarRegistroPorId(id);

            if (registroSelecionado is null) return View("Index");
            var excluirVM = new ExcluirContatoViewModel(registroSelecionado.Id, registroSelecionado.Nome);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {

            foreach (var compromisso in repoisitorioCompromisso.SelecionarRegistros())
            {
                if (compromisso.Contato != null &&
                    compromisso.Contato.Id == id &&
                    compromisso.DataOcorrencia >= DateTime.Today)
                {
                    TempData["MensagemErro"] = "Contato não pode ser excluído pois possui compromisso em aberto!";
                    return RedirectToAction(nameof(Excluir), new { id });
                }
            }

            var transacao = contexto.Database.BeginTransaction();
            try
            {
                repositorioContato.ExcluirRegistro(id);
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

