
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Clientes;

public class DetalleModel : PageModel
{
    public ClienteTemporal Cliente { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var encontrado = DatosTemporales.Clientes.FirstOrDefault(c => c.IdCliente == id);
        if (encontrado == null) return NotFound();

        Cliente = encontrado;
        return Page();
    }
}
