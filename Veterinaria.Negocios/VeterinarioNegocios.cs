using Veterinaria.Datos;
using Veterinaria.Entidades;

namespace Veterinaria.Negocios;

public class VeterinarioNegocio
{
    private readonly VeterinarioDatos _datos = new();

    public List<Veterinario> Listar()
        => _datos.Listar();

    public List<Veterinario> Buscar(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return _datos.Listar();

        return _datos.Buscar(texto.Trim());
    }

    public Veterinario? BuscarPorId(int idVeterinario)
        => _datos.BuscarPorId(idVeterinario);

    public int Agregar(Veterinario veterinario)
    {
        Validar(veterinario);
        return _datos.Agregar(veterinario);
    }

    public bool Modificar(Veterinario veterinario)
    {
        if (veterinario.IdVeterinario <= 0)
            throw new ArgumentException("Veterinario no válido.");

        Validar(veterinario);
        return _datos.Modificar(veterinario);
    }

    public bool Eliminar(int idVeterinario)
    {
        if (idVeterinario <= 0)
            throw new ArgumentException("Veterinario no válido.");

        return _datos.Eliminar(idVeterinario);
    }

    private static void Validar(Veterinario veterinario)
    {
        if (string.IsNullOrWhiteSpace(veterinario.Nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(veterinario.ApellidoPaterno))
            throw new ArgumentException(
                "El apellido paterno es obligatorio.");

        if (string.IsNullOrWhiteSpace(veterinario.Especialidad))
            throw new ArgumentException(
                "La especialidad es obligatoria.");
    }
}