using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLPPA
{
    public partial class Bitacora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           bool tienePermisos = false;
           if (Session["usuario"] != null && Session["usuario"] != "")
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
                //MsgBox("Usted no tiene persmisos para acceder a dicha página!");
                Response.Redirect("Default.aspx");
            }

            cargarGrilla();
          
        }

        protected void cargarGrilla()
        {
            DataTable dtLog = new DataTable();

            dtLog = Globales.Bitacora.obtenerLog();

            grillaLog.DataSource = dtLog;
            grillaLog.DataBind();
        }

        protected void grillaLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grillaLog.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }
    }
}