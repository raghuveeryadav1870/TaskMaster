using ASN.Models;
using System.Data;

namespace ASN.IRepository
{
    public interface IProject
    {
        DataTablePager Get(ProjectModel obj, DataTablePager objDataTablePager);
        ProjectModel GetByID(string ID);
        DataTable Save(ProjectModel obj,IFormFile Pic);
        ProjectModel Deactivate(string ID, string Active);
    }
}
