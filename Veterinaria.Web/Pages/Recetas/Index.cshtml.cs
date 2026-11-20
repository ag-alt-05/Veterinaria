using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Recetas;

public class IndexModel : PageModel
{
    private readonly RecetaNegocio _recetaNegocio = new();
    private readonly ConsultaNegocio _consultaNegocio = new();
    private readonly MascotaNegocio _mascotaNegocio = new();
    private readonly VeterinarioNegocio _veterinarioNegocio = new();

    public List<Receta> Recetas { get; set; } = new();

    public List<Consulta> Consultas { get; set; } = new();

    public List<Mascota> Mascotas { get; set; } = new();

    public List<Veterinario> Veterinarios { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Buscar { get; set; }

    public void OnGet()
    {
        Recetas = _recetaNegocio.Listar();
        Consultas = _consultaNegocio.Listar();
        Mascotas = _mascotaNegocio.Listar();
        Veterinarios = _veterinarioNegocio.Listar();

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            string texto = Buscar.Trim().ToLower();

            Recetas = Recetas.Where(receta =>
            {
                Consulta? consulta = Consultas.FirstOrDefault(
                    c => c.IdConsulta == receta.IdConsulta);

                if (consulta == null)
                    return false;

                Mascota? mascota = Mascotas.FirstOrDefault(
                    m => m.IdMascota == consulta.IdMascota);

                Veterinario? veterinario = Veterinarios.FirstOrDefault(
                    v => v.IdVeterinario == consulta.IdVeterinario);

                string nombreVeterinario = veterinario == null
                    ? string.Empty
                    : $"{veterinario.Nombre} {veterinario.ApellidoPaterno}";

                return receta.IdReceta.ToString().Contains(texto)
                    || receta.IdConsulta.ToString().Contains(texto)
                    || (mascota?.Nombre.ToLower().Contains(texto) ?? false)
                    || nombreVeterinario.ToLower().Contains(texto)
                    || (receta.IndicacionesGenerales?.ToLower().Contains(texto) ?? false);
            }).ToList();
        }
    }
}