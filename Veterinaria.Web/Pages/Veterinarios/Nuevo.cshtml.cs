using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Veterinarios;

public class NuevoModel : PageModel
{
    [BindProperty]
    public Veterinario Veterinario { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            VeterinarioNegocio negocio = new();

            int id = negocio.Agregar(Veterinario);

            TempData["Mensaje"] =
                $"Veterinario registrado correctamente. ID: {id}";

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
                "No fue posible registrar el veterinario."
            );

            return Page();
        }
    }
}