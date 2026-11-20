using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Consultas;

public class IndexModel : PageModel
{
    private readonly ConsultaNegocio _consultaNegocio = new();
    private readonly MascotaNegocio _mascotaNegocio = new();
    private readonly VeterinarioNegocio _veterinarioNegocio = new();

    public List<Consulta> Consultas { get; set; } = new();

    public List<Mascota> Mascotas { get; set; } = new();

    public List<Veterinario> Veterinarios { get; set; } = new();

    public string Buscar { get; set; } = string.Empty;

    public void OnGet(string? buscar)
    {
        Buscar = buscar?.Trim() ?? string.Empty;

        Consultas = _consultaNegocio.Listar();
        Mascotas = _mascotaNegocio.Listar();
        Veterinarios = _veterinarioNegocio.Listar();

        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            var texto = Buscar.ToLower();

            Consultas = Consultas.Where(c =>
            {
                var mascota = Mascotas.FirstOrDefault(
                    m => m.IdMascota == c.IdMascota);

                var veterinario = Veterinarios.FirstOrDefault(
                    v => v.IdVeterinario == c.IdVeterinario);

                return
                    (mascota != null &&
                     mascota.Nombre.ToLower().Contains(texto)) ||

                    (veterinario != null &&
                     (veterinario.Nombre + " " +
                      veterinario.ApellidoPaterno).ToLower().Contains(texto)) ||

                    (c.MotivoConsulta ?? "").ToLower().Contains(texto) ||

                    (c.Diagnostico ?? "").ToLower().Contains(texto);
            }).ToList();
        }
    }
}