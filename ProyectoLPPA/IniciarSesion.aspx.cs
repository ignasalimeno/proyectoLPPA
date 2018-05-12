using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLPPA
{
    public partial class IniciarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Iniciar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Password.Text.Length < 8)
                {
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "La contraseña no puede tener menos de 8 dígitos.";
                    return;
                }
                else
                {
                    lbl_Error.Visible = false;
                    lbl_Error.Text = "El usuario o la contraseña son incorrectos.";
                }

                Globales.Usuario _user = new Globales.Usuario();
                _user = _user.obtenerUsuario(txt_Usuario.Text, txt_Password.Text);

                if (_user != null)
                {
                    List<Globales.Seguridad> listErrorDV = Globales.Seguridad.verificarDV();

                    Session["listDVErrores"] = listErrorDV;

                    if (listErrorDV != null)
                    {
                        if (listErrorDV.Count > 0)
                        {
                            if (_user.rol.ToString() == "1")
                            {
                                Session["usuario"] = _user.usuario;
                                Session["password"] = _user.password;
                                Session["rol"] = _user.rol;
                                Session["listPermisos"] = _user.listPermisos;
                                Response.Redirect("BDAlteradaWebMaster.aspx");
                            }
                            if (_user.rol.ToString() != "1")
                            {
                                Response.Redirect("BDAlteradaUsuario.aspx");
                            }
                        }
                    }

                    Session["usuario"] = _user.usuario;
                    Session["password"] = _user.password;
                    Session["rol"] = _user.rol;
                    Session["listPermisos"] = _user.listPermisos;

                    FormsAuthentication.SetAuthCookie(Session["usuario"].ToString(), createPersistentCookie: false);

                    Globales.Bitacora miLog = new Globales.Bitacora();
                    miLog.tipoLog = 1;
                    miLog.usuario = Session["usuario"].ToString();
                    Globales.Bitacora.registrarLog(miLog);

                    switch (Session["rol"].ToString())
                    {
                        case "1":
                            Response.Redirect("IndexWebMaster.aspx");
                            break;
                        case "2":
                            Response.Redirect("Precios.aspx");
                            break;
                        case "3":
                            Response.Redirect("Productos.aspx");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    lbl_Error.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}