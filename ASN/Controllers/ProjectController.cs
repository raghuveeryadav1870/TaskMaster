using ASN.Commons;
using ASN.IRepository;
using ASN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ASN.Controllers
{
    public class ProjectController : Controller
    {
        readonly IConfiguration _configuration;
        readonly ILogger<MySQL> _logger;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IWebHostEnvironment _hostingEnvironment;

        public ProjectController(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessors, IWebHostEnvironment environment)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessors;
            this._hostingEnvironment = environment;
        }
        public IActionResult Project(ProjectModel obj)
        {
            return View(obj);
        }
        [HttpPost]
        public JsonResult Get(ProjectModel obj, IFormCollection request)
        {
            IProject objCommon = new ProjectCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            IDataTablePagerCommon objDataTablePagerCommon = new DataTablePagerCommon();
            DataTablePager objDataTablePager = objDataTablePagerCommon.GetPager(request);
            objDataTablePager = objCommon.Get(obj, objDataTablePager);
            var jsonResult = Json(new Models.DataTablePagerResponse() { draw = objDataTablePager.Draw, recordsFiltered = objDataTablePager.RecordsTotal, recordsTotal = objDataTablePager.RecordsTotal, data = objDataTablePager.data });

            return jsonResult;
        }
        public JsonResult GetSubProjects(String ProjectId)
        {
            IProject objCommon = new ProjectCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            IDropdown objIDropdownCommon = new DropdownCommon(_configuration, _logger);
            List<SelectListItem> SubprojectList = objIDropdownCommon.GetSubprojectList(ProjectId, true);
            var jsonResult = Json(SubprojectList);

            return jsonResult;
        }
        [HttpGet]
        public ActionResult AddProject(string ID)
        {
            ProjectModel objModel = new ProjectModel();
            IProject objCommon = new ProjectCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            IDropdown objIDropdownCommon = new DropdownCommon(_configuration, _logger);
            if (!string.IsNullOrEmpty(ID))
            {
                objModel = objCommon.GetByID(ID);
                objModel.lstSubProject = objIDropdownCommon.GetSubprojectList(Convert.ToString(objModel.Project), true);
            }
            else
            {

                objModel.lstSubProject = objIDropdownCommon.GetSubprojectList(Convert.ToString(0), true);
            }
            objModel.lstUser = objIDropdownCommon.GetUserList(Convert.ToString(0), true);
            objModel.lstProject = objIDropdownCommon.GetProjectList(Convert.ToString(0), true);
            objModel.lstStatus = objIDropdownCommon.GetStatusList(Convert.ToString(""), true);
            objModel.lstPriority = objIDropdownCommon.GetPriortyList(Convert.ToString(""), true);

            return View(objModel);
        }
        [HttpPost]
        public JsonResult Save(IFormFile Img, string Data)
        {
            IProject objCommon = new ProjectCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            var obj = JsonConvert.DeserializeObject<ProjectModel>(Data);
            if (obj != null)
            {
                var jsonResult = Json(JsonConvert.SerializeObject(objCommon.Save(obj,Img)));
                return jsonResult;
            }
            else
            {
                var jsonResult = Json(JsonConvert.SerializeObject(""));
                return jsonResult;
            }

        }
        public ActionResult Deactivate(string ID, string IsActive)
        {
            IProject objCommon = new ProjectCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            var jsonResult = Json(JsonConvert.SerializeObject(objCommon.Deactivate(ID, IsActive)));
            return RedirectToAction("ProjectMaster");


        }
    }
}
