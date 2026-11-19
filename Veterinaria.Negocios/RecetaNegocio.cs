using Veterinaria.Datos;
using Veterinaria.Entidades;

namespace Veterinaria.Negocios;

public class RecetaNegocio
{
    private readonly RecetaDatos _datos = new();
    private readonly ConsultaDatos _consultaDatos = new();

    public List<Receta> Listar()
        => _datos.Listar();

    public Receta? BuscarPorId(int idReceta)
        => _datos.BuscarPorId(idReceta);

    public int Agregar(Receta receta)
    {
        Validar(receta);

        if (_datos.Listar().Exists(r => r.IdConsulta == receta.IdConsulta))
        {
            throw new ReglaNegocioException(
                "Esta consulta ya tiene una receta médica registrada.");
        }

        return _datos.Agregar(receta);
    }

    public bool Modificar(Receta receta)
    {
        if (receta.IdReceta <= 0)
            throw new ArgumentException("Receta no válida.");

        Validar(receta);

        if (_datos.Listar().Exists(r =>
            r.IdConsulta == receta.IdConsulta &&
            r.IdReceta != receta.IdReceta))
        {
            throw new ReglaNegocioException(
                "Esta consulta ya tiene otra receta médica registrada.");
        }

        return _datos.Modificar(receta);
    }

    public bool Eliminar(int idReceta)
    {
        if (idReceta <= 0)
            throw new ArgumentException("Receta no válida.");

        if (_datos.BuscarPorId(idReceta) == null)
            throw new ArgumentException("La receta no existe.");

        return _datos.Eliminar(idReceta);
    }

    private void Validar(Receta receta)
    {
        if (receta.IdConsulta <= 0)
            throw new ArgumentException("Debe seleccionar una consulta.");

        if (_consultaDatos.BuscarPorId(receta.IdConsulta) == null)
        {
            throw new ArgumentException(
                "La consulta seleccionada no existe.");
        }
    }
}