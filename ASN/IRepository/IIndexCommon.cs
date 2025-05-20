using System.Data;

namespace ASN.IRepository
{
    public interface IIndexCommon
    {
        public string RevenueGraph();
        public string VehicleGraph();
        public DataTable DashboardData();
    }
}
