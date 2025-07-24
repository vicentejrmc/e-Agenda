using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCategoria;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class Despesa : EntidadeBase<Despesa>
    {
        public string Descricao {  get; set; }
        public DateTime DataOcorrencia { get; set; }
        public double Valor {  get; set; }
        public FormaDoPagamento FormaDoPagamento { get; set; }
        public List<Categoria> Categorias { get; set; }
        public Despesa()
        {
            Categorias = new List<Categoria>();
        }
        public Despesa(string descricao, DateTime dataOcorrencia, double valor, FormaDoPagamento formaDoPagamento) : this()
        {
            Id = Guid.NewGuid();
            this.Descricao = descricao;
            this.DataOcorrencia = dataOcorrencia;
            this.Valor = valor;
            this.FormaDoPagamento = formaDoPagamento;
        }

        public override void AtualizarRegistro(Despesa registroEditado)
        {
            Descricao = registroEditado.Descricao;
            DataOcorrencia = registroEditado.DataOcorrencia;
            Valor = registroEditado.Valor;
            FormaDoPagamento = registroEditado.FormaDoPagamento;
            Categorias = registroEditado.Categorias;
        }

        public void RegistarCategoria(Categoria categoria)
        {
            if (Categorias.Contains(categoria))
                return;

            categoria.Despesas.Add(this);
            Categorias.Add(categoria);
        }

        public void RemoverCategoria(Categoria categoria)
        {
            if (!Categorias.Contains(categoria))
                return;

            categoria.Despesas.Remove(this);
            Categorias.Remove(categoria);
        }
    }
}
