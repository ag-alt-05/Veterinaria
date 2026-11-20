using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Entidades;

public class Consulta
{
    public int IdConsulta { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una mascota.")]
    public int IdMascota { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un veterinario.")]
    public int IdVeterinario { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [StringLength(250)]
    public string? MotivoConsulta { get; set; }

    public decimal? Peso { get; set; }

    public decimal? Temperatura { get; set; }

    public string? Sintomas { get; set; }

    public string? Diagnostico { get; set; }

    public string? Tratamiento { get; set; }

    public string? Observaciones { get; set; }
}