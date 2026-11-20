using Veterinaria.Datos;
using Veterinaria.Entidades;

namespace Veterinaria.Negocios;

public class DetalleRecetaNegocio
{
    private readonly DetalleRecetaDatos _datos = new();
    private readonly RecetaDatos _recetaDatos = new();
    private readonly MedicamentoDatos _medicamentoDatos = new();

    public List<DetalleReceta> ListarPorReceta(int idReceta)
    {
        if (idReceta <= 0)
            throw new ArgumentException("Receta no válida.");

        return _datos.ListarPorReceta(idReceta);
    }

    public int Agregar(DetalleReceta detalle)
    {
        Validar(detalle);

        return _datos.Agregar(detalle);
    }

    public bool Modificar(DetalleReceta detalle)
    {
        if (detalle.IdDetalle <= 0)
            throw new ArgumentException("Detalle de receta no válido.");

        Validar(detalle);

        return _datos.Modificar(detalle);
    }

    public bool Eliminar(int idDetalle)
    {
        if (idDetalle <= 0)
            throw new ArgumentException("Detalle de receta no válido.");

        return _datos.Eliminar(idDetalle);
    }

    public int EliminarPorReceta(int idReceta)
    {
        if (idReceta <= 0)
            throw new ArgumentException("Receta no válida.");

        return _datos.EliminarPorReceta(idReceta);
    }

    private void Validar(DetalleReceta detalle)
    {
        if (detalle.IdReceta <= 0)
            throw new ArgumentException("Debe seleccionar una receta.");

        if (_recetaDatos.BuscarPorId(detalle.IdReceta) == null)
            throw new ArgumentException("La receta seleccionada no existe.");

        if (detalle.IdMedicamento <= 0)
            throw new ArgumentException("Debe seleccionar un medicamento.");

        if (_medicamentoDatos.BuscarPorId(detalle.IdMedicamento) == null)
            throw new ArgumentException("El medicamento seleccionado no existe.");

        if (string.IsNullOrWhiteSpace(detalle.Dosis))
            throw new ArgumentException("La dosis es obligatoria.");
    }
}