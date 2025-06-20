using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloTarefa;

public interface IRepositorioTarefa
{
    void CadastrarTarefa(Tarefa conta);
    bool EditarTarefa(Guid id, Tarefa conta);
    Tarefa SelecionarPorId(Guid idRegistro);
    List<Tarefa> SelecionarTarefas();
    List<Tarefa> SelecionarTarefasPendentes();
    List<Tarefa> SelecionarTarefasConcluidas();
    void AtualizarStatus(Guid id);
    void AtualizarPercentual(Guid id);
    bool ExcluirTarefa(Guid id);
}