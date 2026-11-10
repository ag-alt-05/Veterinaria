using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Mascotas;

public class NuevaModel : PageModel
{
    [BindProperty]
    public Mascota Mascota { get; set; } = new();

    public List<SelectListItem> Clientes { get; set; } = new();

    public string? Error { get; set; }

    public void OnGet()
    {
        CargarClientes();
    }

    public IActionResult OnPost()
    {
        CargarClientes();

        if (!string.IsNullOrWhiteSpace(Error))
            return Page();

        // Comprobar que el propietario exista
        if (!Clientes.Any(c => c.Value == Mascota.IdCliente.ToString()))
        {
            ModelState.AddModelError(
                "Mascota.IdCliente",
                "Selecciona un propietario válido."
            );
        }

        if (!ModelState.IsValid)
            return Page();

        try
        {
            MascotaNegocio negocio = new();

            int id = negocio.Agregar(Mascota);

            TempData["Mensaje"] =
                $"Mascota registrada correctamente. ID: {id}";

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
                "No fue posible registrar la mascota."
            );

            return Page();
        }
    }

    private void CargarClientes()
    {
        try
        {
            ClienteNegocio negocio = new();

            Clientes = negocio.Listar().Select(c =>
                new SelectListItem(
                    $"{c.Nombre} {c.ApellidoPaterno}",
                    c.IdCliente.ToString()
                )).ToList();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible cargar los propietarios registrados.";
        }
    }
}