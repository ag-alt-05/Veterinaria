using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class MedicamentoDatos
{
    public List<Medicamento> Listar()
    {
        List<Medicamento> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT
                id_medicamento,
                nombre,
                presentacion,
                concentracion
            FROM medicamento
            ORDER BY nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(Mapear(reader));
        }

        return lista;
    }

    public Medicamento? BuscarPorId(int idMedicamento)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT
                id_medicamento,
                nombre,
                presentacion,
                concentracion
            FROM medicamento
            WHERE id_medicamento = @idMedicamento;
            """;

        using MySqlCommand cmd = new(sql, cn);

        cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);

        using MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
            return Mapear(reader);

        return null;
    }

    private static Medicamento Mapear(MySqlDataReader reader)
    {
        return new Medicamento
        {
            IdMedicamento = reader.GetInt32("id_medicamento"),
            Nombre = reader.GetString("nombre"),

            Presentacion = reader.IsDBNull(reader.GetOrdinal("presentacion"))
                ? null
                : reader.GetString("presentacion"),

            Concentracion = reader.IsDBNull(reader.GetOrdinal("concentracion"))
                ? null
                : reader.GetString("concentracion")
        };
    }
}