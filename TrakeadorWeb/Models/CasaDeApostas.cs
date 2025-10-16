using System.ComponentModel.DataAnnotations;

namespace TrakeadorWeb.Models
{
    public class CasaDeApostas
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }

        [StringLength(200)]
        public string? UrlBase { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Relacionamentos
        public virtual ICollection<ExpertCasaApostasAfiliado> Experts { get; set; } = new List<ExpertCasaApostasAfiliado>();
    }
}