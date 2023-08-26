using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceContraseñas.CRUDService
{
    public class ClienteDTO
    {
        private int _idCliente;
        private String _nombres;
        private String _apellidos;
        private String _email;
        private String _password;

        public ClienteDTO()
        {
        }
        public ClienteDTO(int idCliente, string nombres, string apellidos, string email, string password)
        {
            this._idCliente = idCliente;
            this._nombres = nombres;
            this._apellidos = apellidos;
            this._email = email;
            this._password = password;
        }

        public ClienteDTO(string nombres, string apellidos, string email, string password)
        {
            this._nombres = nombres;
            this._apellidos = apellidos;
            this._email = email;
            this._password = password;
        }



        public int IdCliente { get => _idCliente; set => _idCliente = value; }
        public string Nombres { get => _nombres; set => _nombres = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }


        public override String ToString()
        {
            return $"Cliente:  ID= {this._idCliente} , Nombre : {this._idCliente} , Apellidos {this._apellidos} , Correo : {this._email}";
        }

    }
}