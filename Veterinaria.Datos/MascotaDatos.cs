using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class MascotaDatos
{
    public List<Mascota> Listar()
    {
        List<Mascota> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_mascota, id_cliente, nombre, especie, raza, sexo, 
                   fecha_nacimiento, color, senas_particulares,
                   edad_aproximada, tamano, esterilizado, vacunas, alergias, observaciones
            FROM mascota
            ORDER BY nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        using MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
            lista.Add(Mapear(dr));

        return lista;
    }

    public List<Mascota> ListarPorCliente(int idCliente)
    {
        List<Mascota> lista = new();

        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_mascota, id_cliente, nombre, especie, raza, sexo, 
                   fecha_nacimiento, color, senas_particulares,
                   edad_aproximada, tamano, esterilizado, vacunas, alergias, observaciones
            FROM mascota
            WHERE id_cliente = @idCliente
            ORDER BY nombre;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);

        using MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
            lista.Add(Mapear(dr));

        return lista;
    }

    public Mascota? BuscarPorId(int idMascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            SELECT id_mascota, id_cliente, nombre, especie, raza, sexo, 
                   fecha_nacimiento, color, senas_particulares,
                   edad_aproximada, tamano, esterilizado, vacunas, alergias, observaciones
            FROM mascota
            WHERE id_mascota = @idMascota;
            """;

        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idMascota", idMascota);

        using MySqlDataReader dr = cmd.ExecuteReader();

        return dr.Read() ? Mapear(dr) : null;
    }

    public int Agregar(Mascota mascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            INSERT INTO mascota
            (id_cliente, nombre, especie, raza, sexo, fecha_nacimiento, 
             color, senas_particulares, edad_aproximada, tamano, esterilizado, 
             vacunas, alergias, observaciones)
            VALUES
            (@idCliente, @nombre, @especie, @raza, @sexo, @fechaNacimiento,
             @color, @senasParticulares, @edadAproximada, @tamano, @esterilizado,
             @vacunas, @alergias, @observaciones);
            """;

        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, mascota);

        cmd.ExecuteNonQuery();
        return Convert.ToInt32(cmd.LastInsertedId);
    }

    public bool Modificar(Mascota mascota)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();

        const string sql = """
            UPDATE mascota
            SET id_cliente = @idCliente,
                nombre = @nombre,
                especie = @especie,
                raza = @raza,
                sexo = @sexo,
                fecha_nacimiento = @fechaNacimiento,
                color = @color,
                senas_particulares = @senasParticulares,
                edad_aproximada = @edadAproximada,
                tamano = @tamano,
                esterilizado = @esterilizado,
                vacunas = @vacunas,
                alergias = @alergias,
                observaciones = @observaciones
            WHERE id_mascota = @idMascota;
            """;

        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, mascota);
        cmd.Parameters.AddWithValue("@idMascota", mascota.IdMascota);

        return cmd.ExecuteNonQuery() > 0;
    }

    private static void AgregarParametros(
        MySqlCommand cmd,
        Mascota mascota)
    {
        cmd.Parameters.AddWithValue("@idCliente", mascota.IdCliente);
        cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
        cmd.Parameters.AddWithValue("@especie", mascota.Especie);

        cmd.Parameters.AddWithValue(
            "@raza", (object?)mascota.Raza ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@sexo", (object?)mascota.Sexo ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@fechaNacimiento",
            (object?)mascota.FechaNacimiento ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@color", (object?)mascota.Color ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@senasParticulares",
            (object?)mascota.SenasParticulares ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@edadAproximada",
            (object?)mascota.EdadAproximada ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@tamano",
            (object?)mascota.Tamano ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@esterilizado",
            (object?)mascota.Esterilizado ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@vacunas",
            (object?)mascota.Vacunas ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@alergias",
            (object?)mascota.Alergias ?? DBNull.Value);

        cmd.Parameters.AddWithValue(
            "@observaciones",
            (object?)mascota.Observaciones ?? DBNull.Value);
    }

    private static Mascota Mapear(MySqlDataReader dr)
    {
        return new Mascota
        {
            IdMascota = Convert.ToInt32(dr["id_mascota"]),
            IdCliente = Convert.ToInt32(dr["id_cliente"]),
            Nombre = Convert.ToString(dr["nombre"]) ?? "",
            Especie = Convert.ToString(dr["especie"]) ?? "",
            Raza = dr["raza"] == DBNull.Value ? null : Convert.ToString(dr["raza"]),
            Sexo = dr["sexo"] == DBNull.Value ? null : Convert.ToString(dr["sexo"]),
            FechaNacimiento = dr["fecha_nacimiento"] == DBNull.Value ? null : Convert.ToDateTime(dr["fecha_nacimiento"]),
            Color = dr["color"] == DBNull.Value ? null : Convert.ToString(dr["color"]),
            SenasParticulares = dr["senas_particulares"] == DBNull.Value ? null : Convert.ToString(dr["senas_particulares"]),

            EdadAproximada = dr["edad_aproximada"] == DBNull.Value ? null : Convert.ToString(dr["edad_aproximada"]),
            Tamano = dr["tamano"] == DBNull.Value ? null : Convert.ToString(dr["tamano"]),
            Esterilizado = dr["esterilizado"] == DBNull.Value ? null : Convert.ToString(dr["esterilizado"]),
            Vacunas = dr["vacunas"] == DBNull.Value ? null : Convert.ToString(dr["vacunas"]),
            Alergias = dr["alergias"] == DBNull.Value ? null : Convert.ToString(dr["alergias"]),
            Observaciones = dr["observaciones"] == DBNull.Value ? null : Convert.ToString(dr["observaciones"])
        };
    }
}