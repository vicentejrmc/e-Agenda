using eAgenda.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloTarefa;

public class Tarefa : EntidadeBase<Tarefa>
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Prioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataConclusao { get; set; }
    public string StatusConcluida { get; set; }
    public double PercentualConcluida { get; set; }
    public List<Item> Items { get; set; }

    public Tarefa()
    {
        Items = new List<Item>();
    }

    public Tarefa(string titulo, string prioridade, DateTime dataCriacao, double percentualConcluida, string statusConcluida, DateTime dataConclusao) : this()
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
        PercentualConcluida = percentualConcluida;
        StatusConcluida = statusConcluida;
        DataConclusao = dataConclusao;
    }

    public void AtualizarPercentual()
    {
        if (Items == null || Items.Count == 0)
        {
            PercentualConcluida = 0;
            return;
        }

        int quantidadeConcluidos = 0;

        foreach (var item in Items)
        {
            if (item.StatusConclusao == "Concluído")
            {
                quantidadeConcluidos++;
            }
        }

        PercentualConcluida = Math.Round((quantidadeConcluidos * 100.0) / Items.Count, 2);
    }

    public override void AtualizarRegistro(Tarefa registro)
    {
        Titulo = registro.Titulo;
        Prioridade = registro.Prioridade;
        DataCriacao = registro.DataCriacao;
        DataConclusao = registro.DataConclusao;
        StatusConcluida = registro.StatusConcluida;
        PercentualConcluida = registro.PercentualConcluida;

        // Atualiza os itens: limpa e adiciona os novos
        Items.Clear();
        if (registro.Items != null)
        {
            foreach (var item in registro.Items)
            {
                Items.Add(item);
            }
        }
    }

}