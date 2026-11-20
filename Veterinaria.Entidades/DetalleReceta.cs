using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Entidades;

public class DetalleReceta
{
    public int IdDetalle { get; set; }

    public int IdReceta { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un medicamento.")]
    public int IdMedicamento { get; set; }

    [Required(ErrorMessage = "La dosis es obligatoria.")]
    [StringLength(80)]
    public string Dosis { get; set; } = string.Empty;

    [StringLength(80)]
    public string? Frecuencia { get; set; }

    [StringLength(80)]
    public string? Duracion { get; set; }

    [StringLength(60)]
    public string? ViaAdministracion { get; set; }

    [StringLength(250)]
    public string? Indicaciones { get; set; }
}