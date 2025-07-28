using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.Orm.ModuloDespesa
{
    public class RepositorioDespesaEmOrm : RepositorioBaseEmOrm<Despesa>, IRepositorioDespesa
    {
        public RepositorioDespesaEmOrm(eAgendaDbContext contexto) : base(contexto)
        {
        }
    }
}
