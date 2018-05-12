using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globales
{
    public class Permisos
    {
        public string nombrePantalla;

        internal List<Permisos> obtenerPermisos(int rol)
        {
            try
            {
                List<Permisos> _listPermisos = new List<Permisos>();
                string str = "";
                str = str + "Select * ";
                str = str + "From Permisos ";
                str = str + "Where idPermiso in ( ";
                str = str + "Select idPermiso from Roles_Permisos Where idRol = " + rol + ")";

                DataTable dt = Datos.connectToDB.fillTableSQL(str);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Permisos newPermiso = new Permisos();

                        newPermiso.nombrePantalla = dt.Rows[i]["Pantalla"].ToString();

                        _listPermisos.Add(newPermiso);
                    }
                }

                return _listPermisos;
            }
            catch (Exception)
            {
                return null;
            }
        }


     
    }
}
