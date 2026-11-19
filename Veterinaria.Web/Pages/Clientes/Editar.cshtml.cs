using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Clientes;

public class EditarModel : PageModel
{
    [BindProperty]
    public Cliente Cliente { get; set; } = new();

    public string? Error { get; set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            ClienteNegocio negocio = new();

            Cliente? encontrado = negocio.BuscarPorId(id);

            if (encontrado == null)
                return NotFound();

            Cliente = encontrado;

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible cargar los datos del cliente.";
            return Page();
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            ClienteNegocio negocio = new();

            bool actualizado = negocio.Modificar(Cliente);

            if (!actualizado)
                return NotFound();

            TempData["Mensaje"] =
                "Cliente actualizado correctamente.";

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
                "No fue posible actualizar el cliente."
            );

            return Page();
        }
    }
}