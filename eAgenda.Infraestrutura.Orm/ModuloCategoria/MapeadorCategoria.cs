using eAgenda.Dominio.ModuloCategoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.Orm.ModuloCategoria
{
    public class MapeadorCategoria : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
              builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(c => c.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.Despesas)
                .WithMany(d => d.Categorias)
                .UsingEntity(j => j.ToTable("CategoriaDespesa"));
        }
    }
}
