using ASN.IRepository;
using ASN.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using static ASN.Models.AppModel;
namespace ASN.Commons
{
    public class AppCommon : IApp
    {

        ISecureDatails secureDatails = new SecureDatails();
        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMySQL _mySQL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppCommon(IConfiguration configuration, ILogger<MySQL> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _logger = logger;
            _mySQL = new MySQL(_configuration, _logger);
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = environment;
        }

        List<MySqlParameter>? objParameter;
        public Details SignIn(Details userAuthL)
        {
            try
            {
                Details userAuth = new Details();
                ParkingDetails objParking = new ParkingDetails();
                ISecureDatails secureDatails = new SecureDatails();
                var userLoginDataParameter = new List<MySqlParameter>

            {
                new MySqlParameter("UserEmail", userAuthL.UserEmail),
                new MySqlParameter("UserPassword", userAuthL.UserPassword),
                new MySqlParameter("uniqueDeviceId", userAuthL.uniqueDeviceId),
                new MySqlParameter("deviceName", userAuthL.deviceName),
                new MySqlParameter("platform", userAuthL.platform),
                new MySqlParameter("AppVersion", userAuthL.AppVersion),
                new MySqlParameter("ipAddress", userAuthL.ipAddress),
                new MySqlParameter("osVersion", userAuthL.osVersion)
            };
                using (DataTable dt = _mySQL.ReturnSqlDataTable("PARK_PARKAgentDeviceTran", userLoginDataParameter))
                {
                    if(dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr =  dt.Rows[0];
                        userAuth.Result = Convert.ToInt32(dr["Result"]);
                        if (userAuth.Result > 0) {
                            userAuthL.UserEmail = dr["UserEmail"].ToString();
                            userAuthL.UserPassword = dr["UserPassword"].ToString();
                        var userLoginParameter = new List<MySqlParameter>

                        {
                            new MySqlParameter("p_Email", userAuthL.UserEmail),
                            new MySqlParameter("p_Password", userAuthL.UserPassword)
                        };
                    DataTable dtResult = _mySQL.ReturnSqlDataTable("PARK_PARKUsers", userLoginParameter);

                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        DataRow row = dtResult.Rows[0];
                        userAuth.UserID = Convert.ToInt32(row["UserId"]);
                        userAuth.UserEmail = row["UserEmail"].ToString();
                        userAuth.UserType = row["UserType"].ToString();
                        userAuth.UserName = row["UserName"].ToString();
                        //userAuth.ParkingId = Convert.ToInt32(row["ParkingId"]);
                        userAuth.Result = Convert.ToInt32(row["Result"]);
                        userAuth.Message = "Successfully Login";
                        userAuth.Action = " ";

                        objParking.ParkingId = Convert.ToInt32(row["ParkingId"]);
                        objParking.ParkingName = Convert.ToString(row["ParkingName"]);
                        objParking.GSTIN = Convert.ToString(row["GSTIN"]);
                        objParking.AuthorityName = Convert.ToString(row["AuthorityName"]);
                        objParking.GateName = Convert.ToString(row["Gate"]);
                        objParking.GateID = Convert.ToInt32(row["GateID"]);
                        userAuth.ParkingDetails = objParking;

                        //IDropdown objIDropdownCommon = new DropdownCommon(_configuration, _logger);
                        //userAuth.lstVehicleType = objIDropdownCommon.GetVechialTypeList(Convert.ToString(0), true);

                        objParameter = new List<MySqlParameter>();
                        List<Price> objlstPriceModel = new List<Price>();
                        objParameter.Add(new MySqlParameter("@ParkingID", Convert.ToInt32(objParking.ParkingId)));
                        objParameter.Add(new MySqlParameter("@VehicleType", Convert.ToInt32(0)));
                        DataSet dtResult2 = _mySQL.ReturnSqlDataSet("PARK_PARKPriceMaster_Gets", objParameter);
                        if (dtResult2.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in dtResult2.Tables[0].Rows)
                            {
                                Price obj = new Price();
                                obj.PriceID = Convert.ToInt32(item["PriceID"]);
                                obj.VehicleTypeID = Convert.ToInt32(item["VehicleTypeID"]);
                                obj.VehicleType = item["VechialType"].ToString();
                                obj.RangeFrom = item["RangeFrom"].ToString();
                                obj.RangeTo = item["RangeTo"].ToString();
                                obj.Amount = Convert.ToDecimal(item["Amount"]);
                                obj.ParkingID = Convert.ToInt32(item["ParkingID"]);
                                objlstPriceModel.Add(obj);
                            }
                        }
                        userAuth.Prices = objlstPriceModel;
                        List<Vehicle> objlstVehicleTypeModel = new List<Vehicle>();
                        DataSet dtResult3 = _mySQL.ReturnSqlDataSet("PARK_PARKVechialTypeMaster_Gets", objParameter);
                        if (dtResult3.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow item in dtResult3.Tables[0].Rows)
                            {
                                Vehicle objVehicle = new Vehicle();
                                objVehicle.VehicleTypeID = Convert.ToInt32(item["vehicalTypeID"]);
                                objVehicle.VehicleType = item["VechialType"].ToString();
                                objlstVehicleTypeModel.Add(objVehicle);
                            }
                        }
                        userAuth.VehicleType = objlstVehicleTypeModel;
                    }
                    else
                    {
                        userAuth.UserID = 0;
                        userAuth.UserEmail = " ";
                        userAuth.UserType = " ";
                        userAuth.UserName = " ";
                        userAuth.Result = 0;
                        userAuth.Message = "Invalid Details ";
                        userAuth.Action = " ";

                    }
                    }
                        else
                        {
                            userAuth.UserID = 0;
                            userAuth.UserEmail = " ";
                            userAuth.UserType = " ";
                            userAuth.UserName = " ";
                            userAuth.Result = 0;
                            userAuth.Message = Convert.ToString(dt.Rows[0]["ErrorMessage"]);
                            userAuth.Action = " ";
                        }

                    }
                    else
                    {
                        userAuth.UserID = 0;
                        userAuth.UserEmail = " ";
                        userAuth.UserType = " ";
                        userAuth.UserName = " ";
                        userAuth.Result = 0;
                        userAuth.Message = Convert.ToString(dt.Rows[0]["ErrorMessage"]);
                        userAuth.Action = " ";
                    }
                    return userAuth;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", ex.Message);
                throw;
            }
        }
        public Forgot ForgotPassword(Forgot Forgotdata)
        {
            var userLoginParameter = new List<MySqlParameter>

        {
            new MySqlParameter("p_Email", Forgotdata.UserEmail),
        };
            try
            {
                using (DataTable dt = _mySQL.ReturnSqlDataTable("PARK_PARKForgotUser", userLoginParameter))
                {
                    if (dt.Rows.Count > 0)
                    {
                        Forgotdata.UserID = Convert.ToInt32(dt.Rows[0]["UserId"]);
                        Forgotdata.UserEmail = Convert.ToString(dt.Rows[0]["UserEmail"]);
                        Forgotdata.Result = Convert.ToInt32(dt.Rows[0]["Result"]);
                        Random rnd = new Random();
                        Forgotdata.OTP = rnd.Next(100000, 1000000);
                        Forgotdata.Message = "Successful";
                    }
                    else
                    {
                        Forgotdata.Message = "Invalid Email";
                    }
                }

                return Forgotdata;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (userLoginParameter != null)
                    userLoginParameter = null;
            }

        }
        public ResetPassword ResetPassword(ResetPassword Forgotdata)
        {
            var userLoginParameter = new List<MySqlParameter>

        {
            new MySqlParameter("Email", Forgotdata.UserEmail),
             new MySqlParameter("NPassword", Forgotdata.NPassword),
        };
            try
            {
                using (DataTable dt = _mySQL.ReturnSqlDataTable("PARK_PARKChangePassword", userLoginParameter))
                {
                    Forgotdata.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    Forgotdata.Result = Convert.ToInt32(dt.Rows[0]["Result"]);
                    Forgotdata.Message = "Successfully Reset password";
                }

                return Forgotdata;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (userLoginParameter != null)
                    userLoginParameter = null;
            }

        }
     
        public  List<CheckOutDataModel> CheckOutData(CheckOutDataModel obj)
        {
            try
            {
                objParameter = new List<MySqlParameter>();
                List<CheckOutDataModel> CheckoutDataList = new List<CheckOutDataModel>();
                ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);
                objParameter.Add(new MySqlParameter("@VehicleNumber", obj.VehicleNumber));
                objParameter.Add(new MySqlParameter("@ParkingID", obj.ParkingID));
                objParameter.Add(new MySqlParameter("@BookingNumber", obj.BookingNumber));
                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("PARK_PARKBooking_CheckInData", objParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            if (Convert.ToInt32(item["Result"]) > 0) { 
                            if (Convert.ToBoolean(item["IsRental"]) == true)
                            {
                                obj.Price = 0;
                                }
                            else
                            {
                                obj.Price = Convert.ToInt32(item["Amount"]);
                           }
                                CheckoutDataList.Add(new CheckOutDataModel()
                                {
                                    ParkingID = Convert.ToInt32(item["ParkingID"]),
                                    Result = Convert.ToInt32(item["Result"]),
                                    Vehicle = Convert.ToString(item["VehicleType"]),
                                    Price = obj.Price,
                                    IsRental = Convert.ToBoolean(item["IsRental"]),
                                    BookingNumber = Convert.ToString(item["BookingNumber"]),
                                    CheckInTime = Convert.ToString(item["CheckInTime"]),
                                    VehicleNumber = Convert.ToString(item["VehicleNumber"]),
                                    GSTAmount = Convert.ToDecimal(dtResult.Rows[0]["GSTAmount"]),
                                    TaxableAmount = Convert.ToDecimal(dtResult.Rows[0]["TaxableAmount"]),
                                    //obj.GSTSlabID = Convert.ToInt32(dtResult.Rows[0]["GSTSlabID"]);
                                    Message = "Successfully Data Return"

                                });
                            }
                            else
                            {
                                CheckoutDataList.Add(new CheckOutDataModel()
                                {
                                    Message = "Invalid Data Return "
                                });
                            }
                        }

                    }
                    else
                    {
                        obj.Result = 0;
                        obj.Message = "No Data Found ";
                        CheckoutDataList.Add(obj);
                    }
              

                }
                return CheckoutDataList;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", ex.Message);
                throw;
            }
            finally
            {
                if (objParameter != null)
                    objParameter = null;
            }
        }

        public CheckOutDataModel CheckOutDetails(CheckOutDataModel obj)
        {
            try
            {
                objParameter = new List<MySqlParameter>();
                ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);
                objParameter.Add(new MySqlParameter("@VehicleNumber", obj.VehicleNumber));
                objParameter.Add(new MySqlParameter("@ParkingID", obj.ParkingID));
                objParameter.Add(new MySqlParameter("@BookingNumber", obj.BookingNumber));
                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("PARK_PARKBooking_CheckInData", objParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                                if (Convert.ToBoolean(dtResult.Rows[0]["IsRental"]) == true)
                                {
                                    obj.Price = 0;
                                }
                                else
                                {
                                    obj.Price = Convert.ToInt32(dtResult.Rows[0]["Amount"]);
                                }
                                     obj.ParkingID = Convert.ToInt32(dtResult.Rows[0]["ParkingID"]);
                                    obj.Result = Convert.ToInt32(dtResult.Rows[0]["Result"]);
                                    obj.Vehicle = Convert.ToString(dtResult.Rows[0]["VehicleType"]);
                                    obj.IsRental = Convert.ToBoolean(dtResult.Rows[0]["IsRental"]);
                                    obj.BookingNumber = Convert.ToString(dtResult.Rows[0]["BookingNumber"]);
                                    obj.CheckInTime = Convert.ToString(dtResult.Rows[0]["CheckInTime"]);
                                    obj.VehicleNumber = Convert.ToString(dtResult.Rows[0]["VehicleNumber"]);
                                    obj.GSTAmount = Convert.ToDecimal(dtResult.Rows[0]["GSTAmount"]);
                                    obj.TaxableAmount = Convert.ToDecimal(dtResult.Rows[0]["TaxableAmount"]);
                                    obj.Message = "Successfully Data Return";

                     }
                    else
                    {
                obj.Message = "No Data Found";
                                
                    }
                      

                }
                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", ex.Message);
                throw;
            }
            finally
            {
                if (objParameter != null)
                    objParameter = null;
            }
        }
        public List<CheckOutDataModel> CheckOutDataByVehicle(CheckOutDataModel obj)
        {
            try
            {
                objParameter = new List<MySqlParameter>();
                List<CheckOutDataModel> CheckoutDataList = new List<CheckOutDataModel>();
                ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);
                objParameter.Add(new MySqlParameter("@VehicleNumber", obj.VehicleNumber));
                objParameter.Add(new MySqlParameter("@ParkingID", obj.ParkingID));
                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("PARK_PARKBooking_CheckInDataByVehicle", objParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            CheckoutDataList.Add(new CheckOutDataModel()
                            {
                                ParkingID = Convert.ToInt32(item["ParkingID"]),
                                VehicleNumber = Convert.ToString(item["VehicleNumber"]),
                                Vehicle = Convert.ToString(item["VehicleType"]),
                                Price = Convert.ToInt32(item["Amount"]),
                                CheckInTime = Convert.ToString(item["CheckInTime"]),
                                Result = Convert.ToInt32(item["Result"]),
                                BookingNumber = Convert.ToString(item["BookingNumber"]),
                                Message = "Successfully Return",
                            });
                        }
                    }

                }
                return CheckoutDataList;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message}", ex.Message);
                throw;
            }
            finally
            {
                if (objParameter != null)
                    objParameter = null;
            }
        }
        public CheckOutSaveModel CheckOutSave(CheckOutSaveModel obj)
        {
            ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);
            objParameter = new List<MySqlParameter>
            {
                 new MySqlParameter("@ParkingID", obj.ParkingID),
                 new MySqlParameter("@VehicleNumber", obj.VehicleNumber),
                  new MySqlParameter("@Price", obj.Price),
                  new MySqlParameter("@GSTAmount", obj.GSTAmount),
                   new MySqlParameter("@IsFree", obj.IsFree),
                  new MySqlParameter("@TaxableAmount", obj.TaxableAmount),
                 new MySqlParameter("@Device", obj.Device),
                 new MySqlParameter("@Transactionid", obj.Transactionid),
                 new MySqlParameter("@ParmentMode", obj.ParmentMode),
                  new MySqlParameter("@BookingNumber", obj.BookingNumber),
                 new MySqlParameter("@TransactionData", obj.TransactionData),
                 new MySqlParameter("@CreatedBy",  obj.CreatedBy),
            };

            using (DataTable dtResult = _mySQL.ReturnSqlDataTable("PARK_ParkBooking_CheckOut", objParameter))
            {
                if (dtResult.Rows.Count > 0)
                {
                    obj.Result = Convert.ToInt32(dtResult.Rows[0]["Result"]);
                    if (obj.Result > 0)
                    {
                        obj.Message = "Successfully Check Out";
                    }
                    else
                    {
                        obj.Message = Convert.ToString(dtResult.Rows[0]["ErrorMessage"]);
                    }
                }
                else
                {
                    obj.Message = "Faild To Check Out";
                }


            }
            return obj;
        }
        public CheckOutSaveModel RawDataDave(CheckOutSaveModel obj, string Result)
        {
            ISessionFunCommon objSessionFunCommon = new SessionFunCommon(_httpContextAccessor);
            objParameter = new List<MySqlParameter>
            {
                 new MySqlParameter("@ParkingID", obj.ParkingID),
                 new MySqlParameter("@VehicleNumber", obj.VehicleNumber),
                  new MySqlParameter("@BookingNumber", obj.BookingNumber),
                 new MySqlParameter("@RawData", Result),
            };

            using (DataTable dtResult = _mySQL.ReturnSqlDataTable("PARK_ParkBooking_CheckOutRawData", objParameter))
            {
                if (dtResult.Rows.Count > 0)
                {
                    obj.Result = Convert.ToInt32(dtResult.Rows[0]["Result"]);
                    if (obj.Result > 0)
                    {
                        obj.Message = "Successfully Check Out";
                    }
                    else
                    {
                        obj.Message = Convert.ToString(dtResult.Rows[0]["ErrorMessage"]);
                    }
                }
                else
                {
                    obj.Message = "Faild To Check Out";
                }


            }
            return obj;
        }
    }
}
