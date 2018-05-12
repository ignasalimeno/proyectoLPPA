using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globales
{
    public class Backup
    {
        public static string generarBackup()
        {
            try
            {
                string nombre = "MR_LPPA_Backup_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".bak";
                string str = "backup database [MR_LPPA] to disk = 'C:\\TEMP\\" + nombre + "'";
                Datos.connectToDB.launchCommand(str);
                return nombre;
            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }
        public static void borrarBackup(string ruta)
        {
            try
            {
                if ((System.IO.File.Exists(ruta)))
                {
                    System.IO.File.Delete(ruta);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void restaurarBackup(string path, string database)
        {
            try
            {
                string[] nombreArchivoArray = path.Split('\\');
                string nombreArchivo = nombreArchivoArray[nombreArchivoArray.Length - 1];
                
                //string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                ////Datos.connectToDB.launchCommandSQLUser(sqlStmt2);

                ////string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='"+ path +"' WITH REPLACE;";
                //string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='C:\\TEMP\\"+ nombreArchivo +"' WITH REPLACE;";
                //Datos.connectToDB.launchCommandSQLUser(sqlStmt3);

                //string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                //Datos.connectToDB.launchCommandSQLUser(sqlStmt4);

                string str = "IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'MR_LPPA') begin EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'MR_LPPA';USE [master];ALTER DATABASE [MR_LPPA] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE;USE [master];DROP DATABASE [MR_LPPA] end";
                Datos.connectToDB.launchCommandSQLUser(str);
                string str1 = "RESTORE DATABASE [MR_LPPA] FROM DISK = 'C:\\TEMP\\" + nombreArchivo + "'";
                Datos.connectToDB.launchCommandSQLUser(str1);

            }
            catch (Exception)
            {
                
                throw;
            }

        }
    }
}
