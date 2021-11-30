using System;
using System.Data;
using MySql.Data.MySqlClient;
using owasp_sqlinjection.Domain.Interfaces;

namespace owas_sqlinjection.Infrastructure.DBContexts
{
    public class MySqlDBContext : IDBContext
    {
        private readonly string connectionString;

        public MySqlDBContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int ExecuteNonQuery(string sql, List<MySqlParameter>? parameters)
        {
            int rowAffected = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = sql;
                    if (parameters != null && parameters.Count > 0)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    rowAffected = command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rowAffected;

        }

        public DataTable QueryList(string sql, List<MySqlParameter>? parameters)
        {
            MySqlDataReader? dataReader = null;
            DataTable? dataTable = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = sql;
                    if (parameters != null && parameters.Count > 0)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    dataReader = command.ExecuteReader();
                    dataTable = new DataTable();
                    dataTable.Load(dataReader);

                    //close the resources
                    dataReader.Close();
                    command.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dataTable;
        }
    }
}

