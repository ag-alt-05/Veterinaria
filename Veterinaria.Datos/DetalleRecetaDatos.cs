using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class DetalleRecetaDatos
{
    public List<DetalleReceta> ListarPorReceta(int idReceta)
    {
        List<DetalleReceta> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT
                id_detalle,
                id_receta,
                id_medicamento,
                dosis,
                frecuencia,
                duracion,
                via_administracion,
                indicaciones
            FROM detalle_receta
            WHERE id_receta = @idReceta
            ORDER BY id_detalle;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idReceta", idReceta);

        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(Mapear(reader));
        }

        return lista;
    }

    public int Agregar(DetalleReceta detalle)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            INSERT INTO detalle_receta
            (
                id_receta,
                id_medicamento,
                dosis,
                frecuencia,
                duracion,
                via_administracion,
                indicaciones
            )
            VALUES
            (
                @idReceta,
                @idMedicamento,
                @dosis,
                @frecuencia,
                @duracion,
                @viaAdministracion,
                @indicaciones
            );

            SELECT LAST_INSERT_ID();
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idReceta", detalle.IdReceta);
        cmd.Parameters.AddWithValue("@idMedicamento", detalle.IdMedicamento);
        cmd.Parameters.AddWithValue("@dosis", detalle.Dosis);

        cmd.Parameters.AddWithValue("@frecuencia",
            (object?)detalle.Frecuencia ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@duracion",
            (object?)detalle.Duracion ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@viaAdministracion",
            (object?)detalle.ViaAdministracion ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@indicaciones",
            (object?)detalle.Indicaciones ?? DBNull.Value);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public bool Modificar(DetalleReceta detalle)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            UPDATE detalle_receta
            SET
                id_medicamento = @idMedicamento,
                dosis = @dosis,
                frecuencia = @frecuencia,
                duracion = @duracion,
                via_administracion = @viaAdministracion,
                indicaciones = @indicaciones
            WHERE id_detalle = @idDetalle;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idDetalle", detalle.IdDetalle);
        cmd.Parameters.AddWithValue("@idMedicamento", detalle.IdMedicamento);
        cmd.Parameters.AddWithValue("@dosis", detalle.Dosis);

        cmd.Parameters.AddWithValue("@frecuencia",
            (object?)detalle.Frecuencia ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@duracion",
            (object?)detalle.Duracion ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@viaAdministracion",
            (object?)detalle.ViaAdministracion ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@indicaciones",
            (object?)detalle.Indicaciones ?? DBNull.Value);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int idDetalle)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            DELETE FROM detalle_receta
            WHERE id_detalle = @idDetalle;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idDetalle", idDetalle);

        return cmd.ExecuteNonQuery() > 0;
    }

    public int EliminarPorReceta(int idReceta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            DELETE FROM detalle_receta
            WHERE id_receta = @idReceta;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idReceta", idReceta);

        return cmd.ExecuteNonQuery();
    }

    private static DetalleReceta Mapear(MySqlDataReader reader)
    {
        return new DetalleReceta
        {
            IdDetalle = reader.GetInt32("id_detalle"),
            IdReceta = reader.GetInt32("id_receta"),
            IdMedicamento = reader.GetInt32("id_medicamento"),
            Dosis = reader.GetString("dosis"),

            Frecuencia = reader.IsDBNull(reader.GetOrdinal("frecuencia"))
                ? null
                : reader.GetString("frecuencia"),

            Duracion = reader.IsDBNull(reader.GetOrdinal("duracion"))
                ? null
                : reader.GetString("duracion"),

            ViaAdministracion = reader.IsDBNull(
                reader.GetOrdinal("via_administracion"))
                ? null
                : reader.GetString("via_administracion"),

            Indicaciones = reader.IsDBNull(reader.GetOrdinal("indicaciones"))
                ? null
                : reader.GetString("indicaciones")
        };
    }
}