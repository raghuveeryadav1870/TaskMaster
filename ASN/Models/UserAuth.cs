using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASN.Models
{
    public class UserAuth
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UserName { get; set; } = "";
        public string? UserType { get; set; }
        public int? ParkingID { get; set; }
        public int? GateID { get; set; }
        public int Result { get; set; }
        public string? Message { get; set; }
        public string? Action { get; set; }
        public int? OTP { get; set; }
    }
}
