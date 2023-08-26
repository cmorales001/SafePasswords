using System;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using WebServiceContraseñas.CRUDService;

namespace WebServiceContraseñas.DAO.CRUDService
{
    public class ClienteDAO : ConexionBD
    {

        public void Insertar(ClienteDTO cliente)
        {
            // try-catch para evitar irrupciones en el sistema
            // si la base de datos no esta disponible o se cae, el sistema no se detiene
            try
            {
                /* metodo de conexion a la base de Datos para crear un nuevo registro de Cliente
                sql es el nombre del procedimiento almacenado en la base de datos, que ejecuta
                un comando sql para crear un nuevo registro de cliente */
                string sql = "CrearCliente";

                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);

                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;
                // ingresamos los parametros que solicita el procedimiento almacenado para la crear de un nuevo registro
                command.Parameters.AddWithValue("@nombres", cliente.Nombres);
                command.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                command.Parameters.AddWithValue("@email", cliente.Email);
                command.Parameters.AddWithValue("@password", cliente.Password);
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
        private ClienteDTO Buscar(string email, int idCliente)
        {

            // metodo Buscarusuario que retorna un objeto cliente para su acceso al sistema o verificar su existencia
            // segun el método por el que sea llamado ya sea buscar por ID o por Email, 

            // se crea una instancia de Cliente DTO llamada clienteEncontrado,
            // para retornar el resultado
            ClienteDTO clienteEncontrado = null;
            try
            {
                //sql contiene el nombre del procedimiento almacenado en la base de datos, que ejecuta un comando sql para buscar un cliente por su email
                String sql = "BuscarCliente";
                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);
                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;

                // ingresamos los parametros que solicita el procedimiento almacenado para la consulta sql, almacenada en el procedimiento almacenado
                command.Parameters.AddWithValue("@email", email);

                // ingresamos los parametros que solicita el procedimiento almacenado para la consulta sql, almacenada en el procedimiento almacenado
                command.Parameters.AddWithValue("@idCliente", idCliente);


                // se abre la conexion
                command.Connection.Open();
                // creamos una instancia de SqlDataReader
                SqlDataReader leerFilas = command.ExecuteReader();

                /* con el metodo Read() recorremos los registros que obtuvo el objeto command, almacenados
                 en el datareader leerfilas, ya que si existen registros devuelve true , y si no false
                 se aclara que solo enviara un registro ya que la busqueda es por medio de email,
                 y esta propiedad tiene la restriccion de ser unica en la BDD*/
                if (leerFilas.Read())
                {
                    ClienteDTO cliente = new ClienteDTO(leerFilas.GetInt32(0), leerFilas.GetString(1), leerFilas.GetString(2), leerFilas.GetString(3), leerFilas.GetString(4));
                    clienteEncontrado = cliente;
                }
                // cerramos el objeto datareader
                leerFilas.Close();
                // cerramos la conexion a la base de datos
                command.Connection.Close();

                // y retornamos el cliente encontrado, si este existe devolvera un cliente, si no devolvera un objeto null
                return clienteEncontrado;
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
            return clienteEncontrado;
        }

        public ClienteDTO BuscarCliente(string email)
        {
            // metodo Buscarcliente que retorna un objeto cliente para su acceso al sistema o verificar su existencia
            // método sobrecargado con el parametro de string email
            // se crea una instancia de Cliente DTO llamada clienteEncontrado, para almacenar el resultado de la busqueda de un cliente, para el registro o el ingreso
            ClienteDTO clienteEncontrado = null;

            //llamamos al método Buscar que existe en la misma claseDao, en la que busca un usuario de forma general 
            // por su email y por su id, pero lo usamos segun la necesidad
            clienteEncontrado = this.Buscar(email, -1);

            return clienteEncontrado;

        }

        public ClienteDTO BuscarCliente(int idCliente)
        {
            // metodo  BuscarUsuariopor su id que retorna un objeto cliente para su acceso al sistema
            // o verificar su existencia

            // método sobrecargado con el parametro de  int idCliente

            // se crea una instancia de Cliente DTO llamada clienteEncontrado,
            // para almacenar el resultado de la busqueda de un cliente, para el registro o el ingreso
            ClienteDTO clienteEncontrado;

            //llamamos al método Buscar que existe en la misma claseDao, en la que busca un usuario de forma general 
            // por su email y por su id, pero lo usamos segun la necesidad
            clienteEncontrado = this.Buscar("", idCliente);

            return clienteEncontrado;

        }

        public void Editar(ClienteDTO cliente)
        {
            // metodo de conexion a la base de Datos para actualizar un registro de Cliente
            // try-catch para evitar irrupciones en el sistema
            // si la base de datos no esta disponible o se cae, el sistema no se detiene
            try
            {
                /*sql es la variable que contiene el nombre ddel procedimiento almacenado en la base de datos,
                que ejecuta un comando sql para actualizar un registro de cliente*/
                string sql = "ActualizarCliente";

                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);
                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;
                // ingresamos los parametros que solicita el procedimiento almacenado para la crear de un nuevo registro
                command.Parameters.AddWithValue("@idCliente", cliente.IdCliente);
                command.Parameters.AddWithValue("@nombres", cliente.Nombres);
                command.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                command.Parameters.AddWithValue("@email", cliente.Email);
                command.Parameters.AddWithValue("@password", cliente.Password);
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

        public void Eliminar(int idCliente)
        {
            //metodo de conexion a la base de Datos para eliminar un registro de Cliente
            try
            {
                /*sql es la variable que contiene el nombre ddel procedimiento almacenado en la base de datos,
                que ejecuta un comando sql para eliminar un registro de contraseña*/
                string sql = "BorrarCliente";

                /*se crea una instancia SqlCommand llamada command, que se crea con el 
                 constructor que recibe la sentencia sql a ejecutar , y la conexion a la base de datos 
                 heredada de la clase padre ConexionBD
                 */
                SqlCommand command = new SqlCommand(sql, base.conexionSql);
                // le asignamos el tipo procedimento almacenado a la instancia command
                command.CommandType = CommandType.StoredProcedure;
                // ingresamos los parametros que solicita el procedimiento almacenado para la crear de un nuevo registro
                command.Parameters.AddWithValue("@idCliente", idCliente);
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