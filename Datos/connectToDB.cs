using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class connectToDB
    {
        static SqlConnection _objConnectionSql;

        static SqlConnection objConnectionSql()
        {
            _objConnectionSql = new SqlConnection("Data Source=asm;Initial Catalog=MR_LPPA;Integrated Security=True");
            
            return _objConnectionSql;

        }

        public static void updateTable(String selectCommand, DataTable dtable)
        {
            SqlDataAdapter dAdapter = new SqlDataAdapter(selectCommand, objConnectionSql());
            SqlCommandBuilder cBuilder = new SqlCommandBuilder(dAdapter);
            dAdapter.InsertCommand = cBuilder.GetInsertCommand();
            dAdapter.DeleteCommand = cBuilder.GetDeleteCommand();
            dAdapter.UpdateCommand = cBuilder.GetUpdateCommand();
            dAdapter.Update(dtable);
        }

        public static DataTable fillTableSQL(String selectCommand)
        {

            try
            {

                SqlDataAdapter dAdapter = new SqlDataAdapter(selectCommand, objConnectionSql());

                DataTable dTable = new DataTable();

                dAdapter.SelectCommand.CommandTimeout = 0;

                dAdapter.Fill(dTable);

                return dTable;

            }

            catch (Exception)
            {



                return null;

            }

        }

        public static void launchCommand(String selectCommand)
        {

            SqlCommand command = new SqlCommand();
            command.CommandText = selectCommand;
            command.CommandType = CommandType.Text;
            command.Connection = objConnectionSql();
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();

        }

        public static string readOneField(String selectCommand)
        {

            string result = "";

            SqlCommand command = new SqlCommand();

            command.CommandText = selectCommand;

            command.CommandType = CommandType.Text;

            command.Connection = objConnectionSql();

            command.Connection.Open();

            result = command.ExecuteScalar().ToString();

            command.Connection.Close();

            return result;

        }

        public static bool launchCommandsWithTransaction(List<string> commands)
        {



            SqlConnection conex = objConnectionSql();

            SqlTransaction trans;

            conex.Open();

            trans = conex.BeginTransaction();



            try
            {

                foreach (string str in commands)
                {

                    SqlCommand comando = new SqlCommand(str, conex, trans);

                    comando.ExecuteNonQuery();

                }

                trans.Commit();

                return true;

            }

            catch (Exception)
            {



                trans.Rollback();

                return false;

            }

            finally
            {

                conex.Close();

            }

        }

        public static int launchCommandWithRead(string selectCommand, string fieldToRead)
        {
            try
            {
                var command = new SqlCommand();
                command.CommandText = selectCommand;
                command.CommandType = CommandType.Text;
                command.Connection = objConnectionSql();
                command.Connection.Open();
                command.ExecuteNonQuery();

                command.CommandText = "select @@IDENTITY";

                int identity = Convert.ToInt32(command.ExecuteScalar());

                command.Connection.Close();

                return identity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void launchCommandSQLUser(String selectCommand)
        {
            SqlConnection newConnection = new SqlConnection("Data Source=.;Initial Catalog=master;User ID=adm;Password=");
            
            SqlCommand command = new SqlCommand();
            command.CommandText = selectCommand;
            command.CommandType = CommandType.Text;
            command.Connection = newConnection;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();

        }
    }
}
