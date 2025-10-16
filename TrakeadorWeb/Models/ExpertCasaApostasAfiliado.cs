using System.ComponentModel.DataAnnotations;

namespace TrakeadorWeb.Models
{
    public class ExpertCasaApostasAfiliado
    {
        public int Id { get; set; }

        [Required]
        public int ExpertId { get; set; }

        [Required]
        public int CasaDeApostasId { get; set; }

        [Required]
        [StringLength(500)]
        public string CodigoAfiliado { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? ParametrosAdicionais { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Relacionamentos
        public virtual Expert Expert { get; set; } = null!;
        public virtual CasaDeApostas CasaDeApostas { get; set; } = null!;
    }
}