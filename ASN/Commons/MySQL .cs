using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace ASN.IRepository
{
    public class MySQL : IMySQL
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;

        public MySQL(IConfiguration configuration, ILogger<MySQL> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public DataTable ReturnSqlDataTable(string StoredProcedureName, List<MySqlParameter>? sqlParameter = null)
        {
            DataTable dataTable = new DataTable();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(StoredProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (sqlParameter != null)
                        {
                            cmd.Parameters.AddRange(sqlParameter.ToArray());
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing ReturnSqlDataTable.");
            }

            return dataTable;
        }

        public DataSet ReturnSqlDataSet(string StoredProcedureName, List<MySqlParameter>? sqlParameter = null)
        {
            DataSet dataSet = new DataSet();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(StoredProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (sqlParameter != null)
                        {
                            cmd.Parameters.AddRange(sqlParameter.ToArray());
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing ReturnSqlDataSet.");
            }

            return dataSet;
        }
    }
}
