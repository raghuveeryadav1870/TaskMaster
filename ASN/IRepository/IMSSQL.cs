using System.Data;
using System.Data.SqlClient;

namespace ASN.IRepository
{
    public interface IMSSQL
    {
        DataTable ReturnSqlDataTable(string StoredProcedureName, List<SqlParameter>? sqlParameter = null);

        DataSet ReturnSqlDataSet(string StoredProcedureName, List<SqlParameter>? sqlParameter = null);
    }
}
