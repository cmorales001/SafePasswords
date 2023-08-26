using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using WebServiceContraseñas.DAO.CRUDService;

namespace WebServiceContraseñas.CRUDService.Negocio
{
    public class ClienteService
    {
        // se crea un instancia de ClienteDao para la conexion a la capa de datos DAO
        private readonly ClienteDAO clienteDatos = new ClienteDAO();

        public Boolean ClienteRegistro(ClienteDTO cliente)
        {
            /* el metodo ClienteRegistro , se encarga de la creacion de un nuevo registro de cliente,
             revisando que todos los parametros sean correctos, incluso si ya existe un usuario
            esto con el objetivo de controlar el ingreso de parametros independientemente de la vista*/

            // se crea un variable que devolera un valor booleano a la interfaz grafica segun cada caso
            Boolean respuesta = false;


            
                if (clienteDatos.BuscarCliente(cliente.Email) != null)
                /* el if comprueba que el cliente no se encuentre ya registrado, 
                 * ya que el metodo Acceso usuario devuelve la consulta
                 *y si este devuelve un cliente, significa que no puede registrar con ese email, ya que es unico*/
                {
                    return respuesta; 
                }
                else
                {
                    // si el usuario no existe entonces, encripta la contraseña del usuario y ejecuta el registro en la BDD con el metodo insertar de cliente Dao
                    cliente.Password = GenerarSHA1(cliente.Password);
                    clienteDatos.Insertar(cliente);
                    respuesta = true;
                }
            
            
            return respuesta;
        }



        public ClienteDTO ClienteLogin(string email, string password)
        {
            // creo una variable ClienteDTO llamada Cliente Encontrado para almacenar el Cliente a ingresar
            ClienteDTO ClienteEncontrado = null;
            // buscamos el cliente por medio del  metodo BuscarUsuario del objeto ClienteDao, que retorna un cliente si existe o retorna null si no
            ClienteEncontrado = clienteDatos.BuscarCliente(email);


            if (ClienteEncontrado != null && ClienteEncontrado.Password == GenerarSHA1(password))
            /* Se verifica que el cliente exista y tambien se encripta la contraseña ingresada en la interfaz
             * para verificar si coincide con el registro de la BDD, 
             si esta coincide devuelve el objeto cliente con la información del cliente*/
            {
                //para ser enviado , a traves del servicio web se elimina su contraseña ya que esta permanece en la bdd 
                // ademas no exponerla, solo enviando los datos como nombres, id e email
                ClienteEncontrado.Password = "";
                return ClienteEncontrado;
            }
            else
            /* si las contraseñas coinciden entonces envian los parametros del cliente a la clase Cliente Sesion,
             que albergara la sesion una vez comprobadas las credenciales */
            {
                return null ;
            }
        }

        public Boolean ClienteEditar(ClienteDTO cliente, string passwordActual)
        {
            // el metodo ClienteEditar , se encarga de la modificación de un registro de un cliente,

            // se crea una variable que devolvera un valor de verdadero o falso a la interfaz grafica segun cada caso
            bool respuesta = false;



            // obtengo el objeto del usuario a actualizar desde la capa Dao
            ClienteDTO ClienteEdit = clienteDatos.BuscarCliente(cliente.IdCliente);
            //guardo la contraseña extraida de la base de datos
            string passwordAct = ClienteEdit.Password;
            //se encripta la contraseña ingresada en el form, y se compara con el registro de la bdd
            if (GenerarSHA1(passwordActual) != passwordAct)
            {
                // si la contraseña no coincide envia el valor falso
                return respuesta;
            }
            else
            {

                // si se ingresaron nuevos datos, se los almacena en el objeto a actualizar, solo en el caso de no estan vacios
                // se encripta la nueva  contraseña del usuario y ejecuta el registro en la BDD con el metodo editar de cliente Dao
                if (!string.IsNullOrEmpty(cliente.Nombres))
                {
                    ClienteEdit.Nombres = cliente.Nombres;
                }
                if (!string.IsNullOrEmpty(cliente.Apellidos))
                {
                    ClienteEdit.Apellidos = cliente.Apellidos;
                }
                if (!string.IsNullOrEmpty(cliente.Email))
                {
                    ClienteEdit.Email = cliente.Email;
                }
                if (!string.IsNullOrEmpty(cliente.Password))
                {
                    ClienteEdit.Password = GenerarSHA1(cliente.Password);
                }

                clienteDatos.Editar(ClienteEdit);
                respuesta = true;

            }



            // finalmente retorna el resultado segun corresponda
            return respuesta;

        }

        public Boolean ClienteEliminar(int idCliente, string password)
        {
            // el metodo ClienteEliminar, se encarga de la eliminación de un registro de cliente por medio de su ID,
            // y la verificación de su contraseña , para que no ocurra esta operacion por accidente

            // se crea una variable que devolvera un mensaje a la interfaz grafica segun cada caso
            bool respuesta = false;



            // obtengo el objeto del usuario a actualizar desde la capa Dao
            ClienteDTO ClienteEdit = clienteDatos.BuscarCliente(idCliente);
            //guardo la contraseña extraida de la base de datos
            string passwordBD = ClienteEdit.Password;
            //se encripta la contraseña ingresada en el form, y se compara con el registro de la bdd
            if (GenerarSHA1(password) != passwordBD)
            {
                // si la contraseña no coincide retorna un false
                return respuesta;
            }
            else
            {

                //si la contraseña coincide entonces procede a eliminar el registro 

                clienteDatos.Eliminar(idCliente);
                respuesta = true;
            }



            // finalmente retorna el resultado segun corresponda
            return respuesta;

        }


        private string GenerarSHA1(string cadena)
        // metodo que encripta la contraseña usando del algoritmo sha1
        {
            //Codifica la cadena en bytes
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(cadena);
            byte[] result;

            // Crea un objeto para calcular el hash
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            //calcula el hash
            result = sha.ComputeHash(data);

            //construye una cadena con los bytes del hash
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                // si el byte es menor a 16 se agrega un 0 antes de el valor hexadecimal
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }

            return sb.ToString();
        }


    }

}