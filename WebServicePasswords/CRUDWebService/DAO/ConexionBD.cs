using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebServiceContraseñas.DAO.CRUDService
{
    public class ConexionBD
    {
        // conexion cadena obtiene el conectionString para conectarse a la base de datos
        static readonly string conexionCadena = System.Configuration.ConfigurationManager.ConnectionStrings["WebServiceDB"].ConnectionString;
        //conexionSql es el objeto que genera la conexion con la bdd
        protected SqlConnection conexionSql = new SqlConnection(conexionCadena);

    }
}