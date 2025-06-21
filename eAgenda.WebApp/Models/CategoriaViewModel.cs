using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.WebApp.Models
{
    public abstract class FormularioCategoriaViewModel
    {
        [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" precisa conter entre 2 e 100 caracteres.")]
        public string Titulo {  get; set; }
        public List<Guid>? despesas {  get; set; }
        public List<Despesa>? despesas1 { get; set; } = new List<Despesa>();
        public class CadastrarCategoriaViewModel : FormularioCategoriaViewModel
        {
            public CadastrarCategoriaViewModel() { }

            public CadastrarCategoriaViewModel(string titulo, List<Guid> despesas = null) : this()
            {
                Titulo = titulo;
                this.despesas = despesas;
            }
        }

        public class EditarCategoriaViewModel : FormularioCategoriaViewModel
        {
            public Guid Id { get; set; }

            public EditarCategoriaViewModel() { }

            public EditarCategoriaViewModel(Guid id, string titulo, List<Guid>? despesas = null) : this()
            {
                Id = id;
                Titulo = titulo;
                this.despesas = despesas;
            }


        }
        public class ExcluirCategoriaViewModel
        {
            public Guid Id { get; set; }
            public string Titulo { get; set; }

            public ExcluirCategoriaViewModel() { }

            public ExcluirCategoriaViewModel(Guid id, string Titulo, List<Guid>? despesas = null) : this()
            {
                Id = id;
                this.Titulo = Titulo;
            }   
        }
        public class VisualizarCategoriaViewModel
        {
            public List<DetalhesCategoriaViewModel> Registros { get; }

            public VisualizarCategoriaViewModel(List<Categoria> categorias)
            {
                Registros = new List<DetalhesCategoriaViewModel>();

                if (categorias != null)
                {
                    foreach (var c in categorias)
                    {
                        var detalhesVM = c.ParaDetalhesVM();
                        Registros.Add(detalhesVM);
                    }
                }
            }
        }
        public class DetalhesCategoriaViewModel
        {
            public Guid Id { get; }
            public string Titulo { get; }
            public List<Guid>? despesas { get; set; }
            public List<Despesa> despesas1 { get; set; }

            public DetalhesCategoriaViewModel(Guid id, string titulo, List<Despesa> despesas1, List<Guid>? despesas = null)
            {
                Id = id;
                Titulo = titulo;
                this.despesas = despesas;
                this.despesas1 = despesas1;
            }
        }
        public class SelecionarCategoriaViewModel
        {
            public Guid Id { get; set; }
            public string Titulo { get; }
            public List<Despesa>? despesas { get; set; }

            public SelecionarCategoriaViewModel(Guid id, string titulo, List<Despesa>? despesas = null)
            {
                Id = id;
                Titulo = titulo;
                this.despesas = despesas;
            }
        }

        public class VisualizarCategoriaDespesaViewModel
        {
            public DetalhesCategoriaViewModel Registro { get; set; }

            public VisualizarCategoriaDespesaViewModel(Categoria categoria)
            {    
                
                var detalhesVM = categoria.ParaDetalhesVM();
                Registro = detalhesVM;

                   
            }
        }
        public class ExcluirCategoriaDespesaViewModel
        {
            public Guid Id { get; set; }
            public string Titulo { get; set; }
            public Guid idDespesa { get; set; }

            public ExcluirCategoriaDespesaViewModel() { }

            public ExcluirCategoriaDespesaViewModel(Guid id, string Titulo, Guid idDespesa) : this()
            {
                Id = id;
                this.Titulo = Titulo;
                this.idDespesa = idDespesa;
            }
        }
    }
}
