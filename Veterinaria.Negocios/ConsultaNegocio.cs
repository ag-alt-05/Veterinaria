using Veterinaria.Datos;
using Veterinaria.Entidades;

namespace Veterinaria.Negocios;

public class ConsultaNegocio
{
    private readonly ConsultaDatos _datos = new();

    public List<Consulta> Listar()
        => _datos.Listar();

    public Consulta? BuscarPorId(int idConsulta)
        => _datos.BuscarPorId(idConsulta);

    public int Agregar(Consulta consulta)
    {
        Validar(consulta);

        return _datos.Agregar(consulta);
    }

    public bool Modificar(Consulta consulta)
    {
        if (consulta.IdConsulta <= 0)
            throw new ArgumentException("Consulta no válida.");

        Validar(consulta);

        return _datos.Modificar(consulta);
    }

    public bool Eliminar(int idConsulta)
    {
        if (idConsulta <= 0)
            throw new ArgumentException("Consulta no válida.");

        if (_datos.TieneReceta(idConsulta))
            throw new ReglaNegocioException(
                "No se puede eliminar esta consulta porque tiene una receta médica asociada.");

        return _datos.Eliminar(idConsulta);
    }

    private static void Validar(Consulta consulta)
    {
        if (consulta.IdMascota <= 0)
            throw new ArgumentException(
                "Debe seleccionar una mascota.");

        if (consulta.IdVeterinario <= 0)
            throw new ArgumentException(
                "Debe seleccionar un veterinario.");

        if (consulta.Fecha == default)
            throw new ArgumentException(
                "La fecha de consulta no es válida.");
    }
}