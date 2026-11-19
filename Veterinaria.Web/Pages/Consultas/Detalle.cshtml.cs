using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Consultas;

public class DetalleModel : PageModel
{
    public Consulta Consulta { get; set; } = new();

    public Mascota Mascota { get; set; } = new();

    public string Propietario { get; set; } = "";

    public Veterinario Veterinario { get; set; } = new();

    public string? Error { get; set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            ConsultaNegocio consultaNegocio = new();
            MascotaNegocio mascotaNegocio = new();
            ClienteNegocio clienteNegocio = new();
            VeterinarioNegocio veterinarioNegocio = new();

            Consulta? encontrada = consultaNegocio.BuscarPorId(id);

            if (encontrada == null)
                return NotFound();

            Consulta = encontrada;

            Mascota? mascotaEncontrada =
                mascotaNegocio.BuscarPorId(Consulta.IdMascota);

            if (mascotaEncontrada == null)
                return NotFound();

            Mascota = mascotaEncontrada;

            var cliente = clienteNegocio.BuscarPorId(Mascota.IdCliente);

            Propietario = cliente == null
                ? "Sin propietario"
                : $"{cliente.Nombre} {cliente.ApellidoPaterno}";

            Veterinario? veterinarioEncontrado =
                veterinarioNegocio.BuscarPorId(Consulta.IdVeterinario);

            if (veterinarioEncontrado != null)
                Veterinario = veterinarioEncontrado;

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible cargar los detalles de la consulta.";

            return Page();
        }
    }
}