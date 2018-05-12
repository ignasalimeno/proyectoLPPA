using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLPPA
{
    public partial class CerrarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                Globales.Bitacora miLog = new Globales.Bitacora();
                miLog.tipoLog = 2;

                miLog.usuario = Session["usuario"].ToString();
                Globales.Bitacora.registrarLog(miLog);

                Session["usuario"] = "";
                Session["password"] = "";
                Session["rol"] = "";
                Session["listPermisos"] = "";
            }
            Globales.Seguridad.grabarDV();
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx");
        }
    }
}