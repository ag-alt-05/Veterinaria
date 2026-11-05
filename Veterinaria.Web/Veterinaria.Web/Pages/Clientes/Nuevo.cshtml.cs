
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Clientes;

public class NuevoModel : PageModel
{
    [BindProperty]
    public ClienteTemporal Cliente { get; set; } = new();

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        Cliente.IdCliente = DatosTemporales.Clientes.Any()
            ? DatosTemporales.Clientes.Max(c => c.IdCliente) + 1 : 1;

        DatosTemporales.Clientes.Add(Cliente);

        TempData["Mensaje"] = "Cliente registrado correctamente.";
        return RedirectToPage("./Index");
    }
}
