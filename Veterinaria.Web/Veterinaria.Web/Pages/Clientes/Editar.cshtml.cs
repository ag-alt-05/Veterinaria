
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Veterinaria.Web.Models;

namespace Veterinaria.Web.Pages.Clientes;

public class EditarModel : PageModel
{
    [BindProperty]
    public ClienteTemporal Cliente { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var encontrado = DatosTemporales.Clientes.FirstOrDefault(c => c.IdCliente == id);
        if (encontrado == null) return NotFound();

        Cliente = new ClienteTemporal
        {
            IdCliente = encontrado.IdCliente,
            Nombre = encontrado.Nombre,
            ApellidoPaterno = encontrado.ApellidoPaterno,
            ApellidoMaterno = encontrado.ApellidoMaterno,
            Telefono = encontrado.Telefono,
            Correo = encontrado.Correo,
            Direccion = encontrado.Direccion
        };

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var existente = DatosTemporales.Clientes.FirstOrDefault(
            c => c.IdCliente == Cliente.IdCliente);

        if (existente == null) return NotFound();

        existente.Nombre = Cliente.Nombre;
        existente.ApellidoPaterno = Cliente.ApellidoPaterno;
        existente.ApellidoMaterno = Cliente.ApellidoMaterno;
        existente.Telefono = Cliente.Telefono;
        existente.Correo = Cliente.Correo;
        existente.Direccion = Cliente.Direccion;

        TempData["Mensaje"] = "Cliente actualizado correctamente.";
        return RedirectToPage("./Index");
    }
}
