using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace eAgenda.WebApp.Models
{
    
    public abstract class FormularioDespesaViewModel
    {
        public List<Categoria> CategoriasDisponiveis { get; set; } = new();
        public List<Guid> CategoriasSelecionadas { get; set; } = new();
        public string descricao { get; set; }
        public DateTime dataOcorrencia = DateTime.Today;
        public double valor { get; set; }
        public string formaDoPagamento { get; set; }
        public List<Guid> categorias {  get; set; } = new();
        public List<string> categoriasTitulo {  get; set; }


        public class CadastrarDespesaViewModel : FormularioDespesaViewModel
        {
            public CadastrarDespesaViewModel() { }

            public CadastrarDespesaViewModel(string descricao, DateTime dataOcorrencia, double valor, string formaDoPagamento, List<Guid> categorias, List<string> categoriasTitulo)
            {
                this.descricao = descricao;
                this.dataOcorrencia = dataOcorrencia;
                this.valor = valor;
                this.formaDoPagamento = formaDoPagamento;
                this.categorias = categorias;
                this.categoriasTitulo = categoriasTitulo;
            }
        }

        public class EditarDespesaViewModel : FormularioDespesaViewModel
        {
            public Guid Id { get; set; }

            public EditarDespesaViewModel() { }

            public EditarDespesaViewModel(Guid id, string descricao, DateTime dataOcorrencia, double valor, string formaDoPagamento, List<Guid> categorias, List<string> categoriasTitulo)
            {
                Id = id;
                this.descricao = descricao;
                this.dataOcorrencia = dataOcorrencia;
                this.valor = valor;
                this.formaDoPagamento = formaDoPagamento;
                this.categorias = categorias;
                this.categoriasTitulo = categoriasTitulo;
            }
        }
        }

        public class ExcluirDespesaViewModel
        {
            public Guid Id { get; set; }
            public string descricao { get; set; }

            public ExcluirDespesaViewModel() { }

            public ExcluirDespesaViewModel(Guid id, string descricao) : this()
            {
                Id = id;
                this.descricao = descricao;
            }
        }

        public class VisualizarDespesaViewModel
        {
            public List<DetalhesDespesaViewModel> Registros { get; }

            public VisualizarDespesaViewModel(List<Despesa> despesas)
            {
                Registros = new List<DetalhesDespesaViewModel>();

                if (despesas != null)
                {
                    foreach (var d in despesas)
                    {
                        var detalhesVM = d.ParaDetalhesVM();
                        Registros.Add(detalhesVM);
                    }
                }
            }
        }

        public class DetalhesDespesaViewModel
        {
            public Guid Id { get; }
            public string descricao { get; set; }
            public DateTime dataOcorrencia = DateTime.Today;
            public double valor { get; set; }
            public string formaDoPagamento { get; set; }
            public List<Guid> categorias {  get; set; }
            public List<string> categoriasTitulo { get; set; }

            public DetalhesDespesaViewModel(Guid id,string descricao, DateTime dataOcorrencia, double valor, string formaDoPagamento, List<Guid> categorias, List<string> categoriasTitulo)
            {
                Id = id;
                this.descricao = descricao;
                this.dataOcorrencia = dataOcorrencia;
                this.valor = valor;
                this.formaDoPagamento = formaDoPagamento;
                this.categorias = categorias;
                this.categoriasTitulo = categoriasTitulo;
            }
        }

        public class SelecionarDespesaViewModel
        {
            public Guid Id { get; set; }
            public string descricao { get; }
            public double valor { get; set; }

            public SelecionarDespesaViewModel(Guid id, string descricao, double valor)
            {
                Id = id;
                this.descricao = descricao;
                this.valor = valor;
            }
        }
    }


