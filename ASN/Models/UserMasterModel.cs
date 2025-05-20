using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace ASN.Models
{
    public class UserMasterModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string UserType { get; set; }
        public List<SelectListItem> Profilelst { get; set; }
        public bool Active { get; set; }
    }
}
