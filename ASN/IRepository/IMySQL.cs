
using System.Data;
using MySql.Data.MySqlClient; // MySQL-specific namespace

namespace ASN.IRepository
{
    public interface IMySQL
    {
        DataTable ReturnSqlDataTable(string StoredProcedureName, List<MySqlParameter>? sqlParameter = null);

        DataSet ReturnSqlDataSet(string StoredProcedureName, List<MySqlParameter>? sqlParameter = null);
    }
}
