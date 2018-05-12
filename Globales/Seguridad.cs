using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Globales
{
    public class Seguridad
    {
        public string tabla;
        public string id;

        public static List<Seguridad> verificarDV()
        {
            try
            {
                List<Seguridad> _myList = new List<Seguridad>();

                //Recorro las tablas de la base de datos
                string str = "USE MR_LPPA; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME NOT LIKE 'sys%'";
                DataTable dtTablas = Datos.connectToDB.fillTableSQL(str);
                List<string> listaTablas = new List<string>();

                if (dtTablas.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTablas.Rows.Count; i++)
                    {
                        listaTablas.Add(dtTablas.Rows[i]["TABLE_NAME"].ToString());
                    }
                }

                //Recorro cada tabla   
                foreach (string tabla in listaTablas)
                {
                    string queryDV = "select COUNT(0) from sys.columns where object_id = ";
                    queryDV = queryDV + "(select object_id from sys.tables where name = '"+ tabla +"') and name = 'DV'";

                    string countDV = Datos.connectToDB.readOneField(queryDV);

                    if (int.Parse(countDV) > 0)
                    {
                        str = "SELECT * FROM " + tabla;
                        DataTable miTabla = Datos.connectToDB.fillTableSQL(str);

                        if (miTabla.Rows.Count > 0)
                        {
                            for (int i = 0; i < miTabla.Rows.Count; i++)
                            {
                                string fila = "";
                                for (int j = 0; j < miTabla.Columns.Count - 1; j++)
                                {
                                    fila += miTabla.Rows[i][j].ToString();
                                }
                                fila = CalculateMD5Hash(fila);
                                if (fila != miTabla.Rows[i][miTabla.Columns.Count - 1].ToString())
                                {
                                    Seguridad _newError = new Seguridad();
                                    _newError.id = (i+1).ToString();
                                    _newError.tabla = tabla;
                                    _myList.Add(_newError);
                                }
                            }
                        }
                    }

                }

                return _myList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool grabarDV()
        {
            try
            {
                //Recorro las tablas de la base de datos
                string str = "USE MR_LPPA; SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME NOT LIKE 'sys%'";
                DataTable dtTablas = Datos.connectToDB.fillTableSQL(str);
                List<string> listaTablas = new List<string>();

                if (dtTablas.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTablas.Rows.Count; i++)
                    {
                        listaTablas.Add(dtTablas.Rows[i]["TABLE_NAME"].ToString());
                    }
                }

                //Recorro cada tabla   
                foreach (string tabla in listaTablas)
                {
                    string queryDV = "select COUNT(0) from sys.columns where object_id = ";
                    queryDV = queryDV + "(select object_id from sys.tables where name = '"+ tabla +"') and name = 'DV'";

                    string countDV = Datos.connectToDB.readOneField(queryDV);

                    if (int.Parse(countDV) > 0)
                    {
                        str = "USE MR_LPPA; SELECT * FROM " + tabla;
                        DataTable miTabla = Datos.connectToDB.fillTableSQL(str);

                        if (miTabla.Rows.Count > 0)
                        {
                            for (int i = 0; i < miTabla.Rows.Count; i++)
                            {
                                string fila = "";
                                for (int j = 0; j < miTabla.Columns.Count - 1; j++)
                                {
                                    fila += miTabla.Rows[i][j].ToString();
                                }
                                miTabla.Rows[i][miTabla.Columns.Count - 1] = CalculateMD5Hash(fila);
                            }
                            Datos.connectToDB.updateTable("USE MR_LPPA; SELECT * FROM " + tabla, miTabla);
                        }

                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static string CalculateMD5Hash(string input)
        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }

    }


}
