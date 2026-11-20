using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class VeterinarioDatos
{
    public List<Veterinario> Listar()
    {
        List<Veterinario> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_veterinario, nombre, apellido_paterno,
                   apellido_materno, cedula, especialidad,
                   telefono, correo
            FROM veterinario
            ORDER BY apellido_paterno, nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        using MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            lista.Add(Mapear(dr));
        }

        return lista;
    }

    public Veterinario? BuscarPorId(int idVeterinario)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_veterinario, nombre, apellido_paterno,
                   apellido_materno, cedula, especialidad,
                   telefono, correo
            FROM veterinario
            WHERE id_veterinario = @idVeterinario;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idVeterinario", idVeterinario);

        using MySqlDataReader dr = cmd.ExecuteReader();

        return dr.Read() ? Mapear(dr) : null;
    }

    public List<Veterinario> Buscar(string texto)
    {
        List<Veterinario> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_veterinario, nombre, apellido_paterno,
                   apellido_materno, cedula, especialidad,
                   telefono, correo
            FROM veterinario
            WHERE nombre LIKE @texto
               OR apellido_paterno LIKE @texto
               OR apellido_materno LIKE @texto
               OR cedula LIKE @texto
               OR especialidad LIKE @texto
               OR telefono LIKE @texto
               OR correo LIKE @texto
            ORDER BY apellido_paterno, nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@texto", $"%{texto}%");

        using MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            lista.Add(Mapear(dr));
        }

        return lista;
    }

    public int Agregar(Veterinario veterinario)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            INSERT INTO veterinario
            (nombre, apellido_paterno, apellido_materno,
             cedula, especialidad, telefono, correo)
            VALUES
            (@nombre, @apellidoPaterno, @apellidoMaterno,
             @cedula, @especialidad, @telefono, @correo);
            """;

        using MySqlCommand cmd = new(sql, cn);

        AgregarParametros(cmd, veterinario);

        cmd.ExecuteNonQuery();

        return Convert.ToInt32(cmd.LastInsertedId);
    }

    public bool Modificar(Veterinario veterinario)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            UPDATE veterinario
            SET nombre = @nombre,
                apellido_paterno = @apellidoPaterno,
                apellido_materno = @apellidoMaterno,
                cedula = @cedula,
                especialidad = @especialidad,
                telefono = @telefono,
                correo = @correo
            WHERE id_veterinario = @idVeterinario;
            """;

        using MySqlCommand cmd = new(sql, cn);

        AgregarParametros(cmd, veterinario);

        cmd.Parameters.AddWithValue("@idVeterinario", veterinario.IdVeterinario);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Eliminar(int idVeterinario)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            DELETE FROM veterinario
            WHERE id_veterinario = @idVeterinario;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idVeterinario", idVeterinario);

        return cmd.ExecuteNonQuery() > 0;
    }

    private static void AgregarParametros(MySqlCommand cmd, Veterinario veterinario)
    {
        cmd.Parameters.AddWithValue("@nombre", veterinario.Nombre);

        cmd.Parameters.AddWithValue("@apellidoPaterno", veterinario.ApellidoPaterno);

        cmd.Parameters.AddWithValue("@apellidoMaterno",
            (object?)veterinario.ApellidoMaterno ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@cedula",
            (object?)veterinario.Cedula ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@especialidad", veterinario.Especialidad);

        cmd.Parameters.AddWithValue("@telefono",
            (object?)veterinario.Telefono ?? DBNull.Value);

        cmd.Parameters.AddWithValue("@correo",
            (object?)veterinario.Correo ?? DBNull.Value);
    }

    private static Veterinario Mapear(MySqlDataReader dr)
    {
        return new Veterinario
        {
            IdVeterinario = Convert.ToInt32(dr["id_veterinario"]),
            Nombre = Convert.ToString(dr["nombre"]) ?? "",
            ApellidoPaterno = Convert.ToString(dr["apellido_paterno"]) ?? "",

            ApellidoMaterno = dr["apellido_materno"] == DBNull.Value
                ? null
                : Convert.ToString(dr["apellido_materno"]),

            Cedula = dr["cedula"] == DBNull.Value
                ? null
                : Convert.ToString(dr["cedula"]),

            Especialidad = Convert.ToString(dr["especialidad"]) ?? "",

            Telefono = dr["telefono"] == DBNull.Value
                ? null
                : Convert.ToString(dr["telefono"]),

            Correo = dr["correo"] == DBNull.Value
                ? null
                : Convert.ToString(dr["correo"])
        };
    }
}