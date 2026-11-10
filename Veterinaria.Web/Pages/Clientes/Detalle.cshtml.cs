using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Clientes;

public class DetalleModel : PageModel
{
    public Cliente Cliente { get; set; } = new();

    public List<Mascota> Mascotas { get; set; } = new();

    public string? Error { get; set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            ClienteNegocio clienteNegocio = new();

            Cliente? encontrado = clienteNegocio.BuscarPorId(id);

            if (encontrado == null)
                return NotFound();

            Cliente = encontrado;

            MascotaNegocio mascotaNegocio = new();

            Mascotas = mascotaNegocio.ListarPorCliente(Cliente.IdCliente);

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible cargar los datos del cliente y sus mascotas.";

            return Page();
        }
    }
}