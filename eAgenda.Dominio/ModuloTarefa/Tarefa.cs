using eAgenda.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloTarefa;

public class Tarefa : EntidadeBase<Tarefa>
{
    private DateTime? dataConclusao;

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
    public Tarefa(string titulo, string prioridade, DateTime dataCriacao, DateTime dataConclusao, string statusConcluida, double percentualConcluida) : this()
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
        DataConclusao = dataConclusao;
        StatusConcluida = statusConcluida;
        PercentualConcluida = percentualConcluida;
    }

    public Tarefa(string titulo, string prioridade, DateTime dataCriacao, double percentualConcluida, string statusConcluida, DateTime? dataConclusao)
    {
        Titulo = titulo;
        Prioridade = prioridade;
        DataCriacao = dataCriacao;
        PercentualConcluida = percentualConcluida;
        StatusConcluida = statusConcluida;
        this.dataConclusao = dataConclusao;
    }

    public override void AtualizarRegistro(Tarefa registro)
    {
        Titulo = registro.Titulo;
        Prioridade = registro.Prioridade;
        DataCriacao = registro.DataCriacao;
        DataConclusao = registro.DataConclusao;
        StatusConcluida = registro.StatusConcluida;
        PercentualConcluida = registro.PercentualConcluida;
    }
}