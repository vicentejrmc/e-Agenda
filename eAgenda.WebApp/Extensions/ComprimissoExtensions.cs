using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApp.Models;

namespace eAgenda.WebApp.Extensions
{
    public static class ComprimissoExtensions
    {
        private static readonly IRepositorioContato repositorioContato;

        public static Compromisso ParaEntidade(this FormularioCompromissoViewModel formularioVM)
        {
            return new Compromisso(
                formularioVM.Assunto,
                formularioVM.DataOcorrencia,
                formularioVM.HoraInicio,
                formularioVM.HoraTermino,
                formularioVM.TipoCompromisso,
                formularioVM.Link,
                formularioVM.Local,
                formularioVM.Contato
            );
        }

        public static DetalhesCompromissoViewModel ParaDetalhesVM(this Compromisso compromisso)
        {
            return new DetalhesCompromissoViewModel(
                compromisso.Id,
                compromisso.Assunto,
                compromisso.DataOcorrencia,
                compromisso.HoraInicio,
                compromisso.HoraTermino,
                compromisso.TipoCompromisso,
                compromisso.Local,
                compromisso.Link,
                compromisso.Contato
            );
        }
    }
}
