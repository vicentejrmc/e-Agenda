using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.Orm.ModuloTarefa
{
    public class RepositorioTarefaEmOrm :  IRepositorioTarefa
    {
        private readonly DbSet<Tarefa> tarefas;

        public RepositorioTarefaEmOrm(eAgendaDbContext contexto)
        {
            tarefas = contexto.Tarefas;
        }

        public void AtualizarPercentual(Guid id)
        {
            var tarefa = SelecionarPorId(id);
            if (tarefa == null)
                return;

            tarefa.AtualizarPercentual();
            tarefas.Update(tarefa);
        }

        public void AtualizarStatus(Guid id)
        {
            var tarefa = SelecionarPorId(id);
            if (tarefa == null)

            tarefa.AtualizarPercentual();

            tarefa.StatusConcluida = tarefa.PercentualConcluida == 100 ? "Concluído" : "Pendente";
        }

        public void CadastrarTarefa(Tarefa conta)
        {
            tarefas.Add(conta);
        }

        public bool EditarTarefa(Guid id, Tarefa conta)
        {
            var tarefaExistente = SelecionarPorId(id);
            
            if (tarefaExistente == null)
                return false;

            tarefaExistente.AtualizarRegistro(conta);
            return true;
        }

        public bool ExcluirTarefa(Guid id)
        {
            var tarefa = SelecionarPorId(id);
            
            if (tarefa == null)
                return false;

            tarefas.Remove(tarefa);
            return true;
        }

        public Tarefa? SelecionarPorId(Guid idRegistro)
        {
            return tarefas.FirstOrDefault(t => t.Id.Equals(idRegistro));
        }

        public List<Tarefa> SelecionarTarefas()
        {
            return tarefas.ToList();
        }

        public List<Tarefa> SelecionarTarefasConcluidas()
        {
            return tarefas.Where(t => t.StatusConcluida == "Concluído").ToList();
        }

        public List<Tarefa> SelecionarTarefasPendentes()
        {
            return tarefas.Where(t => t.StatusConcluida == "Pendente").ToList();
        }
    }
}
