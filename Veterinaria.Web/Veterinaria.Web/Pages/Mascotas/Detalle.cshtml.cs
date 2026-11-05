
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Mascotas;

public class DetalleModel : PageModel
{
    public MascotaTemporal Mascota { get; set; } = new();
    public string Propietario { get; set; } = "";

    public IActionResult OnGet(int id)
    {
        var m = DatosTemporales.Mascotas.FirstOrDefault(x => x.IdMascota == id);
        if (m == null) return NotFound();

        Mascota = m;

        var cliente = DatosTemporales.Clientes.FirstOrDefault(
            c => c.IdCliente == m.IdCliente);

        Propietario = cliente == null ? "Sin propietario" :
            $"{cliente.Nombre} {cliente.ApellidoPaterno}";

        return Page();
    }
}
