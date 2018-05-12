using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLPPA
{
    public partial class BDAlteradaWebMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtErrores = new DataTable();
            dtErrores.Columns.Add(new DataColumn("Tabla"));
            dtErrores.Columns.Add(new DataColumn("IdRegistro"));

            foreach (Globales.Seguridad item in (List<Globales.Seguridad>)Session["listDVErrores"])
            {
                DataRow dr = dtErrores.NewRow();

                dr["Tabla"] = item.tabla;
                dr["IdRegistro"] = item.id;

                dtErrores.Rows.Add(dr);

            }

            grillaErrores.DataSource = dtErrores;
            grillaErrores.DataBind();
        }

        protected void btn_RecalcularDV_Click(object sender, EventArgs e)
        {
            Globales.Seguridad.grabarDV();

            Session.RemoveAll();
            FormsAuthentication.SignOut();

            Response.Redirect("IniciarSesion.aspx");
        }

        protected void grillaErrores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btn_RestaurarBD_Click(object sender, EventArgs e)
        {
            string path;
            string path1;

            path = Server.MapPath("~/");
            path1 = path + FileUpload1.FileName;
            Globales.Backup.borrarBackup(path1);

            FileUpload1.SaveAs("C:\\TEMP\\" + FileUpload1.FileName);


            Globales.Backup.restaurarBackup(path1, "MR_LPPA");
            Response.Redirect("CerrarSesion.aspx");
        }


    }
}