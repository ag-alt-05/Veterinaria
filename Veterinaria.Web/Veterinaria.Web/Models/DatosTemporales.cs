
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Web.Models;

public class ClienteTemporal
{
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
    public string ApellidoPaterno { get; set; } = "";

    public string? ApellidoMaterno { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^[0-9]{10}$",
        ErrorMessage = "Ingresa un teléfono de 10 dígitos.")]
    public string Telefono { get; set; } = "";

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo válido.")]
    public string Correo { get; set; } = "";

    public string? Direccion { get; set; }
}

public class MascotaTemporal
{
    public int IdMascota { get; set; }

    [Required(ErrorMessage = "Selecciona un propietario.")]
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = "";

    [Required(ErrorMessage = "La especie es obligatoria.")]
    public string Especie { get; set; } = "";

    public string? Raza { get; set; }

    [Required(ErrorMessage = "Selecciona el sexo.")]
    public string Sexo { get; set; } = "";

    public string? Color { get; set; }
}

public static class DatosTemporales
{
    public static List<ClienteTemporal> Clientes { get; } = new()
    {
        new() { IdCliente=1, Nombre="Ana", ApellidoPaterno="López",
            ApellidoMaterno="García", Telefono="6461234567",
            Correo="ana@correo.com", Direccion="Ensenada" },

        new() { IdCliente=2, Nombre="Carlos", ApellidoPaterno="Ramírez",
            ApellidoMaterno="Torres", Telefono="6467654321",
            Correo="carlos@correo.com", Direccion="Ensenada" }
    };

    public static List<MascotaTemporal> Mascotas { get; } = new()
    {
        new() { IdMascota=1, IdCliente=1, Nombre="Max",
            Especie="Perro", Raza="Labrador", Sexo="Macho" },

        new() { IdMascota=2, IdCliente=1, Nombre="Luna",
            Especie="Gato", Raza="Siamés", Sexo="Hembra" },

        new() { IdMascota=3, IdCliente=2, Nombre="Rocky",
            Especie="Perro", Raza="Pastor Alemán", Sexo="Macho" }
    };
}
