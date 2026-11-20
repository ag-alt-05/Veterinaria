using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Consultas;

public class EditarModel : PageModel
{
    private readonly ConsultaNegocio _consultaNegocio = new();
    private readonly MascotaNegocio _mascotaNegocio = new();
    private readonly VeterinarioNegocio _veterinarioNegocio = new();

    [BindProperty]
    public Consulta Consulta { get; set; } = new();

    public List<Mascota> Mascotas { get; set; } = new();

    public List<Veterinario> Veterinarios { get; set; } = new();

    public string? Error { get; set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            Consulta? encontrada = _consultaNegocio.BuscarPorId(id);

            if (encontrada == null)
                return NotFound();

            Consulta = encontrada;

            CargarListas();

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            return NotFound();
        }
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            CargarListas();
            return Page();
        }

        try
        {
            _consultaNegocio.Modificar(Consulta);

            TempData["Mensaje"] = "La consulta se actualizó correctamente.";

            return RedirectToPage("./Index");
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);

            CargarListas();

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible actualizar la consulta.";

            CargarListas();

            return Page();
        }
    }

    private void CargarListas()
    {
        Mascotas = _mascotaNegocio.Listar();
        Veterinarios = _veterinarioNegocio.Listar();
    }
}