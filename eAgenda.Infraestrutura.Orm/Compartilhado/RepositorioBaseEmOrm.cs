using eAgenda.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Orm.Compartilhado
{
    public class RepositorioBaseEmOrm<T> where T : EntidadeBase<T>
    {
        private readonly DbSet<T> registros;

        public RepositorioBaseEmOrm(eAgendaDbContext contexto)
        {
            this.registros = contexto.Set<T>();
        }

        public void CadastrarRegistro(T novoRegistro)
        {
            // Adiciona o novo registro na tabela Ts do banco de dados
            registros.Add(novoRegistro);

            // contexto.SaveChanges();
            // Salva as alterações no banco de dados
            // Entretanto estaremos usando o Entity Framework Core, que já gerencia
            // as transações automaticamente no proprio TController.
            // de forma que poderemos controlar as transações de forma mais eficiente
            // e evitar problemas de concorrência(Várias ações passadas ao mesmo tempo)
            // que podem gerar erros de gravação/exceções.
        }

        public bool EditarRegistro(Guid idRegistro, T registroEditado)
        {
            // Busca o registro pelo ID na tabela Ts do banco de dados
            var registro = SelecionarRegistroPorId(idRegistro);

            // Se o registro não for encontrado, retorna false
            if (registro == null)
                return false;

            // Atualiza os dados do registro encontrado com os dados do registro editado
            registro.AtualizarRegistro(registroEditado);

            // Retorna true indicando que a edição foi bem-sucedida
            return true;

        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            // Busca o registro pelo ID na tabela Ts do banco de dados
            var registro = SelecionarRegistroPorId(idRegistro);

            // Se o registro não for encontrado, retorna false
            if (registro == null)
                return false;

            // Remove o registro encontrado da tabela Ts do banco de dados
            registros.Remove(registro);

            return true;
        }

        public T? SelecionarRegistroPorId(Guid idRegistro)
        {
            // Busca o registro pelo ID na tabela Ts do banco de dados
            return registros.FirstOrDefault(c => c.Id.Equals(idRegistro))!;
            // Método FirstOrDefault retorna o primeiro elemento que satisfaz a condição especificada
            // ou null se nenhum elemento for encontrado. por isso o uso do operador '?' na assinatura do método
            // o que deve ser replicado e tratado na Interface
        }

        public List<T> SelecionarRegistros()
        {
            // Retorna todos os registros da tabela Ts do banco de dados
            return registros.ToList();
        }

    }
}
