using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebServiceContraseñas.CRUDService.DAO;
using WebServiceContraseñas.CRUDService.Negocio;
using WebServiceContraseñas.CRUDService.Service;
using WebServiceContraseñas.CRUDService;
using WebServiceContraseñas;

namespace WebServicePasswords
{
    /// <summary>
    /// Descripción breve de WebServicePassword
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePassword : System.Web.Services.WebService
    {

        // variables para interactuar con los servicios del servicio web
        private readonly PasswordGenerate generadorPassword;
        private readonly ClienteService serviceCliente;
        private readonly PasswordService servicePassword;

        public WebServicePassword()
        {
            // inicializo las variables 
            this.generadorPassword = new PasswordGenerate();
            this.serviceCliente = new ClienteService();
            this.servicePassword = new PasswordService();
        }

        //*************MÉTODOS PARA GENERACIÓN DE CONTRASEÑAS SEGURAS****************

        [WebMethod]
        public string GenerarPasswordFacilRecordar()
        {
            String passWordResultante = this.generadorPassword.PasswordFacilRecordar();
            return passWordResultante;    
        }

        [WebMethod]
        public string GenerarPassword(int longitud, Boolean numeros, Boolean simbolos)
        {
            
            String passWordResultante = this.generadorPassword.GenerarPassword(longitud, simbolos, numeros);
            return passWordResultante;
           
        }



        // ******************MÉTODOS PARA CRUD DE USUARIOS(CLIENTES)**********
        [WebMethod]
        public Boolean RegistroUsuario(ClienteDTO clienteRegistro)
        {
             //variable que almacena la respuesta de la capa Service
              bool respuesta;
              // método ClienteRegistro de la capa ClienteService para procesar la información de entrada
                respuesta = this.serviceCliente.ClienteRegistro(clienteRegistro);

              return respuesta;
        }

        [WebMethod]
        public ClienteDTO AccesoUsuario(string email, string password)// login
        {
            //variable que almacena la respuesta de la capa Service
            ClienteDTO clienteEncontrado;
            // método ClienteLogin de la capa ClienteService para procesar la información de entrada y acceder al sistema
            clienteEncontrado = this.serviceCliente.ClienteLogin(email, password);
            return clienteEncontrado;
        }

        [WebMethod]
        public Boolean EditarUsuario(ClienteDTO cliente, string passwordActual)
        {
            //variable que almacena la respuesta de la capa Service
            bool respuesta;
            // método ClienteEditar de la capa ClienteService para procesar la información de entrada y editar el resgistro
            respuesta = this.serviceCliente.ClienteEditar(cliente, passwordActual);

            return respuesta;
        }

        [WebMethod]
        public Boolean EliminarUsuario(int idCliente, string passwordActual)
        {
            //variable que almacena la respuesta de la capa Service
            bool respuesta;
            // método ClienteResgitro de la capa ClienteService para procesar la información de entrada
            respuesta = this.serviceCliente.ClienteEliminar(idCliente, passwordActual);
            return respuesta;
        }

        //*************MÉTODOS PARA CRUD DE CONTRASEÑAS***************

        [WebMethod]
        public bool RegistroPassword(PasswordDTO passwordRegistro)
        {
            bool respuesta = false;
            // método PasswordRegistro de la capa PasswordService para procesar la información de entrada
            respuesta = this.servicePassword.PasswordRegistro(passwordRegistro);
            return respuesta;
        }

        [WebMethod]
        public List<PasswordDTO> AccesoPasswords(int idCliente)// login
        {
            //variable que almacena la respuesta de la capa Service
            List<PasswordDTO> passwords;
            // método PasswordBuscar de la capa PasswordService para obtener las contraseñas de un cliente
            passwords = this.servicePassword.PasswordBuscar(idCliente);
            return passwords;
        }

        [WebMethod]
        public void EditarPasword(PasswordDTO passwordEdit)
        {
            // método PasswordEditar de la capa PasswordService para actualizar el registro de una contraseña
            this.servicePassword.PasswordEditar(passwordEdit);

        }

        [WebMethod]
        public void EliminarPassword(int idPassword)
        {
            // método ClienteResgitro de la capa ClienteService para procesar la información de entrada
            this.servicePassword.PasswordEliminar(idPassword);

        }
    }
}
