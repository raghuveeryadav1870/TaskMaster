using ASN.Models;

namespace ASN.IRepository
{
    public interface IUserLogin
    {
        UserAuth SignIn(UserAuth userAuth);
    }
}
