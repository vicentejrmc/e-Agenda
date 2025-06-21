using eAgenda.Dominio.Compartilhado;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class Despesa : EntidadeBase<Despesa>
    {
        public string descricao {  get; set; }
        public DateTime dataOcorrencia { get; set; }
        public double valor {  get; set; }
        public string formaDoPagamento { get; set; }
        public List<Guid> categorias { get; set; }
        public List<string> categoriasTitulo {  get; set; }
        public Despesa() { }
        public Despesa(string descricao, DateTime dataOcorrencia, double valor, string formaDoPagamento, List<Guid> categorias, List<string> categoriasTitulo)
        {
            Id = Guid.NewGuid();
            this.descricao = descricao;
            this.dataOcorrencia = dataOcorrencia;
            this.valor = valor;
            this.formaDoPagamento = formaDoPagamento;
            this.categorias = categorias;
            this.categoriasTitulo = categoriasTitulo;
        }

        public override void AtualizarRegistro(Despesa registroEditado)
        {
            descricao = registroEditado.descricao;
            dataOcorrencia = registroEditado.dataOcorrencia;
            valor = registroEditado.valor;
            formaDoPagamento = registroEditado.formaDoPagamento;
            categorias = registroEditado.categorias;
            categoriasTitulo = registroEditado.categoriasTitulo;
        }
    }
}
