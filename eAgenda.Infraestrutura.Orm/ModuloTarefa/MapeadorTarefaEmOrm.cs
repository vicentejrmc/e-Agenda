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
    public class MapeadorTarefaEmOrm : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
           builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(t => t.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Prioridade)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.DataCriacao)
                .IsRequired();

            builder.Property(t => t.DataConclusao)
                .IsRequired();

            builder.Property(t => t.StatusConcluida)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.PercentualConcluida)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.HasMany(t => t.Items)
                .WithOne(i => i.Tarefa)
                .OnDelete(DeleteBehavior.Cascade); // Deleta os itens associados quando a tarefa é deletada
        }
    }
}
