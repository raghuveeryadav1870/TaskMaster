using System.Data;

namespace ASN.IRepository
{
    public interface ICommonFunction
    {
        public List<object> DataTableToSerializer(DataTable table);
    }
}
