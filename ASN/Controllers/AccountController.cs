using ASN.Commons;
using ASN.IRepository;
using ASN.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace ASN.Controllers
{
    public class AccountController : Controller
    {
      //  Models.UserAuth objUD = CommonFunction.GetUserIdFromSession();

        readonly IConfiguration _configuration;
        readonly ILogger<MySQL> _logger;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IWebHostEnvironment _hostingEnvironment;

        public AccountController(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessors, IWebHostEnvironment environment)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessors;
            this._hostingEnvironment = environment;
        }

     
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserAuth());
        }
        [HttpPost]
        public JsonResult Login(UserAuth userAuth)
        {
            IUserLogin userLogin = new UserLogin(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(userLogin.SignIn(userAuth)));
            return jsonResult;
            
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
