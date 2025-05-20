using ASN.IRepository;
using ASN.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace ASN.Commons
{
    public class UserMasterCommon : IUserMaster
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMySQL _mySQL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserMasterCommon(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _logger = logger;
            _mySQL = new MySQL(_configuration, _logger);
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
        }

        List<MySqlParameter>? objParameter;

        public DataTablePager Get(UserMasterModel obj, DataTablePager objDataTablePager)
        {
            try
            {
                objParameter = new List<MySqlParameter>();
                ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);
                objParameter.Add(new MySqlParameter("@SortColumn", objDataTablePager.SortColumn));
                objParameter.Add(new MySqlParameter("@SortColumnDir", objDataTablePager.SortColumnDir));
                objParameter.Add(new MySqlParameter("@SkipPage", objDataTablePager.Skip));
                objParameter.Add(new MySqlParameter("@PageSize", objDataTablePager.PageSize));
                objParameter.Add(new MySqlParameter("@SearchText", objDataTablePager.SearchText));
                DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_UserMasterGets", objParameter);
                if (dtResult.Rows.Count > 0)
                {
                    objDataTablePager.RecordsTotal = Convert.ToInt32(dtResult.Rows[0]["RecordsTotalCount"]);
                    ICommonFunction objCommonFunction = new CommonFunction(_configuration, _logger, _httpContextAccessor);
                    objDataTablePager.data = objCommonFunction.DataTableToSerializer(dtResult);
                }
                else
                {
                    objDataTablePager.RecordsTotal = 0;
                    objDataTablePager.data = new List<object>();
                }

                return objDataTablePager;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", ex.Message);
                throw;
            }
            finally
            {
                if (objParameter != null)
                    objParameter = null;
            }
        }
        public UserMasterModel GetByID(string ID)
        {
            UserMasterModel obj = new UserMasterModel();
            objParameter = new List<MySqlParameter>();
            objParameter.Add(new MySqlParameter("@ID", ID));
            DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_UserMasterGetByID", objParameter);
            if (dtResult.Rows.Count > 0)
            {
                obj.UserID = Convert.ToInt32(dtResult.Rows[0]["UserID"]);
                obj.UserName = dtResult.Rows[0]["UserName"].ToString();
                obj.UserEmail = dtResult.Rows[0]["UserEmail"].ToString();
                obj.UserPhone = dtResult.Rows[0]["phone"].ToString();
                obj.UserType = dtResult.Rows[0]["UserType"].ToString();
                obj.UserPassword = dtResult.Rows[0]["UserPassword"].ToString();
            }
            return obj;
        }

        public DataTable Save(UserMasterModel obj)
        {
            ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);
            objParameter = new List<MySqlParameter>
            {
                new MySqlParameter("@p_ID", obj.UserID),
                new MySqlParameter("@p_UserName", obj.UserName),
                new MySqlParameter("@p_UserPWD", obj.UserPassword),
                 new MySqlParameter("@p_phone", obj.UserPhone),
                 new MySqlParameter("@p_ProfileType", obj.UserType),
                 new MySqlParameter("@p_UserEmailID", obj.UserEmail),
                 new MySqlParameter("@p_Active", obj.Active),
                new MySqlParameter("@p_BY",  objSessionFunCommon.GetUserData().UserID),
            };

            DataTable dtResult = _mySQL.ReturnSqlDataTable("K2d2_SaveUserMaster", objParameter);
            return dtResult;

        }
        public UserMasterModel Deactivate(string ID, string IsActive)
        {
            UserMasterModel obj = new UserMasterModel();
            objParameter = new List<MySqlParameter>();
            objParameter.Add(new MySqlParameter("@Pk", ID));
            objParameter.Add(new MySqlParameter("@IsActive", IsActive));
            DataTable dtResult = _mySQL.ReturnSqlDataTable("K2d2_UserMaster_Deactivate", objParameter);
            if (dtResult.Rows.Count > 0)
            {
                obj.UserID = Convert.ToInt32(dtResult.Rows[0]["Pk"]);
            }
            return obj;
        }
    }
}
