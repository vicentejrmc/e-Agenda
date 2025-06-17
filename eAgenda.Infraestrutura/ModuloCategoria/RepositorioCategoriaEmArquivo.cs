using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Infraestrutura.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.ModuloCategoria
{
    public class RepositorioCategoriaEmArquivo : RepositorioBaseEmArquivo<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoriaEmArquivo(ContextoDeDados contexto) : base(contexto)
        {
        }
        protected override List<Categoria> ObterRegistros()
        {
            return contexto.Categorias;
        }
    }
}
