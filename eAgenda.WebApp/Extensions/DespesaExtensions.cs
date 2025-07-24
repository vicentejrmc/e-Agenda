using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApp.Models;
using static eAgenda.WebApp.Models.FormularioDespesaViewModel;

namespace eAgenda.WebApp.Extensions
{
    public static class DespesaExtensions
    {
        public static Despesa ParaEntidade(this FormularioDespesaViewModel formularioVM)
        {
            return new Despesa(formularioVM.Descricao, formularioVM.DataOcorrencia, formularioVM.valor, formularioVM.FormaDoPagamento, formularioVM.CategoriaSelecionadas, formularioVM.CategoriasDisponiveis);
        }

        public static DetalhesDespesaViewModel ParaDetalhesVM(this Despesa despesa)
        {
            return new DetalhesDespesaViewModel(
                despesa.Id,
                despesa.Descricao,
                despesa.DataOcorrencia,
                despesa.Valor,
                despesa.FormaDoPagamento,
                despesa.Categorias,
                despesa.CategoriasTitulo
            );
        }
    }
}
