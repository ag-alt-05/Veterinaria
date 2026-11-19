using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Entidades;
using Veterinaria.Negocios;

namespace Veterinaria.Web.Pages.Mascotas;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int? IdCliente { get; set; }

    public List<Mascota> Mascotas { get; set; } = new();

    public Dictionary<int, string> Propietarios { get; set; } = new();

    public string? Error { get; set; }

    public void OnGet()
    {
        try
        {
            MascotaNegocio mascotaNegocio = new();
            ClienteNegocio clienteNegocio = new();

            // Obtener mascotas
            Mascotas = IdCliente.HasValue
                ? mascotaNegocio.ListarPorCliente(IdCliente.Value)
                : mascotaNegocio.Listar();

            // Obtener propietarios
            var clientes = clienteNegocio.Listar();

            Propietarios = clientes.ToDictionary(
                c => c.IdCliente,
                c => $"{c.Nombre} {c.ApellidoPaterno}"
            );
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);

            Error = "No fue posible consultar las mascotas. Verifique la conexión con MySQL.";
        }
    }

    public string NombrePropietario(int id)
    {
        return Propietarios.TryGetValue(id, out string? nombre)
            ? nombre
            : "Sin propietario";
    }
}