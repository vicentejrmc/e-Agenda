using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.ModuloTarefa
{
    public class RepositorioTarefaEmArquivo : RepositorioBaseEmArquivo<Tarefa>, IRepositorioTarefa
    {
        public RepositorioTarefaEmArquivo(ContextoDeDados contexto) : base(contexto)
        {
        }
        protected override List<Tarefa> ObterRegistros()
        {
            return contexto.Tarefas;
        }
    }
}