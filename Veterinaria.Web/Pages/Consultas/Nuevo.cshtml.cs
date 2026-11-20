using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Consultas;

public class NuevoModel : PageModel
{
    private readonly ConsultaNegocio _consultaNegocio = new();
    private readonly MascotaNegocio _mascotaNegocio = new();
    private readonly VeterinarioNegocio _veterinarioNegocio = new();

    [BindProperty]
    public Consulta Consulta { get; set; } = new();

    public List<Mascota> Mascotas { get; set; } = new();

    public List<Veterinario> Veterinarios { get; set; } = new();

    public string? Error { get; set; }

    public void OnGet()
    {
        Consulta.Fecha = DateTime.Now;

        CargarListas();
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
            _consultaNegocio.Agregar(Consulta);

            TempData["Mensaje"] = "La consulta se registró correctamente.";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
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