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

    }


  public  class NorthwindDataSet
    {
        public void select(ConnectionSettings set, string table)
        {

            string connectionString = "Data Source=" + set.Data_Source + "; " + "Initial Catalog=" +
                                      set.Initial_Catalog + "; " + "Integrated Security = TRUE;";
            ConnectToData(connectionString, table);
        }

        public void insert(ConnectionSettings set, DataTable table)
        {

            string connectionString = "Data Source=" + set.Data_Source + ";  " + "Initial Catalog=" +
                                      set.Initial_Catalog + ";" + "Integrated Security = TRUE;";
            ConnectToData1(connectionString, table);
        }

        private static DataSet ConnectToData(string connectionString, string table)
        {
            DataSet dataSet;
            //Create a SqlConnection to the Northwind database.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                //Create a SqlDataAdapter for the Suppliers table.
                SqlDataAdapter adapter = new SqlDataAdapter();

                // A table mapping names the DataTable.
                adapter.TableMappings.Add("Table", table);

                // Open the connection.
                connection.Open();
                string commandline = "SELECT * FROM " + table + '"';
                // Create a SqlCommand to retrieve Suppliers data.
                SqlCommand command = new SqlCommand(commandline, connection);
                command.CommandType = CommandType.Text;

                // Set the SqlDataAdapter's SelectCommand.
                adapter.SelectCommand = command;

                // Fill the DataSet.
                dataSet = new DataSet(table);
                adapter.Fill(dataSet);

                // Close the connection.
                connection.Close();

            }
            return dataSet;
        }

        private static void ConnectToData1(string connectionString, DataTable table)
        {

            //Create a SqlConnection to the Northwind database.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                string commandline = "SELECT * FROM " + table.TableName ;
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(commandline, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

             
                foreach ( DataRow tab in table.Rows)
                {
                    try
                    {
                        ds.Tables[0].ImportRow(tab);
                    }
                    catch(Exception e)
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


            }

        }
    }
}
