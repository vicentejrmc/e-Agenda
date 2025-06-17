using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.WebApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.WebApp.Models
{
    public abstract class FormularioCompromissoViewModel
    {
        [Required(ErrorMessage = "O campo \"Assunto\" é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Assunto\" precisa conter entre 2 e 100 caracteres.")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "O campo \"Data de Ocorrência\" é obrigatório.")]
        public DateTime DataOcorrencia { get; set; }

        [Required(ErrorMessage = "O campo \"Hora de Início\" é obrigatório.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "O campo \"Hora de Término\" é obrigatório.")]
        public TimeSpan HoraTermino { get; set; }

        [Required(ErrorMessage = "O campo \"Tipo de Compromisso\" é obrigatório.")]
        public TipoCompromissoEnum TipoCompromisso { get; set; }
        public string? Local { get; set; }
        public string? Link { get; set; }
        public Contato? Contato { get; set; } 
    }

    public class CadastrarCompromissoViewModel : FormularioCompromissoViewModel
    {
        public CadastrarCompromissoViewModel() { }
        public CadastrarCompromissoViewModel
            (string assunto,
            DateTime dataOcorrencia,
            TimeSpan horaInicio,
            TimeSpan horaTermino,
            TipoCompromissoEnum tipoCompromisso,
            string? local = null,
            string? link = null,
            Contato? contato = null)
        {
            Assunto = assunto;
            DataOcorrencia = dataOcorrencia;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            TipoCompromisso = tipoCompromisso;
            Local = local;
            Link = link;
            Contato = contato;
        }
    }

    public class EditarCompromissoViewModel : FormularioCompromissoViewModel
    {
        public Guid Id { get; set; }
        public EditarCompromissoViewModel() { }
        public EditarCompromissoViewModel
            (Guid id,
            string assunto,
            DateTime dataOcorrencia,
            TimeSpan horaInicio,
            TimeSpan horaTermino,
            TipoCompromissoEnum tipoCompromisso,
            string? local = null,
            string? link = null,
            Contato? contato = null) : this()
        {
            Id = id;
            Assunto = assunto;
            DataOcorrencia = dataOcorrencia;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            TipoCompromisso = tipoCompromisso;
            Local = local;
            Link = link;
            Contato = contato;
        }
    }

    public class ExcluirCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public ExcluirCompromissoViewModel() { }

        public ExcluirCompromissoViewModel(Guid id, string assunto) : this()
        {
            Id = id;
            Assunto = assunto;
        }
    }


    public class VisualizarCompromissoViewModel
    {
        public List<DetalhesCompromissoViewModel> Registros { get; }

        public VisualizarCompromissoViewModel(List<Compromisso> compromissos)
        {
            Registros = new List<DetalhesCompromissoViewModel>();

            if (compromissos != null)
            {
                foreach (var m in compromissos)
                {
                    var detalhesVM = m.ParaDetalhesVM();
                    Registros.Add(detalhesVM);
                }
            }
        }
    }

    public class DetalhesCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TipoCompromissoEnum TipoCompromisso { get; set; }
        public string? Local { get; set; }
        public string? Link { get; set; }
        public Contato? Contato { get; set; }
        public DetalhesCompromissoViewModel() { }
        public DetalhesCompromissoViewModel
            (Guid id,
            string assunto, 
            DateTime dataOcorrencia,
            TimeSpan horaInicio,
            TimeSpan horaTermino,
            TipoCompromissoEnum tipoCompromisso,
            string? local = null,
            string? link = null, 
            Contato? contato = null)
        {
            Id = id;
            Assunto = assunto;
            DataOcorrencia = dataOcorrencia;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            TipoCompromisso = tipoCompromisso;
            Local = local;
            Link = link;
            Contato = contato;
        }
    }

    public class SelecionarCompromissoViewModel
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public SelecionarCompromissoViewModel() { }
        public SelecionarCompromissoViewModel(Guid id, string assunto) : this()
        {
            Id = id;
            Assunto = assunto;
        }
    }
}
