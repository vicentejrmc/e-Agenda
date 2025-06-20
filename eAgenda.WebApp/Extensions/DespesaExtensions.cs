using eAgenda.Dominio.ModuloDespesa;
using eAgenda.WebApp.Models;
using static eAgenda.WebApp.Models.FormularioDespesaViewModel;

namespace eAgenda.WebApp.Extensions
{
    public static class DespesaExtensions
    {
        public static Despesa ParaEntidade(this FormularioDespesaViewModel formularioVM)
        {
            return new Despesa(formularioVM.descricao, formularioVM.dataOcorrencia, formularioVM.valor, formularioVM.formaDoPagamento, formularioVM.categorias, formularioVM.categoriasTitulo);
        }

        public static DetalhesDespesaViewModel ParaDetalhesVM(this Despesa despesa)
        {
            return new DetalhesDespesaViewModel(
                despesa.Id,
                despesa.descricao,
                despesa.dataOcorrencia,
                despesa.valor,
                despesa.formaDoPagamento,
                despesa.categorias,
                despesa.categoriasTitulo
            );
        }
    }
}
