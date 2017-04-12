using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace BDOperational
{

    public struct ConnectionSettings
    {

        public string Data_Source;
        public string Initial_Catalog;
        public string user_name;
        public string password;

    }


    public class NorthwindDataSet
    {
        public DataSet selectTable(ConnectionSettings set)
        {
            string connectionString = "Data Source=" + set.Data_Source + "; " + "Initial Catalog=" +
                                       set.Initial_Catalog + "; " + "Integrated Security = TRUE;";



            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                string commandline = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG ='" + set.Initial_Catalog + "'";

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(commandline, connection);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception e)
                {

                }
                connection.Close();
                return ds;
            }
        }

        public DataSet select(ConnectionSettings set, string table)
        {

            string connectionString = "Data Source=" + set.Data_Source + "; " + "Initial Catalog=" +
                                      set.Initial_Catalog + "; " + "Integrated Security = TRUE;";
            return ConnectToData(connectionString, table);
        }

        public void insert(ConnectionSettings set, DataTable table)
        {

            string connectionString = "Data Source=" + set.Data_Source + ";  " + "Initial Catalog=" +
                                      set.Initial_Catalog + ";" + "Integrated Security = TRUE;";
            ConnectToData1(connectionString, table);
        }

        private static DataSet ConnectToData(string connectionString, string table)
        {
            using (SqlConnection connection =
                 new SqlConnection(connectionString))
            {
                string commandline = "SELECT * FROM " + table;
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(commandline, connection);
                DataSet ds = new DataSet();
                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception e)
                {

                }
                connection.Close();
                return ds;
            }

        }

        private static void ConnectToData1(string connectionString, DataTable table)
        {

            //Create a SqlConnection to the Northwind database.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                string commandline = "SELECT * FROM " + table.TableName;
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(commandline, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);


                foreach (DataRow tab in table.Rows)
                {
                    try
                    {
                        ds.Tables[0].ImportRow(tab);
                    }
                    catch (Exception e)
                    {

                    }
                }
                // создаем объект SqlCommandBuilder
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                try
                {
                    adapter.Update(ds);
                }
                catch (Exception e)
                {

                }
                // альтернативный способ - обновление только одной таблицы
                //adapter.Update(dt);
                // заново получаем данные из бд
                // очищаем полностью DataSet
                ds.Clear();
                // перезагружаем данные
                adapter.Fill(ds);
                connection.Close();


            }

        }
    }
}
