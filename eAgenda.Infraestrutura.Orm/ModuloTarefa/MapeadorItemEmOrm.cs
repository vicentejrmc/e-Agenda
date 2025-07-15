using eAgenda.Dominio.ModuloTarefa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Infraestrutura.Orm.ModuloTarefa
{
    public class MapeadorItemEmOrm : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(i => i.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(i => i.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.StatusConclusao)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(i => i.Tarefa)
                .WithMany(t => t.Items)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade); // Deleta os itens associados quando a tarefa é deletada
        }
    }
}
