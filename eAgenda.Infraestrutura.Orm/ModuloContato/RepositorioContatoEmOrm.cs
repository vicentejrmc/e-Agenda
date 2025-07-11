using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Orm.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.Orm.ModuloContato
{
    public class RepositorioContatoEmOrm : IRepositorioContato
    {
        // O DbContext é a classe que representa a sessão com o banco de dados
        // e é responsável por gerenciar as entidades e suas operações, é
        // utilizado para realizar operações de CRUD (Create, Read, Update, Delete) no banco de dados
        // Atraves do DbContext, é possível acessar as tabelas do banco de dados. (eAgendaDbContext contexto)
        // Repetimos a lógica aprendida anteriormente nos repositórios de arquivo, mas agora utilizando
        // o Entity Framework Core para persistir os dados no banco de dados relacional.

        private readonly eAgendaDbContext contexto;     
        public RepositorioContatoEmOrm(eAgendaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public void CadastrarRegistro(Contato novoRegistro)
        {
            // Adiciona o novo registro na tabela Contatos do banco de dados
            contexto.Contatos.Add(novoRegistro);

            // contexto.SaveChanges();
            // Salva as alterações no banco de dados
            // Entretanto estaremos usando o Entity Framework Core, que já gerencia
            // as transações automaticamente no proprio ContatoController.
            // de forma que poderemos controlar as transações de forma mais eficiente
            // e evitar problemas de concorrência(Várias ações passadas ao mesmo tempo)
            // que podem gerar erros de gravação/exceções.
        }

        public bool EditarRegistro(Guid idRegistro, Contato registroEditado)
        {
            // Busca o registro pelo ID na tabela Contatos do banco de dados
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
            // Busca o registro pelo ID na tabela Contatos do banco de dados
            var registro = SelecionarRegistroPorId(idRegistro);

            // Se o registro não for encontrado, retorna false
            if (registro == null)
                return false;

            // Remove o registro encontrado da tabela Contatos do banco de dados
            contexto.Contatos.Remove(registro);

            return true;
        }

        public Contato? SelecionarRegistroPorId(Guid idRegistro)
        {
            // Busca o registro pelo ID na tabela Contatos do banco de dados
            return contexto.Contatos.FirstOrDefault(c => c.Id.Equals(idRegistro))!;
            // Método FirstOrDefault retorna o primeiro elemento que satisfaz a condição especificada
            // ou null se nenhum elemento for encontrado. por isso o uso do operador '?' na assinatura do método
            // o que deve ser replicado e tratado na Interface
        }

        public List<Contato> SelecionarRegistros()
        {
            // Retorna todos os registros da tabela Contatos do banco de dados
            return contexto.Contatos.ToList();
        }
    }
}
