using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.WebApp.Models
{
    public  class FormularioCategoriaViewModel
    {
        [Required(ErrorMessage = "O campo \"Título\" é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Título\" precisa conter entre 2 e 100 caracteres.")]
        public string Titulo {  get; set; }

        public class CadastrarCategoriaViewModel : FormularioCategoriaViewModel
        {
            public CadastrarCategoriaViewModel() { }

            public CadastrarCategoriaViewModel(string titulo, List<Guid> despesas = null) : this()
            {
                Titulo = titulo;
            }
        }

        public class EditarCategoriaViewModel : FormularioCategoriaViewModel
        {
            public Guid Id { get; set; }

            public EditarCategoriaViewModel() { }

            public EditarCategoriaViewModel(Guid id, string titulo) : this()
            {
                Id = id;
                Titulo = titulo;
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

                    foreach (var c in categorias)
                        Registros.Add(c.ParaDetalhesVM());
            }
        }
        public class DetalhesCategoriaViewModel
        {
            public Guid Id { get; }
            public string Titulo { get; }       
            public List<DetalhesDespesaViewModel> Despesas { get; set; }

            public double DespesaTotal { get; set; }

            public DetalhesCategoriaViewModel(Guid id, string titulo, List<Despesa> despesas)
            {
                Id = id;
                Titulo = titulo;

                Despesas = new List<DetalhesDespesaViewModel>();

                foreach (var d in despesas)
                {
                    DespesaTotal += d.Valor;
                    
                    var detalhesDespesaVM = new DetalhesDespesaViewModel(
                        d.Id,
                        d.Descricao,
                        d.DataOcorrencia,
                        d.Valor,
                        d.FormaDoPagamento,
                        d.Categorias,
                        d.CategoriasTitulo
                        );
                }
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
            public Guid IdDespesa { get; set; }

            public ExcluirCategoriaDespesaViewModel() { }

            public ExcluirCategoriaDespesaViewModel(Guid id, string Titulo, Guid idDespesa) : this()
            {
                Id = id;
                this.Titulo = Titulo;
                this.IdDespesa = idDespesa;
            }
        }
    }
}
