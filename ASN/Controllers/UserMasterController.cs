using ASN.Commons;
using ASN.Filters;
using ASN.IRepository;
using ASN.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Principal;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;


namespace ASN.Controllers
{
    [ServiceFilter(typeof(FilterAuthCheck))]
    public class UserMasterController : Controller
    {
        readonly IConfiguration _configuration;
        readonly ILogger<MySQL> _logger;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IWebHostEnvironment _hostingEnvironment;

        public UserMasterController(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessors, IWebHostEnvironment environment)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessors;
            this._hostingEnvironment = environment;
        }
        public IActionResult UserMaster(UserMasterModel obj)
        {
            return View(obj);
        }
        [HttpPost]
        public JsonResult Get(UserMasterModel obj, IFormCollection request)
        {
            IUserMaster objCommon = new UserMasterCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            IDataTablePagerCommon objDataTablePagerCommon = new DataTablePagerCommon();
            DataTablePager objDataTablePager = objDataTablePagerCommon.GetPager(request);
            objDataTablePager = objCommon.Get(obj, objDataTablePager);
            var jsonResult = Json(new Models.DataTablePagerResponse() { draw = objDataTablePager.Draw, recordsFiltered = objDataTablePager.RecordsTotal, recordsTotal = objDataTablePager.RecordsTotal, data = objDataTablePager.data });

            return jsonResult;
        }
        [HttpGet]
        public ActionResult AddUserMaster(string ID)
        {
            UserMasterModel objModel = new UserMasterModel();
            IUserMaster objCommon = new UserMasterCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            IDropdown objIDropdownCommon = new DropdownCommon(_configuration, _logger);
            if (!string.IsNullOrEmpty(ID))
            {
                objModel = objCommon.GetByID(ID);
            }
            objModel.Profilelst = objIDropdownCommon.GetProfileList(true);
            return View(objModel);
        }
        [HttpPost]
        public JsonResult Save(IFormFile Img, string Data)
        {
            IUserMaster objCommon = new UserMasterCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            var obj = JsonConvert.DeserializeObject<UserMasterModel>(Data);
            if (obj != null)
            {
                var jsonResult = Json(JsonConvert.SerializeObject(objCommon.Save(obj)));
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
            IUserMaster objCommon = new UserMasterCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            var jsonResult = Json(JsonConvert.SerializeObject(objCommon.Deactivate(ID, IsActive)));
            return RedirectToAction("UserMaster");


        }
    }
}
