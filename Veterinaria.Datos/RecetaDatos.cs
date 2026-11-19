using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class RecetaDatos
{
    public List<Receta> Listar()
    {
        List<Receta> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_receta, id_consulta, fecha, indicaciones_generales
            FROM receta
            ORDER BY fecha DESC;
            """;

        using MySqlCommand cmd = new(sql, cn);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(Mapear(reader));
        }

        return lista;
    }

    public Receta? BuscarPorId(int idReceta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_receta, id_consulta, fecha, indicaciones_generales
            FROM receta
            WHERE id_receta = @idReceta;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idReceta", idReceta);

        using MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
            return Mapear(reader);

        return null;
    }

    public int Agregar(Receta receta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            INSERT INTO receta
            (
                id_consulta,
                fecha,
                indicaciones_generales
            )
            VALUES
            (
                @idConsulta,
                @fecha,
                @indicacionesGenerales
            );

            SELECT LAST_INSERT_ID();
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idConsulta", receta.IdConsulta);
        cmd.Parameters.AddWithValue("@fecha", receta.Fecha);

        cmd.Parameters.AddWithValue("@indicacionesGenerales",
            (object?)receta.IndicacionesGenerales ?? DBNull.Value);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool Modificar(Receta receta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            UPDATE receta
            SET
                id_consulta = @idConsulta,
                fecha = @fecha,
                indicaciones_generales = @indicacionesGenerales
            WHERE id_receta = @idReceta;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idReceta", receta.IdReceta);
        cmd.Parameters.AddWithValue("@idConsulta", receta.IdConsulta);
        cmd.Parameters.AddWithValue("@fecha", receta.Fecha);

        cmd.Parameters.AddWithValue("@indicacionesGenerales",
            (object?)receta.IndicacionesGenerales ?? DBNull.Value);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool TieneDetalles(int idReceta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT COUNT(*)
            FROM detalle_receta
            WHERE id_receta = @idReceta;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idReceta", idReceta);

        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public bool Eliminar(int idReceta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        using MySqlTransaction transaccion = cn.BeginTransaction();

        try
        {
            // Primero eliminamos los medicamentos asociados.
            const string sqlDetalles = """
            DELETE FROM detalle_receta
            WHERE id_receta = @idReceta;
            """;

            using (MySqlCommand cmdDetalles = new(sqlDetalles, cn, transaccion))
            {
                cmdDetalles.Parameters.AddWithValue("@idReceta", idReceta);
                cmdDetalles.ExecuteNonQuery();
            }

            // Después eliminamos la receta.
            const string sqlReceta = """
            DELETE FROM receta
            WHERE id_receta = @idReceta;
            """;

            int filasAfectadas;

            using (MySqlCommand cmdReceta = new(sqlReceta, cn, transaccion))
            {
                cmdReceta.Parameters.AddWithValue("@idReceta", idReceta);
                filasAfectadas = cmdReceta.ExecuteNonQuery();
            }

            transaccion.Commit();

            return filasAfectadas > 0;
        }
        catch
        {
            transaccion.Rollback();
            throw;
        }
    }

    private static Receta Mapear(MySqlDataReader reader)
    {
        return new Receta
        {
            IdReceta = reader.GetInt32("id_receta"),
            IdConsulta = reader.GetInt32("id_consulta"),
            Fecha = reader.GetDateTime("fecha"),

            IndicacionesGenerales = reader.IsDBNull(
                reader.GetOrdinal("indicaciones_generales"))
                ? null
                : reader.GetString("indicaciones_generales")
        };
    }
}