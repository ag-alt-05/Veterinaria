using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Entidades;

public class Cliente
{
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(60)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
    [StringLength(60)]
    public string ApellidoPaterno { get; set; } = string.Empty;

    [StringLength(60)]
    public string? ApellidoMaterno { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;

    // --- NUEVOS CAMPOS DEL DBA ---
    [StringLength(20)]
    public string? TelefonoAlternativo { get; set; }

    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    [StringLength(120)]
    public string? Correo { get; set; }

    [StringLength(200)]
    public string? Direccion { get; set; }

    [StringLength(100)]
    public string? Colonia { get; set; }

    [StringLength(100)]
    public string? Ciudad { get; set; }

    [StringLength(100)]
    public string? Estado { get; set; }

    [StringLength(10)]
    public string? CodigoPostal { get; set; }

    [StringLength(120)]
    public string? ContactoEmergencia { get; set; }

    [StringLength(50)]
    public string? ParentescoEmergencia { get; set; }

    [StringLength(20)]
    public string? TelefonoEmergencia { get; set; }

    public string? Observaciones { get; set; }
    // -----------------------------

    public DateTime FechaRegistro { get; set; }
}