using eAgenda.Dominio.ModuloCategoria;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.Orm.ModuloCategoria
{
    public class RepositorioCategoriaEmOrm : RepositorioBaseEmOrm<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoriaEmOrm(eAgendaDbContext contexto) : base(contexto)
        {
        }
    }
}
