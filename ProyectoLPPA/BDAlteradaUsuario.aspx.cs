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
    public partial class BDAlteradaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session["usuario"] = "";
            Session["password"] = "";
            Session["rol"] = "";
            Session["listPermisos"] = "";
        }
    }
}