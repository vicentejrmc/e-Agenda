using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeBar.Dominio.ModuloContato;
using ControleDeBar.Dominio.ModuloCompromisso;
using ControleDeBar.Dominio.ModuloDespesa;
using ControleDeBar.Dominio.ModuloCategoria;
using ControleDeBar.Dominio.ModuloTarefa;

namespace ControleDeBar.Infraestrura.Arquivos.Compartilhado;

public class ContextoDados
{
    private string pastaArmazenamento = "C:\\ArquivosJson";
    private string arquivoArmazenamento = "dados-controle-bar.json";

    public List<Contato> Contatos { get; set; }
    public List<Compromisso> Compromissos { get; set; }
    public List<Despesa> Despesas { get; set; }
    public List<Categoria> Categorias { get; set; }
    public List<Tarefa> Tarefas { get; set; }

    public ContextoDados()
    {
        Contatos = new List<Contato>();
        Compromissos = new List<Compromisso>();
        Despesas = new List<Despesa>();
        Categorias = new List<Categoria>();
        Tarefas = new List<Tarefa>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(
            json,
            jsonOptions
        )!;

        if (contextoArmazenado == null) return;

        Tarefas = contextoArmazenado.Tarefas;
        Categorias = contextoArmazenado.Categorias;
        Despesas = contextoArmazenado.Despesas;
        Compromissos = contextoArmazenado.Compromissos;
        Contatos = contextoArmazenado.Contatos;
    }
}