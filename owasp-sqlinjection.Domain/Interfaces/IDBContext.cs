using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace owasp_sqlinjection.Domain.Interfaces
{
    public interface IDBContext
    {
        DataTable QueryList(string sql, List<MySqlParameter>? parameters);
        int ExecuteNonQuery(string sql, List<MySqlParameter>? parameters);
    }
}

