using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Datos;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Recetas;

public class NuevoModel : PageModel
{
    private readonly RecetaNegocio _recetaNegocio = new();
    private readonly DetalleRecetaNegocio _detalleNegocio = new();
    private readonly ConsultaNegocio _consultaNegocio = new();
    private readonly MascotaNegocio _mascotaNegocio = new();
    private readonly VeterinarioNegocio _veterinarioNegocio = new();
    private readonly MedicamentoDatos _medicamentoDatos = new();

    [BindProperty]
    public Receta Receta { get; set; } = new();

    [BindProperty]
    public DetalleReceta Detalle { get; set; } = new();

    public List<Consulta> Consultas { get; set; } = new();

    public List<Mascota> Mascotas { get; set; } = new();

    public List<Veterinario> Veterinarios { get; set; } = new();

    public List<Medicamento> Medicamentos { get; set; } = new();

    public void OnGet()
    {
        CargarListas();
        Receta.Fecha = DateTime.Now;
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
            Receta.Fecha = Receta.Fecha == default
                ? DateTime.Now
                : Receta.Fecha;

            int idReceta = _recetaNegocio.Agregar(Receta);

            Detalle.IdReceta = idReceta;

            _detalleNegocio.Agregar(Detalle);

            TempData["Mensaje"] = "La receta se registró correctamente.";

            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            CargarListas();
            return Page();
        }
    }

    private void CargarListas()
    {
        List<Receta> recetas = _recetaNegocio.Listar();

        List<int> consultasConReceta = recetas
            .Select(r => r.IdConsulta)
            .ToList();

        Consultas = _consultaNegocio.Listar()
            .Where(c => !consultasConReceta.Contains(c.IdConsulta))
            .ToList();

        Mascotas = _mascotaNegocio.Listar();
        Veterinarios = _veterinarioNegocio.Listar();
        Medicamentos = _medicamentoDatos.Listar();
    }
}