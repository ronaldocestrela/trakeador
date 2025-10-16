using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TrakeadorWeb.ViewModels
{
    public class ExpertCasaApostasViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ExpertId { get; set; }

        [Display(Name = "Expert")]
        public string ExpertNome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione uma casa de apostas")]
        [Display(Name = "Casa de Apostas")]
        public int CasaDeApostasId { get; set; }

        [Display(Name = "Casa de Apostas")]
        public string CasaDeApostasNome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O código de afiliado é obrigatório")]
        [StringLength(500, ErrorMessage = "O código de afiliado não pode ter mais de 500 caracteres")]
        [Display(Name = "Código de Afiliado")]
        public string CodigoAfiliado { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Os parâmetros adicionais não podem ter mais de 1000 caracteres")]
        [Display(Name = "Parâmetros Adicionais")]
        public string? ParametrosAdicionais { get; set; }

        public SelectList? CasasDeApostas { get; set; }
    }
}