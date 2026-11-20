using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Veterinarios;

public class EditarModel : PageModel
{
    [BindProperty]
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

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            VeterinarioNegocio negocio = new();

            bool actualizado = negocio.Modificar(Veterinario);

            if (!actualizado)
                return NotFound();

            TempData["Mensaje"] =
                "Veterinario actualizado correctamente.";

            return RedirectToPage("./Index");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            ModelState.AddModelError(
                string.Empty,
                "No fue posible actualizar el veterinario."
            );

            return Page();
        }
    }
}