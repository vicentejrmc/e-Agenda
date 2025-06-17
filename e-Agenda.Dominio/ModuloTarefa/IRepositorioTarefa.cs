namespace e_Agenda.Dominio.ModuloTarefa;

public interface IRepositorioTarefa
{
    void CadastrarTarefa(Tarefa tarefa);
    Tarefa SelecionarPorId(Guid idRegistro);
    List<Tarefa> SelecionarTarefas();
    List<Tarefa> SelecionarTarefasAbertas();
    List<Tarefa> SelecionarTarfeasFechadas();
    List<Tarefa> SelecionarTarefaPorPeriodo(DateTime data);
}