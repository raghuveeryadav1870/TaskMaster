using ASN.Models;

namespace ASN.IRepository
{
    public interface IDataTablePagerCommon
    {
        public DataTablePager GetPager(IFormCollection request);
    }
}
