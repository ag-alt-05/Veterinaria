using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Entidades;

public class Veterinario
{
    public int IdVeterinario { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(60)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
    [StringLength(60)]
    public string ApellidoPaterno { get; set; } = string.Empty;

    [StringLength(60)]
    public string? ApellidoMaterno { get; set; }

    [StringLength(30)]
    public string? Cedula { get; set; }

    [Required(ErrorMessage = "La especialidad es obligatoria.")]
    [StringLength(100)]
    public string Especialidad { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    [StringLength(120)]
    public string? Correo { get; set; }
}