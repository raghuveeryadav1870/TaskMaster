using ASN.Commons;
using ASN.IRepository;
using ASN.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using static ASN.Models.AppModel;

namespace ASN.Controllers
{
    [Route("api/{controller}/{action}")]
    [ApiController]
    public class AppController : Controller
    {
        readonly IConfiguration _configuration;
        readonly ILogger<MySQL> _logger;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IWebHostEnvironment _hostingEnvironment;

        public AppController(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessors, IWebHostEnvironment environment)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessors;
            this._hostingEnvironment = environment;
        }

        [HttpPost]
        public string VerifyLogin(Details Logindata)
        {
            try
            {
                if (Logindata == null)
                {
                    throw new Exception("Validation failed.");
                }
                if (!ModelState.IsValid)
                {
                    throw new Exception("Validation failed.");
                }
                IApp userLogin = new AppCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
                Details Data = userLogin.SignIn(Logindata);
                var jsonResult = JsonConvert.SerializeObject(Data);
                if (Data.Result == 1)
                    return jsonResult;
                else
                    return jsonResult;

            }
            catch (Exception ex)
            {
                var jsonResult = " ";
                return jsonResult;
            }
        }
        [HttpPost]
        public string ForgotPassword(Forgot Forgotdata)
        {
            try
            {
                if (Forgotdata == null)
                {
                    throw new Exception("Validation failed.");
                }
                if (!ModelState.IsValid)
                {
                    throw new Exception("Validation failed.");
                }
                IApp forgotdata = new AppCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
                Forgot Data = forgotdata.ForgotPassword(Forgotdata);
                var jsonResult = JsonConvert.SerializeObject(Data);
                if (Data.Result == 1)
                    return jsonResult;
                else
                    return jsonResult;

            }
            catch (Exception ex)
            {
                var jsonResult = " ";
                return jsonResult;
            }
        }
        [HttpPost]
        public string ResetPassword(ResetPassword Forgotdata)
        {
            try
            {
                if (Forgotdata == null)
                {
                    throw new Exception("Validation failed.");
                }
                if (!ModelState.IsValid)
                {
                    throw new Exception("Validation failed.");
                }
                IApp forgotdata = new AppCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
                ResetPassword Data = forgotdata.ResetPassword(Forgotdata);
                var jsonResult = JsonConvert.SerializeObject(Data);
                if (Data.Result == 1)
                    return jsonResult;
                else
                    return jsonResult;

            }
            catch (Exception ex)
            {
                var jsonResult = " ";
                return jsonResult;
            }
        }
      
    }
}
