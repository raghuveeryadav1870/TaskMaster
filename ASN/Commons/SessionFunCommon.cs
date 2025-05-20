using ASN.IRepository;
using ASN.Models;

namespace ASN.Commons
{
    public class SessionFunCommon : ISessionFunCommon
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionFunCommon(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserAuth GetUserData()
        {
            UserAuth userAuth = new();
            try
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    string? strsession = SessionExtensions.GetString(_httpContextAccessor.HttpContext.Session, "UserSession");
                    if (!string.IsNullOrEmpty(strsession))
                    {
                        string? finalstr = new SecureDatails().Decrypt(strsession);
                        if (!string.IsNullOrEmpty(finalstr))
                        {
                            UserAuth? userAuth1 = System.Text.Json.JsonSerializer.Deserialize<UserAuth>(finalstr);
                            if (userAuth1 != null)
                            {
                                userAuth = userAuth1;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return userAuth;
        }
        
    }
}


