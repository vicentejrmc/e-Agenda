using eAgenda.Dominio.ModuloContato;
using eAgenda.Infraestrutura.Orm.ModuloContato;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infraestrutura.Orm.Compartilhado
{
    public class eAgendaDbContext : DbContext
    {
// DbSet é uma coleção(lista) de entidades do tipo Contato que serão mapeadas para a tabela Contatos no banco de dados
        public DbSet<Contato> Contatos { get; set; } 

        public eAgendaDbContext(DbContextOptions options) : base(options)
        {
// o Construtor aqui é necessário para o Entity Framework Core reconhecer o DbContext (Conecções externas)
// eAgendaDbContext é o nome do DbContext que será utilizado para a conexão com o banco de dados
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeadorContatoEmOrm());
            base.OnModelCreating(modelBuilder);
        }
    }
}
