using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class ConsultaDatos
{
    public List<Consulta> Listar()
    {
        List<Consulta> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_consulta, id_mascota, id_veterinario,
                   fecha, motivo_consulta, peso, temperatura,
                   sintomas, diagnostico, tratamiento, observaciones
            FROM consulta
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

    public Consulta? BuscarPorId(int idConsulta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_consulta, id_mascota, id_veterinario,
                   fecha, motivo_consulta, peso, temperatura,
                   sintomas, diagnostico, tratamiento, observaciones
            FROM consulta
            WHERE id_consulta = @idConsulta;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idConsulta", idConsulta);

        using MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return Mapear(reader);
        }

        return null;
    }

    public int Agregar(Consulta consulta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            INSERT INTO consulta
            (
                id_mascota,
                id_veterinario,
                fecha,
                motivo_consulta,
                peso,
                temperatura,
                sintomas,
                diagnostico,
                tratamiento,
                observaciones
            )
            VALUES
            (
                @idMascota,
                @idVeterinario,
                @fecha,
                @motivoConsulta,
                @peso,
                @temperatura,
                @sintomas,
                @diagnostico,
                @tratamiento,
                @observaciones
            );
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idMascota", consulta.IdMascota);
        cmd.Parameters.AddWithValue("@idVeterinario", consulta.IdVeterinario);
        cmd.Parameters.AddWithValue("@fecha", consulta.Fecha);
        cmd.Parameters.AddWithValue("@motivoConsulta",
            (object?)consulta.MotivoConsulta ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@peso",
            (object?)consulta.Peso ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@temperatura",
            (object?)consulta.Temperatura ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@sintomas",
            (object?)consulta.Sintomas ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@diagnostico",
            (object?)consulta.Diagnostico ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@tratamiento",
            (object?)consulta.Tratamiento ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@observaciones",
            (object?)consulta.Observaciones ?? DBNull.Value);

        cmd.ExecuteNonQuery();

        return Convert.ToInt32(cmd.LastInsertedId);
    }

    public bool Modificar(Consulta consulta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
        UPDATE consulta
        SET
            id_mascota = @idMascota,
            id_veterinario = @idVeterinario,
            fecha = @fecha,
            motivo_consulta = @motivoConsulta,
            peso = @peso,
            temperatura = @temperatura,
            sintomas = @sintomas,
            diagnostico = @diagnostico,
            tratamiento = @tratamiento,
            observaciones = @observaciones
        WHERE id_consulta = @idConsulta;
        """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idConsulta", consulta.IdConsulta);
        cmd.Parameters.AddWithValue("@idMascota", consulta.IdMascota);
        cmd.Parameters.AddWithValue("@idVeterinario", consulta.IdVeterinario);
        cmd.Parameters.AddWithValue("@fecha", consulta.Fecha);

        cmd.Parameters.AddWithValue("@motivoConsulta",
            (object?)consulta.MotivoConsulta ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@peso",
            (object?)consulta.Peso ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@temperatura",
            (object?)consulta.Temperatura ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@sintomas",
            (object?)consulta.Sintomas ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@diagnostico",
            (object?)consulta.Diagnostico ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@tratamiento",
            (object?)consulta.Tratamiento ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@observaciones",
            (object?)consulta.Observaciones ?? DBNull.Value);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool TieneReceta(int idConsulta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
        SELECT COUNT(*)
        FROM receta
        WHERE id_consulta = @idConsulta;
        """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idConsulta", idConsulta);

        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public bool Eliminar(int idConsulta)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
        DELETE FROM consulta
        WHERE id_consulta = @idConsulta;
        """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idConsulta", idConsulta);

        return cmd.ExecuteNonQuery() > 0;
    }

    private static Consulta Mapear(MySqlDataReader reader)
    {
        return new Consulta
        {
            IdConsulta = Convert.ToInt32(reader["id_consulta"]),
            IdMascota = Convert.ToInt32(reader["id_mascota"]),
            IdVeterinario = Convert.ToInt32(reader["id_veterinario"]),
            Fecha = Convert.ToDateTime(reader["fecha"]),

            MotivoConsulta = reader["motivo_consulta"] == DBNull.Value
                ? null
                : Convert.ToString(reader["motivo_consulta"]),

            Peso = reader["peso"] == DBNull.Value
                ? null
                : Convert.ToDecimal(reader["peso"]),

            Temperatura = reader["temperatura"] == DBNull.Value
                ? null
                : Convert.ToDecimal(reader["temperatura"]),

            Sintomas = reader["sintomas"] == DBNull.Value
                ? null
                : Convert.ToString(reader["sintomas"]),

            Diagnostico = reader["diagnostico"] == DBNull.Value
                ? null
                : Convert.ToString(reader["diagnostico"]),

            Tratamiento = reader["tratamiento"] == DBNull.Value
                ? null
                : Convert.ToString(reader["tratamiento"]),

            Observaciones = reader["observaciones"] == DBNull.Value
                ? null
                : Convert.ToString(reader["observaciones"])
        };
    }
}