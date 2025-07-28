using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace eAgenda.WebApp.Models
{

    public class FormularioDespesaViewModel
    {
        [Required(ErrorMessage = "O campo \"Descrição\" é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" precisa conter entre 2 e 100 caracteres.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo \"Data de Ocorrência\" é obrigatório.")]
        public DateTime DataOcorrencia { get; set; }

        [Required(ErrorMessage = "O campo \"Valor\" é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Valor\" deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo \"Forma de Pagamento\" é obrigatório.")]
        public FormaDoPagamento FormaDoPagamento { get; set; }

        [Required(ErrorMessage = "O campo \"Categorias Selecionadas\" é necessita de ao menos um valor preenchido.")]
        public List<Guid>? CategoriasSelecionadas { get; set; }
        public List<SelectListItem>? CategoriasDisponiveis { get; set; }
    }

    public class CadastrarDespesaViewModel : FormularioDespesaViewModel
    {
        public CadastrarDespesaViewModel()
        {
            CategoriasSelecionadas = new List<Guid>();
            CategoriasDisponiveis = new List<SelectListItem>();
        }

        public CadastrarDespesaViewModel(List<Categoria> categoriasDisponiveis) : this()
        {
            foreach (var c in categoriasDisponiveis)
            {
                var selecionarVM = new SelectListItem(c.Titulo, c.Id.ToString());
                CategoriasDisponiveis?.Add(selecionarVM);
            }
        }
    }

    public class EditarDespesaViewModel : FormularioDespesaViewModel
    {
        public Guid Id { get; set; }

        public EditarDespesaViewModel()
        {
            CategoriasSelecionadas = new List<Guid>();
            CategoriasDisponiveis = new List<SelectListItem>();
        }

        public EditarDespesaViewModel(
            Guid id,
            string descricao,
            DateTime dataOcorrencia,
            decimal valor,
            FormaDoPagamento formaDoPagamento,
            List<Categoria> categoriasSelecionadas,
            List<Categoria> categoriasDisponiveis
            )
        {
            Id = id;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaDoPagamento = formaDoPagamento;

            foreach (var c in categoriasSelecionadas)
                CategoriasSelecionadas?.Add(c.Id);

            foreach (var cd in categoriasDisponiveis)
            {
                var selecionarVM = new SelectListItem(cd.Titulo, cd.Id.ToString());

                CategoriasDisponiveis?.Add(selecionarVM);
            }
        }
    }


    public class ExcluirDespesaViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }

        public ExcluirDespesaViewModel(Guid id, string descricao)
        {
            Id = id;
            this.Descricao = descricao;
        }
    }

    public class VisualizarDespesaViewModel
    {
        public List<DetalhesDespesaViewModel> Registros { get; set; }

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
        public string Descricao { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public decimal Valor { get; set; }
        public FormaDoPagamento FormaDoPagamento { get; set; }
        public List<string> Categorias { get; set; }

        public DetalhesDespesaViewModel(
            Guid id,
            string descricao,
            DateTime dataOcorrencia,
            decimal valor,
            FormaDoPagamento formaDoPagamento,
            List<Categoria> categorias
            )
        {
            Id = id;
            Descricao = descricao;
            DataOcorrencia = dataOcorrencia;
            Valor = valor;
            FormaDoPagamento = formaDoPagamento;

            Categorias = new List<string>();
            if (categorias != null)
            {
                foreach (var c in categorias)
                    Categorias.Add(c.Titulo);
            }
        }
    }

    public class SelecionarDespesaViewModel
    {
        public Guid Id { get; set; }
        public string Descricao { get; }
        public decimal Valor { get; set; }

        public SelecionarDespesaViewModel(Guid id, string descricao, decimal valor)
        {
            Id = id;
            Descricao = descricao;
            Valor = valor;
        }
    }
}


