using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.ModuloContato;
using eAgenda.WebApp.Extensions;
using eAgenda.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using static eAgenda.WebApp.Models.FormularioContatoViewModel;

namespace eAgenda.WebApp.Controllers
{
    [Route("contatos")]
    public class ContatoController : Controller
    {
        private readonly ContextoDeDados contextoDados;
        private readonly IRepositorioContato repositorioContato;

        public ContatoController()
        {
            contextoDados = new ContextoDeDados(true);
            repositorioContato = new RepositorioContatoEmArquivo(contextoDados);
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
                    break;
                }
            }

            foreach (var item in registros)
            {
                if (item.Telefone.Equals(cadastrarVM.Telefone))
                {
                    ModelState.AddModelError("CadastroUnico", "Já existe um Contato registrado com este Telefone.");
                }
            }

            if (!ModelState.IsValid)
                return View(cadastrarVM);

            var entidade = cadastrarVM.ParaEntidade();

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

            repositorioContato.EditarRegistro(id, entidadeEditada);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("excluir/{id:guid}")]
        public IActionResult Excluir(Guid id)
        {
            var registroSelecionado = repositorioContato.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirContatoViewModel(registroSelecionado.Id, registroSelecionado.Nome);

            return View(excluirVM);
        }

        [HttpPost("excluir/{id:guid}")]
        public IActionResult ExcluirConfirmado(Guid id)
        {
            //Substituição do código original que verifica compromissos em aberto a ser implementada posteriormente,
            //quando a funcionalidade de compromissos estiver completa.
            //------------------------------------------------------------------------------------------------------
            
            //var compromissosEmAberto = repositorioCompromisso.SelecionarCompromissosEmAbertoPorContato(id);

            //if (compromissosEmAberto.Any())
            //{
            //    TempData["MensagemErro"] = "Não é possível excluir o contato pois ele possui compromisso(s) em aberto.";
            //    return RedirectToAction("Index");
            //}

            //repositorioContato.Excluir(id);

            //TempData["MensagemSucesso"] = "Contato excluído com sucesso!";
            //return RedirectToAction("Index");

            repositorioContato.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

