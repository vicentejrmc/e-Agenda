using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class Despesa : EntidadeBase<Despesa>
    {
        public string Descricao {  get; set; }
        public DateTime DataOcorrencia { get; set; }
        public double Valor {  get; set; }
        public string FormaDoPagamento { get; set; }
        public List<Guid> Categorias { get; set; }
        public List<string> CategoriasTitulo {  get; set; }
        public Despesa() { }
        public Despesa(string descricao, DateTime dataOcorrencia, double valor, string formaDoPagamento, List<Guid> categorias, List<string> categoriasTitulo)
        {
            Id = Guid.NewGuid();
            this.Descricao = descricao;
            this.DataOcorrencia = dataOcorrencia;
            this.Valor = valor;
            this.FormaDoPagamento = formaDoPagamento;
            this.Categorias = categorias;
            this.CategoriasTitulo = categoriasTitulo;
        }

        public override void AtualizarRegistro(Despesa registroEditado)
        {
            Descricao = registroEditado.Descricao;
            DataOcorrencia = registroEditado.DataOcorrencia;
            Valor = registroEditado.Valor;
            FormaDoPagamento = registroEditado.FormaDoPagamento;
            Categorias = registroEditado.Categorias;
            CategoriasTitulo = registroEditado.CategoriasTitulo;
        }
    }
}
