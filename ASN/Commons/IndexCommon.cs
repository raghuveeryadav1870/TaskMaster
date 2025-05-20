using ASN.IRepository;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace ASN.Commons
{
    public class IndexCommon : IIndexCommon
    {
        List<MySqlParameter>? _Parameter;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMySQL _mySQL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexCommon(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _logger = logger;
            _mySQL = new MySQL(_configuration, _logger);
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
        }

        public string RevenueGraph()
        {
            try
            {
                DataTable dtResult = RevenueData();
                string statusData = ReturnJsonGraphData(dtResult);
                return statusData;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_Parameter != null)
                    _Parameter = null;
            }

        }
        public string VehicleGraph()
        {
            try
            {
                DataTable dtResult = VehicleData();
                string statusData = ReturnJsonGraphData(dtResult);
                return statusData;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_Parameter != null)
                    _Parameter = null;
            }

        }
        public DataTable VehicleData()
        {
            try
            {
                _Parameter = new List<MySqlParameter>();

                DataTable dtResult = _mySQL.ReturnSqlDataTable("park_parkDailyVehcileGraph", _Parameter);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_Parameter != null)
                    _Parameter = null;
            }
        }
        public DataTable DashboardData( )
        {
            try
            {
                _Parameter = new List<MySqlParameter>();

                DataTable dtResult = _mySQL.ReturnSqlDataTable("park_parkingDashboardData", _Parameter);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_Parameter != null)
                    _Parameter = null;
            }
        }
        public DataTable RevenueData()
        {
            try
            {
                _Parameter = new List<MySqlParameter>();

                DataTable dtResult = _mySQL.ReturnSqlDataTable("park_parkingDailyGraphData", _Parameter);
                return dtResult;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_Parameter != null)
                    _Parameter = null;
            }
        }
        public string ReturnJsonGraphData(DataTable dtResult)
        {
            StringBuilder jsonString = new StringBuilder();

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                //jsonString.Append("[");

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    jsonString.Append("{");
                    DataRow row = dtResult.Rows[i];

                    for (int j = 0; j < dtResult.Columns.Count; j++)
                    {
                        string columnName = dtResult.Columns[j].ColumnName;
                        string value = row[columnName]?.ToString() ?? ""; 

                        jsonString.Append("\"" + columnName + "\": ");

                        if (string.IsNullOrEmpty(value))
                        {
                            jsonString.Append("0"); 
                        }
                        else
                        {
                            jsonString.Append("\"" + value + "\"");
                        }

                        if (j < dtResult.Columns.Count - 1)
                        {
                            jsonString.Append(", ");
                        }
                    }

                    jsonString.Append("}");

                    if (i < dtResult.Rows.Count - 1)
                    {
                        jsonString.Append(", ");
                    }
                }

               // jsonString.Append("]");
            }

            return jsonString.ToString();
        }


    }
}
