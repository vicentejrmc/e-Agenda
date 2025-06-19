using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCategoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloDespesa
{
    public class Despesa : EntidadeBase<Despesa>
    {
        public string descricao {  get; set; }
        public DateTime dataOcorrencia = DateTime.Today;
        public double valor {  get; set; }
        public string formaDoPagamento { get; set; }

        public List<Guid> categorias;
        public List<string> categoriasTitulo;
        public Despesa() { }
        public Despesa(string descricao, DateTime dataOcorrencia, double valor, string formaDoPagamento, List<Guid> categorias, List<string> categoriasTitulo)
        {
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
