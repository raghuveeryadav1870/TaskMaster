using ASN.Models;
using System.Data;

namespace ASN.IRepository
{
    public interface IUserMaster
    {
        DataTablePager Get(UserMasterModel obj, DataTablePager objDataTablePager);
        UserMasterModel GetByID(string ID);
        DataTable Save(UserMasterModel obj);
        UserMasterModel Deactivate(string ID, string Active);
    }
}
