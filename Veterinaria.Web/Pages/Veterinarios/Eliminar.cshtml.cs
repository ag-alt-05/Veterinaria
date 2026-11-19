using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Veterinarios;

public class EliminarModel : PageModel
{
    public Veterinario Veterinario { get; private set; } = new();

    public string? Error { get; private set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            VeterinarioNegocio negocio = new();

            Veterinario? veterinario = negocio.BuscarPorId(id);

            if (veterinario is null)
                return NotFound();

            Veterinario = veterinario;

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible consultar el veterinario.";

            return Page();
        }
    }

    public IActionResult OnPost(int id)
    {
        try
        {
            VeterinarioNegocio negocio = new();

            Veterinario? veterinario = negocio.BuscarPorId(id);

            if (veterinario is null)
                return NotFound();

            Veterinario = veterinario;

            bool eliminado = negocio.Eliminar(id);

            if (!eliminado)
                return NotFound();

            TempData["Mensaje"] =
                "Veterinario eliminado correctamente.";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible eliminar el veterinario.";

            return Page();
        }
    }
}