using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLPPA
{
    public partial class Backup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Generar_Click(object sender, EventArgs e)
        {
            string nombre = Globales.Backup.generarBackup();

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombre);
            Response.TransmitFile("C:\\TEMP\\" + nombre);

            Response.End();
        }

        protected void btn_Restaurar_Click(object sender, EventArgs e)
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