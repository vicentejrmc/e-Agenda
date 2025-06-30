using eAgenda.Dominio.ModuloContato;
using Microsoft.Data.SqlClient;

namespace eAgenda.Infraestrutura.SqlServer
{
    public class RepositorioContatoEmSql : IRepositorioContato
    {
        private readonly string connectionString =
            "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=eAgendaDb;Integrated Security=True";
        public void CadastrarRegistro(Contato novoRegistro)
        {
            throw new NotImplementedException();
        }

        public bool EditarRegistro(Guid idRegistro, Contato registroEditado)
        {
            throw new NotImplementedException();
        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            throw new NotImplementedException();
        }

        public Contato SelecionarRegistroPorId(Guid idRegistro)
        {
            throw new NotImplementedException();
        }

        public List<Contato> SelecionarRegistros()
        {
            //configuração 

            var sqlSelecionarTodos =
                @"SELECT
                    [Id],
                    [Nome],
                    [Email],
                    [Telefone],
                    [Cargo],
                    [Empresa]
                FROM
                    [TBCONTATO]
                ";

            SqlConnection coneccaoCombanco = new SqlConnection(connectionString);

            coneccaoCombanco.Open();

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, coneccaoCombanco);

            //Execução
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            var contatos = new List<Contato>();

            while (leitor.Read())
            {
                var contato = ConverterParaContato(leitor);

                contatos.Add(contato);
            }

            return contatos;
        }

        private Contato ConverterParaContato(SqlDataReader leitor)
        {
            var contato = new Contato(
                   Convert.ToString(leitor["NOME"]),
                   Convert.ToString(leitor["EMAIL"]),
                   Convert.ToString(leitor["TELEFONE"]),
                   Convert.ToString(leitor["CARGO"]),
                   Convert.ToString(leitor["EMPRESA"])
                );

            contato.Id = Guid.Parse(leitor["ID"].ToString());

            return contato;
        }
    }
}

