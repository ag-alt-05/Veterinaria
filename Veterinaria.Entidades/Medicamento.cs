using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Entidades;

public class Medicamento
{
    public int IdMedicamento { get; set; }

    [Required(ErrorMessage = "El nombre del medicamento es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Presentacion { get; set; }

    [StringLength(60)]
    public string? Concentracion { get; set; }
}