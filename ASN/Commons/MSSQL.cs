using ASN.IRepository;
using System.Data;
using System.Data.SqlClient;

namespace ASN.Commons
{
    public class MSSQL:IMSSQL
    {
        readonly SqlConnection _con;
        private readonly ILogger<MSSQL> _logger;
        private readonly IConfiguration _configuration;

        public MSSQL(IConfiguration _configurations, ILogger<MSSQL> _loggers)
        {
            _configuration = _configurations;
            _logger = _loggers;
            _con = new SqlConnection(ReturnConnectionString());
        }

        private string ReturnConnectionString()
        {
            return _configuration.GetConnectionString("DBCS").ToString();
        }

        public DataTable ReturnSqlDataTable(string StoredProcedureName, List<SqlParameter>? sqlParameter = null)
        {
            try
            {
                DataTable dt = new();
                using (SqlCommand cmd = new(StoredProcedureName, _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 6000;
                    if (sqlParameter != null)
                    {
                        if (sqlParameter.Count > 0)
                        {
                            foreach (SqlParameter parameter in sqlParameter)
                            {
                                cmd.Parameters.Add(parameter);
                            }
                        }
                    }
                    SqlDataAdapter da = new(cmd);
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                return new DataTable();
            }
            finally
            {
                SqlConnection.ClearPool(_con);
            }
        }

        public DataSet ReturnSqlDataSet(string StoredProcedureName, List<SqlParameter>? sqlParameter = null)
        {
            try
            {
                DataSet ds = new();
                using (SqlCommand cmd = new(StoredProcedureName, _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (sqlParameter != null)
                    {
                        if (sqlParameter.Count > 0)
                        {
                            foreach (SqlParameter parameter in sqlParameter)
                            {
                                cmd.Parameters.Add(parameter);
                            }
                        }
                    }
                    SqlDataAdapter da = new(cmd);
                    da.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                return new DataSet();
            }
            finally
            {
                SqlConnection.ClearPool(_con);
            }
        }

    }
}

