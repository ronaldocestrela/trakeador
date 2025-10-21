using System.ComponentModel.DataAnnotations;

namespace TrakeadorWeb.Models;

public class Destino
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do destino é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecione um canal")]
    public int CanalId { get; set; }
    
    public virtual Canal Canal { get; set; } = null!;
}
