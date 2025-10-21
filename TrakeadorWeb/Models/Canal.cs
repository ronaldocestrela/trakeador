using System.ComponentModel.DataAnnotations;

namespace TrakeadorWeb.Models;

public class Canal
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    public virtual ICollection<Destino> Destinos { get; set; } = new List<Destino>();
}
