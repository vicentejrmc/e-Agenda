using eAgenda.Dominio.ModuloDespesa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.Orm.ModuloDespesa
{
    public class MapeadorDespesaEmOrm : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
           builder.Property(d => d.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(d => d.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.DataOcorrencia)
                .IsRequired();

            builder.Property(d => d.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasMany(d => d.Categorias)
                .WithMany(c => c.Despesas);
        }
    }
}
