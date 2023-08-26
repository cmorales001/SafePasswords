using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebServiceContraseñas.CRUDService.DAO;
using WebServiceContraseñas.DAO.CRUDService;

namespace WebServiceContraseñas.CRUDService.Service
{
    public class PasswordService
    {
        // objeto para la conexión a la Capa de Datos, disponible para todos los métodos
        private readonly PasswordDAO passwordDatos = new PasswordDAO();
        
        public bool PasswordRegistro(PasswordDTO password)
        {

            // verificamos que el nombre para esa contraseña no exista
            bool nombreNoExiste = true;
            List<PasswordDTO> passwords = this.PasswordBuscar(password.IdCliente);
            // se recorre las contraseñas para encontrar coincidencias
            foreach (PasswordDTO password1 in passwords) {
                if (password.Nombre.Equals(password1.Nombre)) 
                {
                    nombreNoExiste = false;
                }
            }

            if (nombreNoExiste)
            {
                // método para registrar una contraseña en el banco de contraseñas del usuario
                // irán encriptadas a la base de dato con su estructura de tabla
                // con ayuda del método CifradoCesar encriptamos la contraseña
                password.Password1 = this.CifradoCesar(password.Password1, 5);
                // utilizamos el objeto passwordDatos para conectarnos a la capaDao y ejecutar la operación CRUD
                this.passwordDatos.Insertar(password);
                return true;
            }
            else 
            {
                return false;
            }
            
        }



        public List<PasswordDTO> PasswordBuscar(int idCliente)
        // método para recuperar las contraseñas de un usuario
        {
            List<PasswordDTO> passwords;

            // declaramos una lista temporal para recoger el resultado
            passwords = this.passwordDatos.BuscarPasswords(idCliente);
            // desencriptamos las contraseñas con ayuda del método Desencriptar
            // recorriendo cada una de ellas
            foreach (PasswordDTO password in passwords)
            {
                password.Password1 = this.DescifradoCesar(password.Password1,5);
            }
            return passwords;

        }

        public void PasswordEditar(PasswordDTO passwordEdit)
        {
            // el metodo PasswordEditar , se encarga de la modificación de un registro de password pertenciente a un cliente,
            // Se encripta la nueva contraseña
            passwordEdit.Password1 = this.CifradoCesar(passwordEdit.Password1, 5);
            // y se actualiza el registro
            this.passwordDatos.Editar(passwordEdit);

        }

        public void PasswordEliminar(int idPassword)
        {
            this.passwordDatos.Eliminar(idPassword);
        }


        // función que encripta una cadena, con opción a desencriptación
        // (ES UNA SIMULACIÓN, con ayuda del algoritmo cifrado Cesar que desplaza cada letra de la cadena original un número fijo de posiciones en el alfabeto
        private  string CifradoCesar(string mensaje, int desplazamiento)
        {
            string mensajeCifrado = "";
            foreach (char letra in mensaje)
            {
                if (letra >= 'a' && letra <= 'z')
                {
                    char letraCifrada = (char)(((letra - 'a') + desplazamiento) % 26 + 'a');
                    mensajeCifrado += letraCifrada;
                }
                else if (letra >= 'A' && letra <= 'Z')
                {
                    char letraCifrada = (char)(((letra - 'A') + desplazamiento) % 26 + 'A');
                    mensajeCifrado += letraCifrada;
                }
                else
                {
                    mensajeCifrado += letra;
                }
            }
            return mensajeCifrado;
        }

        private  string DescifradoCesar(string mensajeCifrado, int desplazamiento)
        {
            return CifradoCesar(mensajeCifrado, 26 - desplazamiento);
        }


    }


}