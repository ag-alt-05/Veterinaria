using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Veterinarios;

public class DetalleModel : PageModel
{
    public Veterinario Veterinario { get; set; } = new();

    public string? Error { get; set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            VeterinarioNegocio negocio = new();

            Veterinario? encontrado = negocio.BuscarPorId(id);

            if (encontrado == null)
                return NotFound();

            Veterinario = encontrado;

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible cargar los datos del veterinario.";

            return Page();
        }
    }
}