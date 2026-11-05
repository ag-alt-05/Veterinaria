
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Clientes;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Buscar { get; set; }

    public List<ClienteTemporal> Clientes { get; set; } = new();

    public void OnGet()
    {
        Clientes = DatosTemporales.Clientes;

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            Clientes = Clientes.Where(c =>
                c.Nombre.Contains(Buscar, StringComparison.OrdinalIgnoreCase) ||
                c.ApellidoPaterno.Contains(Buscar, StringComparison.OrdinalIgnoreCase) ||
                c.Correo.Contains(Buscar, StringComparison.OrdinalIgnoreCase) ||
                c.Telefono.Contains(Buscar, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }
    }
}
