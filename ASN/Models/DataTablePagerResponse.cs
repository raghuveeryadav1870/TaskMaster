namespace ASN.Models
{
    public class DataTablePagerResponse
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public dynamic? data { get; set; }
    }
}
