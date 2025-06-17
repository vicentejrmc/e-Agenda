using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Dominio.ModuloTarefa;

public class Tarefa
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo {get; set; }
    public string Prioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataConclusao { get; set; }
    public string StatusConcluida { get; set; }
    public double PercentualConcluida { get; set; }
    public List<Item> Items { get; set; }

    Tarefa()
    {
        Items = new List<Item>();
    }
    Tarefa(string titulo, string prioridade, DateTime dataCriacao, DateTime dataConclusao, string statusConcluida, double percentualConcluida, List<Item> items) : this()
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
        DataConclusao = dataConclusao;
        StatusConcluida = statusConcluida;
        PercentualConcluida = percentualConcluida;
    }

}