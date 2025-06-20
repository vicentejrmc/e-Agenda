using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infraestrutura.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.ModuloTarefa
{
    public class RepositorioTarefaEmArquivo : IRepositorioTarefa
    {
        private readonly ContextoDeDados contexto;
        private readonly List<Tarefa> registros;
        public RepositorioTarefaEmArquivo(ContextoDeDados contexto)
        {
            this.contexto = contexto;
            registros = contexto.Tarefas;
        }

        public List<Tarefa> SelecionarTarefas()
        {
            return registros;
        }

        public void AtualizarPercentual(Guid id)
        {
            Tarefa tarefa = SelecionarPorId(id);
            if (tarefa.Items == null || tarefa.Items.Count == 0)
            {
                tarefa.PercentualConcluida = 0;
                return;
            }

            int quantidadeConcluidos = 0;

            foreach (var item in tarefa.Items)
            {
                if (item.StatusConclusao == "Concluído")
                {
                    quantidadeConcluidos++;
                }
            }

            tarefa.PercentualConcluida = Math.Round((quantidadeConcluidos * 100.0) / tarefa.Items.Count, 2);
            contexto.Salvar();
            return;
        }

        public void AtualizarStatus(Guid id)
        {
            Tarefa tarefa = SelecionarPorId(id);

            if (tarefa.StatusConcluida == "Concluído") return;

            if (tarefa.PercentualConcluida == 100)
            {
                tarefa.DataConclusao = DateTime.Now;
                tarefa.StatusConcluida = "Concluído";
            }

            contexto.Salvar();
            return;
        }

        public List<Tarefa> SelecionarTarefasConcluidas()
        {
            var tarefasConcluidas = new List<Tarefa>();

            foreach (var item in registros)
            {
                if (item.PercentualConcluida == 100)
                    tarefasConcluidas.Add(item);
            }

            return tarefasConcluidas;
        }

        public List<Tarefa> SelecionarTarefasPendentes()
        {
            var tarefasPendentes = new List<Tarefa>();

            foreach (var item in registros)
            {
                if (item.PercentualConcluida != 100)
                    tarefasPendentes.Add(item);
            }

            return tarefasPendentes;
        }

        public void CadastrarTarefa(Tarefa tarefa)
        {
            registros.Add(tarefa);

            contexto.Salvar();
        }

        public Tarefa SelecionarPorId(Guid idRegistro)
        {
            foreach (var item in registros)
            {
                if (item.Id == idRegistro)
                    return item;
            }

            return null;
        }

        public bool EditarTarefa(Guid id, Tarefa tarefa)
        {
            foreach (var item in registros)
            {
                if (item.Id == id)
                {
                    item.AtualizarRegistro(tarefa);

                    contexto.Salvar();

                    return true;
                }
            }

            return false;
        }

        public bool ExcluirTarefa(Guid id)
        {
            Tarefa registroSelecionado = SelecionarPorId(id);

            if (registroSelecionado != null)
            {
                registros.Remove(registroSelecionado);

                contexto.Salvar();

                return true;
            }

            return false;
        }
    }
}