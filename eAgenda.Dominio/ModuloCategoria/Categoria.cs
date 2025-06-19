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
        public List<Despesa> despesas = new List<Despesa>();

        public Categoria() { }     
        public Categoria(string titulo, List<Despesa> despesas)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            this.despesas = despesas;
        }

        public override void AtualizarRegistro(Categoria registroEditado)
        {
            Titulo = registroEditado.Titulo;

        }
    
    }
}
