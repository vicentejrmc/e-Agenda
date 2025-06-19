namespace eAgenda.Dominio.ModuloTarefa;

public class Item
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string StatusConclusao { get; set; }
    public Tarefa Tarefa { get; set; }

    public Item() { }
    public Item(string titulo, string statusConclusao, Tarefa tarefa)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        StatusConclusao = statusConclusao;
        Tarefa = tarefa;
    }
}