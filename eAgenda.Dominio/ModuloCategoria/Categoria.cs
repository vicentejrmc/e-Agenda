using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloDespesa;

namespace eAgenda.Dominio.ModuloCategoria
{
    public class Categoria : EntidadeBase<Categoria>
    {
        public string Titulo { get; set; }
        public List<Guid> idDespesas { get; set; } = new List<Guid>();
        public List<Despesa> despesas { get; set; } = new List<Despesa> { };

        public Categoria() { }     
        public Categoria(string titulo, List<Guid> despesas, List<Despesa> despesas1)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            this.idDespesas = despesas;
            this.despesas = despesas1;
        }

        public override void AtualizarRegistro(Categoria registroEditado)
        {
            Titulo = registroEditado.Titulo;
            idDespesas = registroEditado.idDespesas;
        }
    
    }
}
