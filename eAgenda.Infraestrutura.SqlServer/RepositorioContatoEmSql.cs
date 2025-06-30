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
            var sqlInserir =
                @"INSERT INTO [TBCONTATO]
                (
                    [Id],
                    [Nome],
                    [Email],
                    [Telefone],
                    [Cargo],
                    [Empresa]
                )
                VALUES
                (
                    @ID,   
                    @NOME,   
                    @EMAIL,
                    @TELEFONE,
                    @CARGO,
                    @EMPRESA
                );";

            SqlConnection coneccaoCombanco = new SqlConnection(connectionString);
            SqlCommand comandoInserir = new SqlCommand(sqlInserir, coneccaoCombanco);

            ConfigurarParametrosContato(novoRegistro, comandoInserir);

            coneccaoCombanco.Open();

            comandoInserir.ExecuteNonQuery();

            coneccaoCombanco.Close();

        }

        public bool EditarRegistro(Guid idRegistro, Contato registroEditado)
        {
            var sqlEditar =
            @"UPDATE [TBCONTATO]	
		    SET
			    [NOME] = @NOME,
			    [EMAIL] = @EMAIL,
			    [TELEFONE] = @TELEFONE,
			    [CARGO] = @CARGO,
			    [EMPRESA] = @EMPRESA
		    WHERE
			    [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            registroEditado.Id = idRegistro;

            ConfigurarParametrosContato(registroEditado, comandoEdicao);

            conexaoComBanco.Open();

            var linhasAfetadas = comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return linhasAfetadas > 0;
        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            var sqlExcluir =
            @"DELETE FROM [TBCONTATO]
		    WHERE
			    [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", idRegistro);

            conexaoComBanco.Open();

            var linhasAfetadas = comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();

            return linhasAfetadas > 0;
        }

        public Contato SelecionarRegistroPorId(Guid idRegistro)
        {
            var sqlSelecionarPorId =
           @"SELECT 
		        [ID], 
		        [NOME], 
		        [EMAIL],
		        [TELEFONE],
		        [CARGO],
		        [EMPRESA]
	        FROM 
		        [TBCONTATO]
            WHERE
                [ID] = @ID";

            SqlConnection conexaoComBanco = new SqlConnection(connectionString);

            SqlCommand comandoSelecao =
                new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", idRegistro);

            conexaoComBanco.Open();

            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Contato? contato = null;

            if (leitor.Read())
                contato = ConverterParaContato(leitor);

            return contato;
        }

        public List<Contato> SelecionarRegistros()
        {
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

        private void ConfigurarParametrosContato(Contato contato, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", contato.Id);
            comando.Parameters.AddWithValue("NOME", contato.Nome);
            comando.Parameters.AddWithValue("EMAIL", contato.Email);
            comando.Parameters.AddWithValue("TELEFONE", contato.Telefone);
            comando.Parameters.AddWithValue("CARGO", contato.Cargo);
            comando.Parameters.AddWithValue("EMPRESA", contato.Empresa);
        }
    }
}

