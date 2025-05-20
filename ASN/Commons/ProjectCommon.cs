using ASN.IRepository;
using ASN.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Zlib;
using System.Data;
using System.Data.SqlClient;

namespace ASN.Commons
{
    public class ProjectCommon : IProject
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMySQL _mySQL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProjectCommon(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _logger = logger;
            _mySQL = new MySQL(_configuration, _logger);
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
        }

        List<MySqlParameter>? objParameter;

        public DataTablePager Get(ProjectModel obj, DataTablePager objDataTablePager)
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
                DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_TaskMasterGet", objParameter);
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
        public ProjectModel GetByID(string ID)
        {
            ProjectModel obj = new ProjectModel();
            objParameter = new List<MySqlParameter>();
            objParameter.Add(new MySqlParameter("@pk", ID));
            DataTable dtResult = _mySQL.ReturnSqlDataTable("K2D2_TaskGetByID", objParameter);
            if (dtResult.Rows.Count > 0)
            {
                obj.ID = Convert.ToInt32(dtResult.Rows[0]["ID"]);
                obj.Project = Convert.ToInt32(dtResult.Rows[0]["Project"]);
                obj.SubProject = Convert.ToInt32(dtResult.Rows[0]["SubProject"]);
                obj.Title = dtResult.Rows[0]["Title"].ToString();
                obj.Description = dtResult.Rows[0]["Description"].ToString();
                obj.Priority = dtResult.Rows[0]["Priority"].ToString();
                obj.Status = dtResult.Rows[0]["Status"].ToString();
                obj.StartDate = dtResult.Rows[0]["Startedon"].ToString();
                obj.EndDate = dtResult.Rows[0]["Endedon"].ToString();
                obj.TotalHours = dtResult.Rows[0]["Manhours"].ToString();
                obj.AllocatedBy = Convert.ToInt32(dtResult.Rows[0]["AllocatedBy"]);
                obj.TakenCareBy = Convert.ToInt32(dtResult.Rows[0]["TakenCareBy"]);
                obj.TestedBy     = Convert.ToInt32(dtResult.Rows[0]["TestedBy"]);
                obj.LiveBy = Convert.ToInt32(dtResult.Rows[0]["LiveBy"]);
            }
            return obj;
        }
        public static string MakeValidFileName(string name)
        {
            return System.Text.RegularExpressions.Regex.Replace(name, "[^a-zA-Z0-9_.]+", "-", System.Text.RegularExpressions.RegexOptions.Compiled);
        }
        public DataTable Save(ProjectModel obj, IFormFile Pic)
        {

            string filePath = "";
            string MFileName = "";
            if (Pic != null)
            {
                string UploadFilePath = string.Empty;
                
                FileInfo fl = new(Pic.FileName);
                MFileName = Pic.FileName;
                string MFileExtension = fl.Extension;
                string MFileNameWExtension = MFileName.Replace(MFileExtension, string.Empty);
                MFileNameWExtension = MakeValidFileName(MFileNameWExtension);
                string NewName = System.DateTime.Now.ToString("MMddyyyyhhmmss");
                MFileName = MFileName.Replace(MFileExtension, string.Empty) + NewName + MFileExtension;
                if (Pic.Length > 0)
                {
                    CommonFunction.UploadFileToAWSS3(null,"lp/UploadedFiles/Attachment/", MFileName, string.Empty);
                    filePath = "lp/UploadedFiles/Attachment/" + MFileName;
                }
            }

            ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);

            objParameter = new List<MySqlParameter>()
            {
                new MySqlParameter("@P_ID",obj.ID),
                new MySqlParameter("@P_Title",obj.Title),
                new MySqlParameter("@P_Description",obj.Description),
                new MySqlParameter("@P_Project",obj.Project),
                new MySqlParameter("@P_SubProject",obj.SubProject),
                new MySqlParameter("@p_AllocatedBy",obj.AllocatedBy),
                new MySqlParameter("@P_TakenCareBy",obj.TakenCareBy),
                new MySqlParameter("@p_TestedBy",obj.TestedBy),
                new MySqlParameter("@P_LiveBy",obj.LiveBy),
                new MySqlParameter("@P_StartDate",obj.StartDate),
                new MySqlParameter("@P_EndDate",obj.EndDate),
                new MySqlParameter("@P_TotalHours",obj.TotalHours),
                new MySqlParameter("@P_Status",obj.Status),
                new MySqlParameter("@P_Priority",obj.Priority),
                new MySqlParameter("@P_Pic", filePath),
                new MySqlParameter("@P_By",objSessionFunCommon.GetUserData().UserID),
            };

            DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_SaveTask", objParameter);
            return dtResult;

        }
        public ProjectModel Deactivate(string ID, string IsActive)
        {
            ProjectModel obj = new ProjectModel();
            objParameter = new List<MySqlParameter>();
            objParameter.Add(new MySqlParameter("@Pk", ID));
            objParameter.Add(new MySqlParameter("@IsActive", IsActive));
            DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_InsertProject", objParameter);
            if (dtResult.Rows.Count > 0)
            {
                obj.ID = Convert.ToInt32(dtResult.Rows[0]["Pk"]);
            }
            return obj;
        }

    }
}
