using ASN.Commons;
using ASN.Filters;
using ASN.IRepository;
using ASN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ASN.Controllers
{
    [ServiceFilter(typeof(FilterAuthCheck))]
    public class HomeController : Controller
    {
        readonly IConfiguration _configuration;
        readonly ILogger<MySQL> _logger;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessors, IWebHostEnvironment environment)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._httpContextAccessor = httpContextAccessors;
            this._hostingEnvironment = environment;
        }


        public IActionResult Index()
        {
            IIndexCommon objIndex = new IndexCommon(_configuration, _logger, _httpContextAccessor, _hostingEnvironment);
            ViewBag.RevenueGraph = objIndex.RevenueGraph();
            ViewBag.VehicleGraph = objIndex.VehicleGraph();
            ViewBag.DashboardData = objIndex.DashboardData();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
