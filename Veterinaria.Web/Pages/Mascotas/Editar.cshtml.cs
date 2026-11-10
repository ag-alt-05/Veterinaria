using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Mascotas;

public class EditarModel : PageModel
{
    [BindProperty]
    public Mascota Mascota { get; set; } = new();

    public List<SelectListItem> Clientes { get; set; } = new();

    public string? Error { get; set; }

    public IActionResult OnGet(int id)
    {
        CargarClientes();

        if (!string.IsNullOrWhiteSpace(Error))
            return Page();

        try
        {
            MascotaNegocio negocio = new();

            Mascota? encontrada = negocio.BuscarPorId(id);

            if (encontrada == null)
                return NotFound();

            Mascota = encontrada;

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible cargar los datos de la mascota.";
            return Page();
        }
    }

    public IActionResult OnPost()
    {
        CargarClientes();

        if (!string.IsNullOrWhiteSpace(Error))
            return Page();

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

            bool actualizado = negocio.Modificar(Mascota);

            if (!actualizado)
                return NotFound();

            TempData["Mensaje"] =
                "Mascota actualizada correctamente.";

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
                "No fue posible actualizar la mascota."
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