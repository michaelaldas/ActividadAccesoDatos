using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Clases
{
    public static class Persona
    {
        private static string CadenaConex= @"desktop-dc0p3q7\sqlexpress; BaseDatos; Integrated Security=true";

        public static int Insertar(string Cedula, string Apellido, string Nombre, DateTime FechaNacimiento, double Peso)
        {
            SqlConnection conexion = new SqlConnection(CadenaConex);
            string sql = "insert into personas(Cedula, Apellido, Nombre, FechaNacimiento, Peso)";
            sql += "values(@Cedula, @Apellido, @Nombre, @FechaNacimiento, @Peso)";
            //3. ejecutar la operacion
            SqlCommand comando = new SqlCommand(sql, conexion);
            //3.1 configurar los parametros @Cedula, @Apellidos, @Nombres, @FechaNacimiento, @Peso
            comando.Parameters.Add(new SqlParameter("@cedula", Cedula));
            comando.Parameters.Add(new SqlParameter("@apellido", Apellido));
            comando.Parameters.Add(new SqlParameter("@nombre", Nombre));
            comando.Parameters.Add(new SqlParameter("@fechadenacimiento", FechaNacimiento));
            comando.Parameters.Add(new SqlParameter("@peso", Peso));
            //3.2 abrir la conexion 
            conexion.Open();
            //3.3 Insertar el registro en la Base de datos
            int res = comando.ExecuteNonQuery();
            //4 Cerrar la conexion
            conexion.Close();
            return res;
        }

        public static int Borrar(string Cedula)
        {
            SqlConnection conexion = new SqlConnection(CadenaConex);
            string eliminar = "DELETE FROM personas WHERE Cedula = @Cedula";
            SqlCommand comando = new SqlCommand(eliminar, conexion);
            //3.1 configurar el parametro @cedula
            comando.Parameters.Add(new SqlParameter("@Cedula", Cedula));
            //3.2 abrir la conexion 
            conexion.Open();
            //3.3 Insertar el registro en la Base de datos
            int res = comando.ExecuteNonQuery();
            //4 Cerrar la conexion
            conexion.Close();
            return res;
        }

        public static DataTable getpersona()
        {
            SqlConnection conexion = new SqlConnection(CadenaConex);
            string sql = "";
            sql = "select cedula as Cédulas, upper(Apellido+ ' ' + Nombre) as [Nombres Completos], FechaNacimiento as [Fechas de nacimiento], Peso as Peso ";
            sql += "from personas order by Apellido, Nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataAdapter ad1 = new SqlDataAdapter(comando);

            //pasar los datos del adaptador a un datatable
            DataTable dt = new DataTable();
            ad1.Fill(dt);
            return dt;
        }

    }

}

