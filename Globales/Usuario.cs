using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globales
{
    public class Usuario
    {
        #region Variables
        public string usuario;
        public string password;
        public int rol;
        public List<Permisos> listPermisos;
        #endregion

        #region Procedimientos
        public Usuario obtenerUsuario(string usuario, string password)
        {
            try
            {
                string str = "SELECT * FROM Usuarios WHERE Usuario = '" + usuario + "'";
                DataTable dt = Datos.connectToDB.fillTableSQL(str);

                if (dt.Rows.Count > 0)
                {
                    Usuario _usuarioNuevo = new Usuario();
                    _usuarioNuevo.usuario = usuario;
                    _usuarioNuevo.password = dt.Rows[0]["Password"].ToString();
                    _usuarioNuevo.rol = int.Parse(dt.Rows[0]["idRol"].ToString());

                    string passEncriptada = Globales.Encriptacion.encriptar(password);

                    if (passEncriptada == _usuarioNuevo.password)
                    {
                        //carga de permisos
                        Permisos newPermisos = new Permisos();
                        _usuarioNuevo.listPermisos = newPermisos.obtenerPermisos(_usuarioNuevo.rol);
                        return _usuarioNuevo;   
                    }
                    else
                    {
                        throw new Exception("El password no es correcto!");
                    }
                }
                else
                {
                    throw new Exception("El usuario no existe!");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

       
        #endregion


    }
}
