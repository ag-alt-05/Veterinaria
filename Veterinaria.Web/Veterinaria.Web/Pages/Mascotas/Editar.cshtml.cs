
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Mascotas;

public class EditarModel : PageModel
{
    [BindProperty]
    public MascotaTemporal Mascota { get; set; } = new();

    public List<SelectListItem> Clientes { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var m = DatosTemporales.Mascotas.FirstOrDefault(x => x.IdMascota == id);
        if (m == null) return NotFound();

        Mascota = new MascotaTemporal
        {
            IdMascota = m.IdMascota,
            IdCliente = m.IdCliente,
            Nombre = m.Nombre,
            Especie = m.Especie,
            Raza = m.Raza,
            Sexo = m.Sexo,
            Color = m.Color
        };

        CargarClientes();
        return Page();
    }

    public IActionResult OnPost()
    {
        CargarClientes();

        if (!DatosTemporales.Clientes.Any(c => c.IdCliente == Mascota.IdCliente))
            ModelState.AddModelError("Mascota.IdCliente", "Selecciona un propietario válido.");

        if (!ModelState.IsValid) return Page();

        var original = DatosTemporales.Mascotas.FirstOrDefault(
            x => x.IdMascota == Mascota.IdMascota);

        if (original == null) return NotFound();

        original.IdCliente = Mascota.IdCliente;
        original.Nombre = Mascota.Nombre;
        original.Especie = Mascota.Especie;
        original.Raza = Mascota.Raza;
        original.Sexo = Mascota.Sexo;
        original.Color = Mascota.Color;

        TempData["Mensaje"] = "Mascota actualizada correctamente.";
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
