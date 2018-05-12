using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globales
{
    public class Bitacora
    {
        public int tipoLog;
        public string usuario;
        public DateTime fecha;
        public string flag1;
        public string flag2;
        public string flag3;

        public static bool registrarLog(Bitacora miRegistro)
        {
            try
            {
                string str = "";
                str = str + "INSERT INTO [MR_LPPA].[dbo].[Log] ";
                str = str + "([idTipo],[Usuario],[Fecha],[Flag1],[Flag2],[Flag3]) ";
                str = str + "VALUES (";
                str = str + miRegistro.tipoLog;
                str = str + ", '" + miRegistro.usuario + "' ";
                str = str + ", GETDATE() ";
                str = str + ", '" + miRegistro.flag1 + "' ";
                str = str + ", '" + miRegistro.flag2 + "' ";
                str = str + ", '" + miRegistro.flag3 + "') ";

                Datos.connectToDB.launchCommand(str);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable obtenerLog()
        {
            try
            {
                DataTable dt = new DataTable();

                string str = "";
                str = str + "SELECT [idLog] ";
                str = str + ",LT.Descr AS Tipo ";
                str = str + ",[Usuario] ";
                str = str + ",[Fecha] ";
                str = str + "FROM [MR_LPPA].[dbo].[Log] L ";
                str = str + "INNER JOIN [MR_LPPA].[dbo].[Log_Tipos] LT ";
                str = str + "ON L.idTipo = LT.idTipo ";
                str = str + "ORDER BY L.Fecha desc";

                dt = Datos.connectToDB.fillTableSQL(str);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
