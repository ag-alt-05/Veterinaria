using MySql.Data.MySqlClient;
using Veterinaria.Entidades;

namespace Veterinaria.Datos;

public class ClienteDatos
{
    public List<Cliente> Listar()
    {
        List<Cliente> lista = new();
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();
        const string sql = """
            SELECT id_cliente, nombre, apellido_paterno, apellido_materno,
                   telefono, telefono_alternativo, correo, direccion, colonia, ciudad,
                   estado, codigo_postal, contacto_emergencia, parentesco_emergencia,
                   telefono_emergencia, observaciones, fecha_registro
            FROM cliente ORDER BY apellido_paterno, nombre;
            """;
        using MySqlCommand cmd = new(sql, cn);
        using MySqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read()) lista.Add(Mapear(dr));
        return lista;
    }

    public Cliente? BuscarPorId(int idCliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();
        const string sql = """
            SELECT id_cliente, nombre, apellido_paterno, apellido_materno,
                   telefono, telefono_alternativo, correo, direccion, colonia, ciudad,
                   estado, codigo_postal, contacto_emergencia, parentesco_emergencia,
                   telefono_emergencia, observaciones, fecha_registro
            FROM cliente WHERE id_cliente = @idCliente;
            """;
        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);
        using MySqlDataReader dr = cmd.ExecuteReader();
        return dr.Read() ? Mapear(dr) : null;
    }

    public List<Cliente> Buscar(string texto)
    {
        List<Cliente> lista = new();
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();
        const string sql = """
            SELECT id_cliente, nombre, apellido_paterno, apellido_materno,
                   telefono, telefono_alternativo, correo, direccion, colonia, ciudad,
                   estado, codigo_postal, contacto_emergencia, parentesco_emergencia,
                   telefono_emergencia, observaciones, fecha_registro
            FROM cliente WHERE nombre LIKE @texto OR apellido_paterno LIKE @texto
               OR apellido_materno LIKE @texto OR telefono LIKE @texto OR correo LIKE @texto
            ORDER BY apellido_paterno, nombre;
            """;
        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@texto", $"%{texto}%");
        using MySqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read()) lista.Add(Mapear(dr));
        return lista;
    }

    public int Agregar(Cliente cliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();
        const string sql = """
            INSERT INTO cliente
            (nombre, apellido_paterno, apellido_materno, telefono, telefono_alternativo, correo,
             direccion, colonia, ciudad, estado, codigo_postal, contacto_emergencia,
             parentesco_emergencia, telefono_emergencia, observaciones)
            VALUES
            (@nombre, @apellidoPaterno, @apellidoMaterno, @telefono, @telefonoAlternativo, @correo,
             @direccion, @colonia, @ciudad, @estado, @codigoPostal, @contactoEmergencia,
             @parentescoEmergencia, @telefonoEmergencia, @observaciones);
            """;
        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, cliente);
        cmd.ExecuteNonQuery();
        return Convert.ToInt32(cmd.LastInsertedId);
    }

    public bool Modificar(Cliente cliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();
        const string sql = """
            UPDATE cliente
            SET nombre = @nombre, apellido_paterno = @apellidoPaterno, apellido_materno = @apellidoMaterno,
                telefono = @telefono, telefono_alternativo = @telefonoAlternativo, correo = @correo,
                direccion = @direccion, colonia = @colonia, ciudad = @ciudad, estado = @estado,
                codigo_postal = @codigoPostal, contacto_emergencia = @contactoEmergencia,
                parentesco_emergencia = @parentescoEmergencia, telefono_emergencia = @telefonoEmergencia,
                observaciones = @observaciones
            WHERE id_cliente = @idCliente;
            """;
        using MySqlCommand cmd = new(sql, cn);
        AgregarParametros(cmd, cliente);
        cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool TieneMascotas(int idCliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();
        const string sql = "SELECT COUNT(*) FROM mascota WHERE id_cliente = @idCliente;";
        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public bool Eliminar(int idCliente)
    {
        using MySqlConnection cn = Conexion.Crear();
        cn.Open();
        const string sql = "DELETE FROM cliente WHERE id_cliente = @idCliente;";
        using MySqlCommand cmd = new(sql, cn);
        cmd.Parameters.AddWithValue("@idCliente", idCliente);
        return cmd.ExecuteNonQuery() > 0;
    }

    private static void AgregarParametros(MySqlCommand cmd, Cliente cliente)
    {
        cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
        cmd.Parameters.AddWithValue("@apellidoPaterno", cliente.ApellidoPaterno);
        cmd.Parameters.AddWithValue("@apellidoMaterno", (object?)cliente.ApellidoMaterno ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
        cmd.Parameters.AddWithValue("@telefonoAlternativo", (object?)cliente.TelefonoAlternativo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@correo", (object?)cliente.Correo ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@direccion", (object?)cliente.Direccion ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@colonia", (object?)cliente.Colonia ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ciudad", (object?)cliente.Ciudad ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@estado", (object?)cliente.Estado ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@codigoPostal", (object?)cliente.CodigoPostal ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@contactoEmergencia", (object?)cliente.ContactoEmergencia ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@parentescoEmergencia", (object?)cliente.ParentescoEmergencia ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@telefonoEmergencia", (object?)cliente.TelefonoEmergencia ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@observaciones", (object?)cliente.Observaciones ?? DBNull.Value);
    }

    private static Cliente Mapear(MySqlDataReader dr)
    {
        return new Cliente
        {
            IdCliente = Convert.ToInt32(dr["id_cliente"]),
            Nombre = Convert.ToString(dr["nombre"]) ?? "",
            ApellidoPaterno = Convert.ToString(dr["apellido_paterno"]) ?? "",
            ApellidoMaterno = dr["apellido_materno"] == DBNull.Value ? null : Convert.ToString(dr["apellido_materno"]),
            Telefono = Convert.ToString(dr["telefono"]) ?? "",
            TelefonoAlternativo = dr["telefono_alternativo"] == DBNull.Value ? null : Convert.ToString(dr["telefono_alternativo"]),
            Correo = dr["correo"] == DBNull.Value ? null : Convert.ToString(dr["correo"]),
            Direccion = dr["direccion"] == DBNull.Value ? null : Convert.ToString(dr["direccion"]),
            Colonia = dr["colonia"] == DBNull.Value ? null : Convert.ToString(dr["colonia"]),
            Ciudad = dr["ciudad"] == DBNull.Value ? null : Convert.ToString(dr["ciudad"]),
            Estado = dr["estado"] == DBNull.Value ? null : Convert.ToString(dr["estado"]),
            CodigoPostal = dr["codigo_postal"] == DBNull.Value ? null : Convert.ToString(dr["codigo_postal"]),
            ContactoEmergencia = dr["contacto_emergencia"] == DBNull.Value ? null : Convert.ToString(dr["contacto_emergencia"]),
            ParentescoEmergencia = dr["parentesco_emergencia"] == DBNull.Value ? null : Convert.ToString(dr["parentesco_emergencia"]),
            TelefonoEmergencia = dr["telefono_emergencia"] == DBNull.Value ? null : Convert.ToString(dr["telefono_emergencia"]),
            Observaciones = dr["observaciones"] == DBNull.Value ? null : Convert.ToString(dr["observaciones"]),
            FechaRegistro = Convert.ToDateTime(dr["fecha_registro"])
        };
    }
}