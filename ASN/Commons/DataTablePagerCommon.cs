using ASN.IRepository;
using ASN.Models;
using ASN.IRepository;
using ASN.Models;
using System.Data.SqlClient;

namespace ASN.Commons
{
    public class DataTablePagerCommon: IDataTablePagerCommon
    {
        public DataTablePager GetPager(IFormCollection request)
        {
            DataTablePager objDataTablePager = new();
            if (request["draw"].FirstOrDefault() != null)
                objDataTablePager.Draw = Convert.ToInt32(request["draw"].FirstOrDefault());
            if (request["start"].FirstOrDefault() != null)
                objDataTablePager.Skip = Convert.ToInt32(request["start"].FirstOrDefault());
            if (request["length"].FirstOrDefault() != null)
                objDataTablePager.PageSize = Convert.ToInt32(request["length"].FirstOrDefault());
            if (request["columns[" + request["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault() != null)
                objDataTablePager.SortColumn = Convert.ToString(request["columns[" + request["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault());
            if (request["order[0][dir]"].FirstOrDefault() != null)
                objDataTablePager.SortColumnDir = Convert.ToString(request["order[0][dir]"].FirstOrDefault());
            if (request["search[value]"].FirstOrDefault() != null)
                objDataTablePager.SearchText = Convert.ToString(request["search[value]"].FirstOrDefault());
            return objDataTablePager;
        }
    }
}
