using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Dominio.ModuloDespesa
{
    public enum FormaDoPagamento
    {
        [Display(Name = "PIX")] Pix,
        [Display(Name = "À Vista")] Dinheiro,
        [Display(Name = "Crédito")] Credito,
        [Display(Name = "Débito")] Debito
    }
}
