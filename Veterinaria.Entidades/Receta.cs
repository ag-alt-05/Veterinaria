using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Entidades;

public class Receta
{
    public int IdReceta { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una consulta.")]
    public int IdConsulta { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public string? IndicacionesGenerales { get; set; }
}