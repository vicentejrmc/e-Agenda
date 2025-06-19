using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using static eAgenda.WebApp.Models.FormularioContatoViewModel;

namespace eAgenda.WebApp.Models
{
    public abstract class FormularioCategoriaViewModel
    {
        public string Titulo {  get; set; }
        public List<Despesa>? despesas {  get; set; }

        public class CadastrarCategoriaViewModel : FormularioCategoriaViewModel
        {
            public CadastrarCategoriaViewModel() { }

            public CadastrarCategoriaViewModel(string titulo, List<Despesa> despesas = null) : this()
            {
                Titulo = titulo;
                this.despesas = despesas;
            }
        }

        public class EditarCategoriaViewModel : FormularioCategoriaViewModel
        {
            public Guid Id { get; set; }

            public EditarCategoriaViewModel() { }

            public EditarCategoriaViewModel(Guid id, string titulo, List<Despesa>? despesas = null) : this()
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

            public ExcluirCategoriaViewModel(Guid id, string titulo) : this()
            {
                Id = id;
                Titulo = titulo;
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
            public List<Despesa>? despesas { get; set; }


            public DetalhesCategoriaViewModel(Guid id, string titulo, List<Despesa>? despesas = null)
            {
                Id = id;
                Titulo = titulo;
                this.despesas = despesas;
            }
        }
        public class SelecionarCategoriaViewModel
        {
            public Guid Id { get; set; }
            public string Titulo { get; }

            public SelecionarCategoriaViewModel(Guid id, string titulo)
            {
                Id = id;
                Titulo = titulo;
            }
        }
    }
}
