using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoLPPA
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;


            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool tienePermisos = false;
            bool isDefaultPage = false;
            if (this.Page.AppRelativeVirtualPath.ToString() == "~/Default.aspx" || this.Page.AppRelativeVirtualPath.ToString() == "~/IniciarSesion.aspx")
            {
                isDefaultPage = true;
            }

            if (Session["usuario"] != "" && Session["usuario"] != null)
            {
                switch (Session["rol"].ToString())
                {
                    case "1":
                        link_Bitacora.Visible = true;
                        link_Backup.Visible = true;
                        break;
                    case "2":
                        link_Precios.Visible = true;
                        link_Clientes.Visible = true;
                        break;
                    case "3":
                        link_Productos.Visible = true;
                        break;
                    default:
                        break;
                }
            }

            if (!isDefaultPage)
            {
                if (Session["usuario"] != "" && Session["usuario"] != null)
                {
                    switch (Session["rol"].ToString())
                    {
                        case "1":
                            link_Bitacora.Visible = true;
                            link_Backup.Visible = true;
                            break;
                        case "2":
                            link_Precios.Visible = true;
                            link_Clientes.Visible = true;
                            break;
                        case "3":
                            link_Productos.Visible = true;
                            break;
                        default:
                            break;
                    }

                    foreach (Globales.Permisos item in (List<Globales.Permisos>)Session["listPermisos"])
                    {
                        if (item.nombrePantalla == Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath).ToString())
                        {
                            tienePermisos = true;
                        }
                    }

                    if (tienePermisos != true)
                    {
                        //MsgBox("Usted no tiene persmisos para acceder a dicha página!");
                        Response.Redirect("Default.aspx");
                    }
                }

                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}