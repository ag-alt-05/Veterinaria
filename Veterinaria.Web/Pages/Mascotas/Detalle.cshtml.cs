using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Mascotas;

public class DetalleModel : PageModel
{
    public Mascota Mascota { get; set; } = new();

    public string Propietario { get; set; } = "";

    public string? Error { get; set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            MascotaNegocio mascotaNegocio = new();
            ClienteNegocio clienteNegocio = new();

            Mascota? encontrada = mascotaNegocio.BuscarPorId(id);

            if (encontrada == null)
                return NotFound();

            Mascota = encontrada;

            var cliente = clienteNegocio.BuscarPorId(Mascota.IdCliente);

            Propietario = cliente == null
                ? "Sin propietario"
                : $"{cliente.Nombre} {cliente.ApellidoPaterno}";

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible cargar los detalles de la mascota.";

            return Page();
        }
    }
}