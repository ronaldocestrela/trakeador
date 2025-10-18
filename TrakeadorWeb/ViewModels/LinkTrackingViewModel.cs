using System.ComponentModel.DataAnnotations;
using TrakeadorWeb.Models;

namespace TrakeadorWeb.ViewModels
{
    public class LinkTrackingViewModel
    {
        public ExpertCasaApostasAfiliado ExpertCasaApostasAfiliado { get; set; } = null!;

        public int ExpertCasaApostasAfiliadoId { get; set; }

        [Required(ErrorMessage = "O link original é obrigatório")]
        [Display(Name = "Link Original")]
        public string LinkOriginal { get; set; } = string.Empty;

        [Display(Name = "Link Rastreado")]
        public string? LinkRastreado { get; set; }

        [Display(Name = "Canal")]
        public string? Canal { get; set; }

        [Display(Name = "Destino")]
        public string? Destino { get; set; }

        [Display(Name = "Detalhes Adicionais")]
        public string? DetalhesAdicionais { get; set; }
    }
}