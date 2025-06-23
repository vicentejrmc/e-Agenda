using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.ModuloContato
{
    public class RepositorioContatoEmArquivo : RepositorioBaseEmArquivo<Contato>, IRepositorioContato
    {
        public RepositorioContatoEmArquivo(ContextoDeDados contexto) : base(contexto)
        {
        }
        protected override List<Contato> ObterRegistros()
        {
            return contexto.Contatos;
        }
    }
}
