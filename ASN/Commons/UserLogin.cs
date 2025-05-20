using ASN.IRepository;
using System.Data;
using ASN.Models;
using ASN.IRepository;
using System.Data;
using System.Text;
using ASN.Models;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using Org.BouncyCastle.Crypto.Parameters;
using System.Data.SqlClient;

namespace ASN.Commons
{
   
    public class UserLogin : IUserLogin
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;
        private readonly IMySQL _mySQL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment? _hostingEnvironment;

        public UserLogin(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment? environment)
        {
            _configuration = configuration;
            _logger = logger;
            _mySQL = new MySQL(_configuration, _logger);
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
        }
        List<MySqlParameter>? userLoginParameter;

        public UserAuth SignIn(UserAuth userAuthL)
        {
            try
            {
                UserAuth userAuth = new UserAuth();
                ISecureDatails secureDatails = new SecureDatails();
                var userLoginParameter = new List<MySqlParameter>
           
        {
            new MySqlParameter("Email", userAuthL.UserEmail),  
            new MySqlParameter("Password", userAuthL.UserPassword)
        };
                DataTable dtResult = _mySQL.ReturnSqlDataTable("K2d2_UserLogin", userLoginParameter);

                if (dtResult != null && dtResult.Rows.Count > 0)
                {
                    DataRow row = dtResult.Rows[0];
                    userAuth.UserID = Convert.ToInt32(row["UserId"]); 
                    userAuth.UserEmail = row["UserEmail"].ToString();
                    userAuth.UserType = row["UserType"].ToString();
                    userAuth.UserName = row["UserName"].ToString();
                    
                    userAuth.Result = Convert.ToInt32(row["Result"]);
                    if (_httpContextAccessor.HttpContext != null)
                    {
                        UserAuth userAuthSession = new UserAuth
                        {
                            UserID = userAuth.UserID,
                            UserEmail = userAuth.UserEmail,
                            UserType = userAuth.UserType,
                            UserName = userAuth.UserName
                        };
                        string encryptedUserAuth = secureDatails.Encrypt(System.Text.Json.JsonSerializer.Serialize(userAuthSession));
                        SessionExtensions.SetString(_httpContextAccessor.HttpContext.Session, "UserSession", encryptedUserAuth);
                    }
                }

                return userAuth;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", ex.Message);
                throw;
            }
        
        }
    }
}
