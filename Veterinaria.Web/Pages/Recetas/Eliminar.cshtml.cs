using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Recetas;

public class EliminarModel : PageModel
{
    private readonly RecetaNegocio _recetaNegocio = new();
    private readonly DetalleRecetaNegocio _detalleNegocio = new();
    private readonly ConsultaNegocio _consultaNegocio = new();
    private readonly MascotaNegocio _mascotaNegocio = new();

    public Receta Receta { get; private set; } = new();

    public Consulta? Consulta { get; private set; }

    public Mascota? Mascota { get; private set; }

    public string? Error { get; private set; }

    public bool TieneDetalles { get; private set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            if (!CargarReceta(id))
                return NotFound();

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible consultar la receta.";
            return Page();
        }
    }

    public IActionResult OnPost(int id)
    {
        try
        {
            if (!CargarReceta(id))
                return NotFound();

            bool eliminado = _recetaNegocio.Eliminar(id);

            if (!eliminado)
                return NotFound();

            TempData["Mensaje"] = "Receta y medicamentos asociados eliminados correctamente.";

            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = ex.Message;
            return Page();
        }
    }

    private bool CargarReceta(int id)
    {
        Receta? receta = _recetaNegocio.BuscarPorId(id);

        if (receta == null)
            return false;

        Receta = receta;

        Consulta = _consultaNegocio.BuscarPorId(Receta.IdConsulta);

        if (Consulta != null)
        {
            Mascota = _mascotaNegocio.BuscarPorId(Consulta.IdMascota);
        }

        TieneDetalles = _detalleNegocio.ListarPorReceta(id).Any();

        return true;
    }
}