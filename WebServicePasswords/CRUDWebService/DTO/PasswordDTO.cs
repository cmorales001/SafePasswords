using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceContraseñas.CRUDService.DAO
{
    public class PasswordDTO
    {
        private int _idPassword;
        // id al cliente que le pertenece
        private int _idCliente;
        // identificador de la contraseña
        private string _nombre;
        // contraseña asignada
        private string _password1;

        public PasswordDTO()
        {
        }
        public PasswordDTO(int idPassword, int idCliente, string nombre, string password1)
        {
            this._idPassword = idPassword;
            this._idCliente = idCliente;
            this._nombre = nombre;
            this._password1 = password1;
        }
        public PasswordDTO(int idCliente, string nombre, string password1 )
        {
            this._idCliente=idCliente;
            this._nombre = nombre;
            this._password1 = password1;
        }

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Password1 { get => _password1; set => _password1 = value; }
        public int IdPassword { get => _idPassword; set => _idPassword = value; }
        public int IdCliente { get => _idCliente; set => _idCliente = value; }
    }
}