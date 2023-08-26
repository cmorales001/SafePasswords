using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebServiceContraseñas.DAO.CRUDService;


namespace WebServiceContraseñas.CRUDService.DAO
{
    internal class PasswordDAO : ConexionBD
    {
        
        public void Insertar(PasswordDTO password)
        {
            // try-catch para evitar irrupciones en el sistema
            // si la base de datos no esta disponible o se cae, el sistema no se detiene
            try
            {
                /* metodo de conexion a la base de Datos para crear un nuevo registro de Password,
                 * 
                donde sql es el nombre del procedimiento almacenado en la base de datos, que ejecuta
                un comando sql para crear un nuevo registro de password */
                string sql = "CrearPassword";

                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);

                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;
                // ingresamos los parametros que solicita el procedimiento almacenado para la creacion de un nuevo registro
                command.Parameters.AddWithValue("@nombre", password.Nombre);
                command.Parameters.AddWithValue("@password", password.Password1);
                command.Parameters.AddWithValue("@idCliente", password.IdCliente);
                // se abre la conexion
                command.Connection.Open();
                //ejecutamos el comando a la base de datos , y este metodo devuelve un entero, referente al numero de filas afectadas, por el sql
                command.ExecuteNonQuery();
                // se cierra la conexión
                command.Connection.Close();
                // se retorna el resultado 
            }
            catch (SqlException ex)
            {
                // mensaje de error que se muestra al usuario final
                string MensajeError = "Problemas con los Datos, intente de nuevo más tarde.";

                // Envía un mensaje de registro de errores 
                string errorMessage = ex.Message;

                // Usa el contexto entregado anteriormente para enviar el mensaje de error a la aplicación web
                HttpContext.Current.Response.Write(MensajeError);
            }



        }

        public List<PasswordDTO> BuscarPasswords(int idCliente)
        {
            // metodo que retorna las contraseñas almacenadas por un cliente
            //se crea una lista que contendrá las contraseñas almacenadas por un usuario,   
            List<PasswordDTO> passwords = new List<PasswordDTO>();
            // try-catch para evitar irrupciones en el sistema
            // si la base de datos no esta disponible o se cae, el sistema no se detiene
            try
            {
                //sql contiene el nombre del procedimiento almacenado en la base de datos, que ejecuta un comando sql para buscar las contraseñas de un cliente
                String sql = "BuscarPasswordCliente";
                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);
                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;
                // ingresamos los parametros que solicita el procedimiento almacenado para la consulta sql, almacenada en el procedimiento almacenado
                command.Parameters.AddWithValue("@idCliente", idCliente);
                // se abre la conexion
                command.Connection.Open();
                // creamos una instancia de SqlDataReader
                SqlDataReader leerFilas = command.ExecuteReader();

                /* con el metodo Read() recorremos los registros que obtuvo el objeto command, almacenados
                 en el datareader leerfilas, ya que si existen registros devuelve true , y si no false
                 */
                while (leerFilas.Read())
                {
                    PasswordDTO password = new PasswordDTO(leerFilas.GetInt32(0), leerFilas.GetInt32(1),  leerFilas.GetString(2), leerFilas.GetString(3));
                    passwords.Add(password);
                }
                // cerramos el objeto datareader
                leerFilas.Close();
                // cerramos la conexion a la base de datos
                command.Connection.Close();

                // y retornamos la lista de contraseñas encontradas
                return passwords;

            }
            catch (SqlException ex)
            {
                // mensaje de error que se muestra al usuario final
                string MensajeError = "Problemas con los Datos, intente de nuevo más tarde.";

                // Envía un mensaje de registro de errores 
                string errorMessage = ex.Message;

                // Usa el contexto entregado anteriormente para enviar el mensaje de error a la aplicación web
                HttpContext.Current.Response.Write(MensajeError);
            }


            // si se presenta algun error la lista de contraseñas se devolverá
            return passwords;
        }

        public void Editar(PasswordDTO password)
        {
            // metodo de conexion a la base de Datos para actualizar un registro de Contraseña
            // try-catch para evitar irrupciones en el sistema
            // si la base de datos no esta disponible o se cae, el sistema no se detiene
            try
            {
                /*sql es la variable que contiene el nombre ddel procedimiento almacenado en la base de datos,
                que ejecuta un comando sql para actualizar un registro de cliente*/
                string sql = "ActualizarPassword";

                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);
                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;
                // ingresamos los parametros que solicita el procedimiento almacenado para la crear de un nuevo registro
                command.Parameters.AddWithValue("@idPassword", password.IdPassword);
                command.Parameters.AddWithValue("@identificador", password.Nombre);
                command.Parameters.AddWithValue("@password", password.Password1);
                // se abre la conexion
                command.Connection.Open();
                //ejecutamos el comando a la base de datos , y este metodo devuelve un entero, referente al numero de filas afectadas, por el sql
                command.ExecuteNonQuery();
                // se cierra la conexión
                command.Connection.Close();
            }
            catch (SqlException ex)
            {
                // mensaje de error que se muestra al usuario final
                string MensajeError = "Problemas con los Datos, intente de nuevo más tarde.";

                // Envía un mensaje de registro de errores 
                string errorMessage = ex.Message;

                // Usa el contexto entregado anteriormente para enviar el mensaje de error a la aplicación web
                HttpContext.Current.Response.Write(MensajeError);
            }

        }

        public void Eliminar(int idPassword)
        {
            // metodo de conexion a la base de Datos para eliminar un registro de Contraseña de un Cliente
            // try-catch para evitar irrupciones en el sistema
            // si la base de datos no esta disponible o se cae, el sistema no se detiene
            try
            {
                /*sql es la variable que contiene el nombre ddel procedimiento almacenado en la base de datos,
                que ejecuta un comando sql para eliminar un registro de contraseña*/
                string sql = "EliminarPassword";

                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);
                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;
                // ingresamos los parametros que solicita el procedimiento almacenado para la crear de un nuevo registro
                command.Parameters.AddWithValue("@idPassword", idPassword);
                // se abre la conexion
                command.Connection.Open();
                //ejecutamos el comando a la base de datos , y este metodo devuelve un entero, referente al numero de filas afectadas, por el sql
                command.ExecuteNonQuery();
                // se cierra la conexión
                command.Connection.Close();
            }
            catch (SqlException ex)
            {
                // mensaje de error que se muestra al usuario final
                string MensajeError = "Problemas con los Datos, intente de nuevo más tarde.";

                // Envía un mensaje de registro de errores 
                string errorMessage = ex.Message;

                // Usa el contexto entregado anteriormente para enviar el mensaje de error a la aplicación web
                HttpContext.Current.Response.Write(MensajeError);
            }


        }
    }
}