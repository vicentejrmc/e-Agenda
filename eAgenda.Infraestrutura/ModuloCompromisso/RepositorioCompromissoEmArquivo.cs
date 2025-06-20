using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Infraestrutura.Compartilhado;

namespace eAgenda.Infraestrutura.ModuloCompromisso
{
    public class RepositorioCompromissoEmArquivo : RepositorioBaseEmArquivo<Compromisso>, IRepositorioCompromisso
    {
        public RepositorioCompromissoEmArquivo(ContextoDeDados contexto) : base(contexto)
        {
        }

        protected override List<Compromisso> ObterRegistros()
        {
            return contexto.Compromissos;
        }
    }
}
