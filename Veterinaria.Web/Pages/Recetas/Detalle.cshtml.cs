using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Datos;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Recetas;

public class DetalleModel : PageModel
{
    private readonly RecetaNegocio _recetaNegocio = new();
    private readonly DetalleRecetaNegocio _detalleNegocio = new();
    private readonly ConsultaNegocio _consultaNegocio = new();
    private readonly MascotaNegocio _mascotaNegocio = new();
    private readonly ClienteNegocio _clienteNegocio = new();
    private readonly VeterinarioNegocio _veterinarioNegocio = new();
    private readonly MedicamentoDatos _medicamentoDatos = new();

    public Receta? Receta { get; set; }

    public Consulta? Consulta { get; set; }

    public Mascota? Mascota { get; set; }

    public Cliente? Cliente { get; set; }

    public Veterinario? Veterinario { get; set; }

    public List<DetalleReceta> Detalles { get; set; } = new();

    public List<Medicamento> Medicamentos { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        Receta = _recetaNegocio.BuscarPorId(id);

        if (Receta == null)
            return NotFound();

        Consulta = _consultaNegocio.BuscarPorId(Receta.IdConsulta);

        if (Consulta != null)
        {
            Mascota = _mascotaNegocio.BuscarPorId(Consulta.IdMascota);

            if (Mascota != null)
                Cliente = _clienteNegocio.BuscarPorId(Mascota.IdCliente);

            Veterinario = _veterinarioNegocio.BuscarPorId(
                Consulta.IdVeterinario);
        }

        Detalles = _detalleNegocio.ListarPorReceta(id);
        Medicamentos = _medicamentoDatos.Listar();

        return Page();
    }
}