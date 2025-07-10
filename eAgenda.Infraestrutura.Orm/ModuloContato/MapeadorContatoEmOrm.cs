using eAgenda.Dominio.ModuloContato;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Orm.ModuloContato
{
    public class MapeadorContatoEmOrm : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.Property(c => c.Id)
                 .ValueGeneratedNever()
                 .IsRequired();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Telefone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.Cargo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Empresa)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
