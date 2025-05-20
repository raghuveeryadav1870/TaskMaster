using ASN.Models;
using System.Data;
using static ASN.Models.AppModel;

namespace ASN.IRepository
{
    public interface IApp
    {
        Details SignIn(Details userAuth);
        Forgot ForgotPassword(Forgot forgotdata);
        ResetPassword ResetPassword(ResetPassword forgotdata);
      
    }
}
