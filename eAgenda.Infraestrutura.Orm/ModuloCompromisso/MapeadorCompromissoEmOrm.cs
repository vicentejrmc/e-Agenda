using eAgenda.Dominio.ModuloCompromisso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infraestrutura.Orm.ModuloCompromisso
{
    public class MapeadorCompromissoEmOrm : IEntityTypeConfiguration<Compromisso>
    {
        public void Configure(EntityTypeBuilder<Compromisso> builder)
        {
           builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(c => c.Assunto)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.DataOcorrencia)
                .IsRequired();

            builder.Property(c => c.HoraInicio)
                .IsRequired();

            builder.Property(c => c.HoraTermino)
                .IsRequired();

            builder.Property(c => c.TipoCompromisso)
                .HasMaxLength(100);

            builder.Property(c => c.Local)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.Link)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.HasOne(c => c.Contato)
                .WithMany(c => c.Compromissos)
                .IsRequired(false);

            // minuto 19 do Video 4
        }
    }
}
