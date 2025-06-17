namespace e_Agenda.Dominio.ModuloTarefa;

public class Item
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; }
    public string StatusConclusao { get; set; }
    public Tarefa Tarefa { get; set; }

    Item(string titulo, string statusConclusao, Tarefa tarefa)
    {
        Titulo = titulo;
        StatusConclusao = statusConclusao;
        Tarefa = tarefa;
    }
}