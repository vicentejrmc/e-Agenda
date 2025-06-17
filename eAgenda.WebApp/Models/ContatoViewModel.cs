using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.WebApp.Models
{
    public abstract class FormularioContatoViewModel
    {
        [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
        [Range(2, 100, ErrorMessage = "O campo \"Nome\" precisa conter um valor entre 2 e 100.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo \"Email\" é obrigatório.")]
        [RegularExpression(@"^[\w\.\-]+@([\w\-]+\.)+[a-zA-Z]{2,}$", ErrorMessage = "O campo \"Email\" não possui um formato válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo \"Telefone\" é obrigatório.")]
        [RegularExpression(@"^\d{2} \d{4,5}-\d{4}$", ErrorMessage = "O campo \"Telefone\" deve estar no formato \"xx xxxx-xxxx\" ou \"xx xxxxx-xxxx\".")]
        public string Telefone { get; set; }

        public string? Cargo { get; set; }
        public string? Empresa { get; set; }

        public class CadastrarContatoViewModel : FormularioContatoViewModel
        {
            public CadastrarContatoViewModel() { }

            public CadastrarContatoViewModel(string nome, string email, string telefone, string? cargo = null, string? empresa = null) : this()
            {
                Nome = nome;
                Email = email;
                Telefone = telefone;
                Cargo = cargo;
                Empresa = empresa;
            }
        }

        public class EditarContatoViewModel : FormularioContatoViewModel
        {
            public Guid Id { get; set; }

            public EditarContatoViewModel() { }

            public EditarContatoViewModel(Guid id, string nome, string email, string telefone, string? cargo = null, string? empresa = null) : this()
            {
                Id = id;
                Nome = nome;
                Email = email;
                Telefone = telefone;
                Cargo = cargo;
                Empresa = empresa;
            }
        }

        public class ExcluirContatoViewModel
        {
            public Guid Id { get; set; }
            public string Nome { get; set; }

            public ExcluirContatoViewModel() { }

            public ExcluirContatoViewModel(Guid id, string nome) : this()
            {
                Id = id;
                Nome = nome;
            }
        }

        public class VisualizarContatoViewModel
        {
            public List<DetalhesContatoViewModel> Registros { get; }

            public VisualizarContatoViewModel(List<Contato> contatos)
            {
                Registros = [];

                foreach (var m in contatos)
                {
                    var detalhesVM = m.ParaDetalhesVM();

                    Registros.Add(detalhesVM);
                }
            }
        }

        public class DetalhesContatoViewModel
        {
            public Guid Id { get; }
            public string Nome { get; }
            public string Email { get; }
            public string Telefone { get; }
            public string? Cargo { get; }
            public string? Empresa { get; }

            public DetalhesContatoViewModel(Guid id, string nome, string email, string telefone, string? cargo = null, string? empresa = null)
            {
                Id = id;
                Nome = nome;
                Email = email;
                Telefone = telefone;
                Cargo = cargo;
                Empresa = empresa;
            }
        }

        public class SelecionarContatoViewModel
        {
            public Guid Id { get; set; }
            public string Telefone { get; }
            public string Nome{ get; set; }

            public SelecionarContatoViewModel(Guid id, string telefone, string nome)
            {
                Id = id;
                Telefone = telefone;
                Nome = nome;
            }
        }
    }
}
