using System.ComponentModel.DataAnnotations;

namespace TrakeadorWeb.Models
{
    public class Expert
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public bool Ativo { get; set; } = true;

        // Relacionamentos
        public virtual ICollection<ExpertCasaApostasAfiliado> CasasDeApostas { get; set; } = new List<ExpertCasaApostasAfiliado>();
    }
}