using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASN.Models
{
    public class AppModel
    {
        public class Details
        {
            public int UserID { get; set; }
            public string UserEmail { get; set; }
            public string? UserPassword { get; set; }
            public string? UserName { get; set; } = "";
            public string? UserType { get; set; }
            public string? uniqueDeviceId { get; set; }
            public string? deviceName { get; set; }
            public string? platform{ get; set; }
            public string? AppVersion { get; set; }
            public string? ipAddress { get; set; }
            public string? osVersion { get; set; }
            public int Result { get; set; }
            public string? Message { get; set; }
            public string? Action { get; set; }
            public List<Price>? Prices { get; set; }
            public List<Vehicle>? VehicleType { get; set; }
            //public int? ParkingId { get; set; }
            public ParkingDetails? ParkingDetails { get; set; }

        }
        public class ParkingDetails
        {
            public int? ParkingId { get; set; }
            public string? ParkingName { get; set; }
            public string GSTIN { get; set; }
            public string AuthorityName { get; set; }
            public string GateName { get; set; }
            public int GateID { get; set; }
        }
        public class Price
        {
            public int? PriceID { get; set; }
            public string? RangeFrom { get; set; }
            public string? RangeTo { get; set; }
            public decimal? Amount { get; set; }
            public string? VehicleType { get; set; }
            public int ParkingID { get; set; }
            public int? VehicleTypeID { get; set; }
        }
        public class Vehicle {
            public int? VehicleTypeID { get; set; }
            public string? VehicleType { get; set; }
        }
        public class Forgot
        {
            public int UserID { get; set; }
            public string UserEmail { get; set; }
            public int? OTP { get; set; }
            public int Result { get; set; }
            public string? Message { get; set; }

        }
        public class ResetPassword
        {
            public int UserID { get; set; }
            public string UserEmail { get; set; }
            public string NPassword { get; set; }
            public int Result { get; set; }
            public string? Message { get; set; }

        }
        public class AppBookingModel
        {

            public int? ParkingID { get; set; }
            //public List<SelectListItem>? lstParkingID { get; set; }
            //public List<SelectListItem>? lstVehicleType { get; set; }
            public int? VehicleType { get; set; }
            public int? Gate { get; set; }
            public int? Action { get; set; }
            //public List<SelectListItem>? lstGate { get; set; }
            public string? VehicleNumber { get; set; }
            public int DeviceId { get; set; }
            public int? BookingID { get; set; }
            public string? BookingNumber { get; set; }
            public decimal? TicketLost { get; set; }
            public int? Result { get; set; }
            public string? Message { get; set; }
            public int? CreatedBy { get; set; }
        }
        public class CheckOutDataModel
        {
            public int? ParkingID { get; set; }
            public string? Vehicle { get; set; }
            public string? CheckInTime { get; set; }
            public int? Price { get; set; }
            public string? BookingNumber { get; set; }
            public bool? IsRental { get; set; }
            public string? VehicleNumber { get; set; }
            //public int GSTSlabID { get; set; }
            public decimal GSTAmount { get; set; }
            public decimal TaxableAmount { get; set; }
            public int? Result { get; set; }
            public string? Message { get; set; }

        }
        public class CheckOutSaveModel
        {
            public int? ParkingID { get; set; }
            public string? VehicleNumber { get; set; }
            public int? Price { get; set; }
            //public int? GSTSlabID { get; set; }
            public decimal? GSTAmount { get; set; }
            public bool? IsFree { get; set; }
            public decimal? TaxableAmount { get; set; }
            public int? Device { get; set; }
            public string? Transactionid { get; set; }
            public string? ParmentMode { get; set; }
            public string? BookingNumber { get; set; }
            public string? TransactionData { get; set; }
            public int? CreatedBy { get; set; }
            public int? Result { get; set; }
            public string? Message { get; set; }

        }
       
    }
}
