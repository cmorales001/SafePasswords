using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebPassword.WSPassword;

namespace WebPassword.WebPages
{
    public partial class WFSession : System.Web.UI.Page
    {
        // objeto de conexion al servicio Web
        private WSPassword.WebServicePasswordSoapClient passwordCliente = new WebServicePasswordSoapClient();
        // Para obtener el valor de la variable de sesión:
        WSPassword.ClienteDTO clienteSession;

        protected void Page_Load(object sender, EventArgs e)
        {
            clienteSession = (WSPassword.ClienteDTO)HttpContext.Current.Session["SessionPassword"];

            if (clienteSession != null)
            {
                this.labelNombreUser.Text = "Bienvenid@ " + clienteSession.Nombres;
            }
            


            if (!IsPostBack)
            {
                llenarGestorDatos();
            }
        }
        

        private void llenarGestorDatos()
        {
            try
            {
                // Crear un arreglo con datos de prueba
                PasswordDTO[] datos = passwordCliente.AccesoPasswords(clienteSession.IdCliente);

                // Enlazar el control GridView al arreglo
                GridView1.DataSource = datos;
                GridView1.DataBind();
            }
            catch (IndexOutOfRangeException ex)
            {
                // Manejar la excepción aquí
                Console.WriteLine("No existen Registros: " + ex.Message);
            }
            catch (System.NullReferenceException)
            { 

            }
            
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Obtener los datos de la fila editada
            var id = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            var nombre = e.NewValues["Nombre"].ToString();
            var apellido = e.NewValues["Apellido"].ToString();

            // Actualizar el arreglo con los datos editados
            // (en lugar de modificar directamente la base de datos)
            var datos = (IEnumerable<dynamic>)GridView1.DataSource;
            var filaEditada = datos.FirstOrDefault(x => x.Id == id);
            if (filaEditada != null)
            {
                filaEditada.Nombre = nombre;
                filaEditada.Apellido = apellido;
            }

            // Finalizar la edición y volver a enlazar el control GridView al arreglo actualizado
            GridView1.EditIndex = -1;
            GridView1.DataSource = datos;
            GridView1.DataBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Obtener el ID de la fila a eliminar
            var id = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());

            // Eliminar la fila del arreglo
            // (en lugar de modificar directamente la base de datos)
            var datos = (IEnumerable<dynamic>)GridView1.DataSource;
            var filaEliminada = datos.FirstOrDefault(x => x.Id == id);
            if (filaEliminada != null)
            {
                var nuevosDatos = datos.Except(new[] { filaEliminada });
                GridView1.DataSource = nuevosDatos;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleteRow")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[rowIndex];
                PasswordDTO passwordDelete = (PasswordDTO)row.DataItem;
                //if (passwordDelete == null) {
                //    Response.Redirect("WFEditar.aspx");
                //}
                //this.passwordCliente.EliminarPassword(passwordDelete.IdPassword);
                // Llamar a tu método DAO para eliminar el registro usando el valor de idPassword

                // Actualizar el control GridView si es necesario
            }
        }

        protected void btnGeneratePass_Click(object sender, EventArgs e)
        {
            bool caracteresEsp = chkSpecialChars1.Checked;
            bool numeros = chkNumbers.Checked;
            int longitud = int.Parse(txtLength.Value.ToString());
            String password = this.passwordCliente.GenerarPassword(longitud, numeros, caracteresEsp);
            this.txtGeneratedPassword1.Text = password;
        }

        protected void btnCopy1_Click(object sender, EventArgs e)
        {
            String password = this.passwordCliente.GenerarPasswordFacilRecordar();
            this.txtGeneratedPassword1.Text = password;
        }
        protected void cerrarSesion(object sender, EventArgs e)
        {
            Session.Clear(); // Limpia todas las variables de sesión
            Response.Redirect("WFLogin.aspx"); // Redirige a la página de inicio
        }

        protected void btnRegPassword_Click(object sender, EventArgs e)
        {
            // recogemos los datos de registro
            string nombre =this.txtNamePassword.Text;
            string password = this.txtRegPassword.Text;
            int idCliente = this.clienteSession.IdCliente;
            // creamos un nuevo registro para enviarlo al servicio web
            WSPassword.PasswordDTO passwordRegistro = new WSPassword.PasswordDTO();
            // le asignamos los atributos necesario
            passwordRegistro.IdCliente = idCliente;
            passwordRegistro.Nombre = nombre;
            passwordRegistro.Password1 = password;
            // consumimos el servicio web 
            bool respuesta = this.passwordCliente.RegistroPassword(passwordRegistro);
            if (respuesta)
            {
                //si el registro se ejecutó refresca los datos
                this.llenarGestorDatos();
                this.txtNamePassword.Text="";
                this.txtRegPassword.Text = "";
            }
            else {
                // si ya existe un registro con ese nombre genera  el mensaje
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Contraseña con ese nombre ya generada.');", true);
                this.txtNamePassword.Text = "";
                this.txtRegPassword.Text = "";
            }
            
            
            
        }

        protected void btnRegenerate1_Click(object sender, EventArgs e)
        {
            string password = this.txtGeneratedPassword1.Text;
            this.txtRegPassword.Text = password;
        }
    }
}