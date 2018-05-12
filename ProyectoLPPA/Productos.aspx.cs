using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLPPA
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          bool tienePermisos = false;
          if (Session["usuario"] != null)
          {
              foreach (Globales.Permisos item in (List<Globales.Permisos>)Session["listPermisos"])
              {
                  if (item.nombrePantalla == Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath).ToString())
                  {
                      tienePermisos = true;
                  }
              }
          }
          else
          {
              Response.Redirect("IniciarSesion.aspx");
          }
          
          if (tienePermisos != true)
          {
              ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Usted no tiene permisos para acceder a esta opción.');window.location ='Default.aspx';",
                true);
          }
        }

        private void MsgBox(string sMessage)
        {
            string msg = "<script language=\"javascript\">";
            msg += "alert('" + sMessage + "');";
            msg += "</script>";
            Response.Write(msg);
        }
    }
}