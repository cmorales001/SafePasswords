using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPassword.WebPages
{
    public partial class WFRegister : System.Web.UI.Page
    {
        private WSPassword.WebServicePasswordSoapClient clientePassword = new WSPassword.WebServicePasswordSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFLogin.aspx");
        }

        // método para dejar en blanco los textbox del formulario
        private void reset() {
            txtRegFirstName.Text = "";
            txtRegLastName.Text = "";
            txtRegUsernam.Text = "";
            txtRegPassword.Text = "";
            txtRegConfPassword.Text = "";
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // se obtienen los datos de la vista
                string nombres = this.txtRegFirstName.Text;
                string apellidos = this.txtRegLastName.Text;
                string email = this.txtRegUsernam.Text;
                string password = this.txtRegPassword.Text;
                string comPassword = this.txtRegConfPassword.Text;

                // si la contraseña ingresada es igual a la comprobación se ejecuta lo siguiente
                if (password == comPassword)
                {
                    // se crea el nuevo objeto a registrar
                    WSPassword.ClienteDTO clienteRegistro = new WSPassword.ClienteDTO();
                    clienteRegistro.Apellidos = apellidos;
                    clienteRegistro.Email = email;
                    clienteRegistro.Password = password;
                    clienteRegistro.Nombres = nombres;

                    bool respuesta = this.clientePassword.RegistroUsuario(clienteRegistro);

                    if (respuesta)
                    {
                        this.reset();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡Nuevo Usuario Registrado!');", true);

                    }
                    else
                    {
                        this.reset();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡El usuario ya Existe!');", true);
                    }
                }
                else
                {
                    // si las contraseñas no coinciden no se procede al registro
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('¡Las contraseñas no coinciden.!');", true);
                }
            }

            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('ServicioWeb no disponible, por el momento.');", true);
            }
            catch (System.Xml.XmlException)
            { }
            

            

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }
    }
}