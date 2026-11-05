
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Mascotas;

public class NuevaModel : PageModel
{
    [BindProperty]
    public MascotaTemporal Mascota { get; set; } = new();

    public List<SelectListItem> Clientes { get; set; } = new();

    public void OnGet() => CargarClientes();

    public IActionResult OnPost()
    {
        CargarClientes();

        if (!DatosTemporales.Clientes.Any(c => c.IdCliente == Mascota.IdCliente))
            ModelState.AddModelError("Mascota.IdCliente", "Selecciona un propietario válido.");

        if (!ModelState.IsValid) return Page();

        Mascota.IdMascota = DatosTemporales.Mascotas.Any()
            ? DatosTemporales.Mascotas.Max(m => m.IdMascota) + 1 : 1;

        DatosTemporales.Mascotas.Add(Mascota);

        TempData["Mensaje"] = "Mascota registrada correctamente.";
        return RedirectToPage("./Index");
    }

    private void CargarClientes()
    {
        Clientes = DatosTemporales.Clientes.Select(c =>
            new SelectListItem(
                $"{c.Nombre} {c.ApellidoPaterno}",
                c.IdCliente.ToString()
            )).ToList();
    }
}
