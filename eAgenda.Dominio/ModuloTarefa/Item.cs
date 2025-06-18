namespace eAgenda.Dominio.ModuloTarefa;

public class Item
{
    public string Titulo { get; set; }
    public string StatusConclusao { get; set; }
    public Tarefa Tarefa { get; set; }

    public Item() { }
    public Item(string titulo, string statusConclusao, Tarefa tarefa)
    {
        Titulo = titulo;
        StatusConclusao = statusConclusao;
        Tarefa = tarefa;
    }
}