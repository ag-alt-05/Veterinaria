
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Mascotas;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int? IdCliente { get; set; }

    public List<MascotaTemporal> Mascotas { get; set; } = new();

    public void OnGet()
    {
        Mascotas = DatosTemporales.Mascotas;

        if (IdCliente.HasValue)
            Mascotas = Mascotas.Where(m => m.IdCliente == IdCliente.Value).ToList();
    }

    public string NombrePropietario(int id)
    {
        var cliente = DatosTemporales.Clientes.FirstOrDefault(c => c.IdCliente == id);
        return cliente == null ? "Sin propietario" :
            $"{cliente.Nombre} {cliente.ApellidoPaterno}";
    }
}
