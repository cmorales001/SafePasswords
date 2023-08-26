using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPassword.WebPages
{
    public partial class WFEditar : System.Web.UI.Page
    {
        private WSPassword.WebServicePasswordSoapClient clientePassword = new WSPassword.WebServicePasswordSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                // Para obtener el valor de la variable de sesión:
                WSPassword.ClienteDTO clienteSession = (WSPassword.ClienteDTO)HttpContext.Current.Session["SessionPassword"];
                this.txtRegFirstName.Text = clienteSession.Nombres;
                this.txtRegLastName.Text = clienteSession.Apellidos;
                this.txtRegUsernam.Text = clienteSession.Email;
            }
            catch(System.NullReferenceException)
            { 
            
            }
            
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            // redirige a la vista de la Sesión
            Response.Redirect("WFSession");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // se obtienen los datos de la vista
            string nombres = this.txtRegFirstName.Text;
            string apellidos = this.txtRegLastName.Text;
            string email = this.txtRegUsernam.Text;
            string password = this.txtRegPassword1.Text;
            string comPassword = this.txtRegConfPassword.Text;
            string passwordActual = this.TextPasswordActual1.Text;


            // si la contraseña ingresada es igual a la comprobación o ambas están vacias se ejecuta lo siguiente
            if (password == comPassword )
            {
                // se crea el nuevo objeto a registrar
                WSPassword.ClienteDTO clienteEdit = (WSPassword.ClienteDTO)HttpContext.Current.Session["SessionPassword"];
                clienteEdit.Apellidos = apellidos;
                clienteEdit.Email = email;
                clienteEdit.Password = password;
                clienteEdit.Nombres = nombres;
                

                bool respuesta = this.clientePassword.EditarUsuario(clienteEdit, passwordActual);

                if (respuesta)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡RegistroActualizado!');", true);
    
                    // registramos los nuevos datos en la sesión pero antes borramos el registro de contraseña
                    clienteEdit.Password = "";
                    HttpContext.Current.Session["SessionPassword"] = clienteEdit;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡La contraseña actual es incorrecta!');", true);
                }
            }
            else
            {
                // si las contraseñas no coinciden no se procede al registro
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡Las contraseñas nuevas no coinciden!');", true);
            }
        }
    }
}