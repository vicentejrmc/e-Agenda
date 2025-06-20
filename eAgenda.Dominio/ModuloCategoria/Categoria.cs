using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloDespesa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloCategoria
{
    public class Categoria : EntidadeBase<Categoria>
    {
        public string Titulo { get; set; }
        public List<Guid> despesas { get; set; } = new List<Guid>();
        public List<Despesa> despesas1 { get; set; } = new List<Despesa> { };

        public Categoria() { }     
        public Categoria(string titulo, List<Guid> despesas, List<Despesa> despesas1)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            this.despesas = despesas;
            this.despesas1 = despesas1;
        }

        public override void AtualizarRegistro(Categoria registroEditado)
        {
            Titulo = registroEditado.Titulo;
            despesas = registroEditado.despesas;
        }
    
    }
}
