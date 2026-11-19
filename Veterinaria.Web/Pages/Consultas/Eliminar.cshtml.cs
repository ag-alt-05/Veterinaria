using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Consultas;

public class EliminarModel : PageModel
{
    public Consulta Consulta { get; private set; } = new();

    public Mascota Mascota { get; private set; } = new();

    public Veterinario Veterinario { get; private set; } = new();

    public string Propietario { get; private set; } = "";

    public string? Error { get; private set; }

    public IActionResult OnGet(int id)
    {
        try
        {
            return Cargar(id);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible consultar la información de la consulta.";

            return Page();
        }
    }

    public IActionResult OnPost(int id)
    {
        try
        {
            IActionResult resultado = Cargar(id);

            if (resultado is NotFoundResult)
                return resultado;

            ConsultaNegocio negocio = new();

            bool eliminada = negocio.Eliminar(id);

            if (!eliminada)
                return NotFound();

            TempData["Mensaje"] = "Consulta eliminada correctamente.";

            return RedirectToPage("./Index");
        }
        catch (ReglaNegocioException ex)
        {
            Error = ex.Message;

            return Page();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible eliminar la consulta.";

            return Page();
        }
    }

    private IActionResult Cargar(int id)
    {
        ConsultaNegocio consultaNegocio = new();

        Consulta? consulta = consultaNegocio.BuscarPorId(id);

        if (consulta is null)
            return NotFound();

        Consulta = consulta;

        MascotaNegocio mascotaNegocio = new();

        Mascota? mascota = mascotaNegocio.BuscarPorId(consulta.IdMascota);

        if (mascota is null)
            return NotFound();

        Mascota = mascota;

        ClienteNegocio clienteNegocio = new();

        Cliente? cliente = clienteNegocio.BuscarPorId(mascota.IdCliente);

        Propietario = cliente == null
            ? "Sin propietario"
            : $"{cliente.Nombre} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno}";

        VeterinarioNegocio veterinarioNegocio = new();

        Veterinario? veterinario =
            veterinarioNegocio.BuscarPorId(consulta.IdVeterinario);

        if (veterinario is null)
            return NotFound();

        Veterinario = veterinario;

        return Page();
    }
}