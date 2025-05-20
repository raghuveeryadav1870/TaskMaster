using ASN.IRepository;
using ASN.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Data;

namespace ASN.Commons
{
    public class DropdownCommon:IDropdown
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MySQL> _logger;
        private readonly IMySQL _mySQL;


        public DropdownCommon(IConfiguration configuration, ILogger<MySQL> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _mySQL = new MySQL(_configuration, _logger);
        }

        private List<MySqlParameter>? _ddlCommonParameter;
        private List<SelectListItem>? _selectionListItem;

        public List<SelectListItem> CountryList(string? CountryID, bool indexZero = false)
        {
            try
            {
                _ddlCommonParameter = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(CountryID) && !CountryID.Equals("0"))
                {
                    _ddlCommonParameter.Add(new MySqlParameter("@CountryID", MySqlDbType.Int32) { Value = CountryID });
                }

                _selectionListItem = new List<SelectListItem>();

                if (indexZero)
                {
                    _selectionListItem.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0"
                    });
                }

                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("K2d2_CountryMaster_Gets", _ddlCommonParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            _selectionListItem.Add(new SelectListItem()
                            {
                                Text = item["CountryName"].ToString(),
                                Value = item["CountryID"].ToString()
                            });
                        }
                    }
                }

                return _selectionListItem;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                throw;
            }
            finally
            {
                _selectionListItem = null;
                _ddlCommonParameter = null;
            }
        }
        public List<SelectListItem> StateList(string? StateID, bool indexZero = false)
        {
            try
            {
                _ddlCommonParameter = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(StateID) && !StateID.Equals("0"))
                {
                    _ddlCommonParameter.Add(new MySqlParameter("@StateID", MySqlDbType.Int32) { Value = StateID });
                }

                _selectionListItem = new List<SelectListItem>();

                if (indexZero)
                {
                    _selectionListItem.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0"
                    });
                }

                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("K2d2_StateMaster_Gets", _ddlCommonParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            _selectionListItem.Add(new SelectListItem()
                            {
                                Text = item["StateName"].ToString(),
                                Value = item["StateID"].ToString()
                            });
                        }
                    }
                }

                return _selectionListItem;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                throw;
            }
            finally
            {
                _selectionListItem = null;
                _ddlCommonParameter = null;
            }
        }
      
        public List<SelectListItem> GetDays(string? DayID, bool indexZero = false)
        {
            try
            {
                _ddlCommonParameter = new List<MySqlParameter>(); 

                _selectionListItem = new List<SelectListItem>();

                if (indexZero)
                {
                    _selectionListItem.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0"
                    });
                }

                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("PARK_PARKDaysMaster_Gets", _ddlCommonParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            _selectionListItem.Add(new SelectListItem()
                            {
                                Text = item["DayName"].ToString(),
                                Value = item["DayID"].ToString()
                            });
                        }
                    }
                }

                return _selectionListItem;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                throw;
            }
            finally
            {
                _selectionListItem = null;
                _ddlCommonParameter = null;
            }
        }
        public List<SelectListItem> GetCompanyList(string? CompanyID, bool indexZero = false)
        {
            try
            {
                _ddlCommonParameter = new List<MySqlParameter>(); // No parameters needed for this procedure

                _selectionListItem = new List<SelectListItem>();

                if (indexZero)
                {
                    _selectionListItem.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0"
                    });
                }

                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_UserMasterGets", _ddlCommonParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            _selectionListItem.Add(new SelectListItem()
                            {
                                Text = item["UserName"].ToString(),
                                Value = item["UserID"].ToString()
                            });
                        }
                    }
                }

                return _selectionListItem;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                throw;
            }
            finally
            {
                _selectionListItem = null;
                _ddlCommonParameter = null;
            }
        }
        
        public List<SelectListItem> GetUserList(string? CustomerID, bool indexZero = false)
        {
            try
            {
                _ddlCommonParameter = new List<MySqlParameter>(); // No parameters needed for this procedure

                _selectionListItem = new List<SelectListItem>();

                if (indexZero)
                {
                    _selectionListItem.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = "0"
                    });
                }

                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_UserMasterGets", _ddlCommonParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            _selectionListItem.Add(new SelectListItem()
                            {
                                Text = item["UserName"].ToString(),
                                Value = item["UserID"].ToString()
                            });
                        }
                    }
                }

                return _selectionListItem;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                throw;
            }
            finally
            {
                _selectionListItem = null;
                _ddlCommonParameter = null;
            }
        }
        public List<SelectListItem> GetProfileList( bool indexZero = false)
        {
            try
            {
                _ddlCommonParameter = new List<MySqlParameter>(); // No parameters needed for this procedure

                _selectionListItem = new List<SelectListItem>();

                if (indexZero)
                {
                    _selectionListItem.Add(new SelectListItem()
                    {
                        Text = "Select",
                        Value = " "
                    });
                }
                _selectionListItem.Add(new SelectListItem()
                {
                    Text = "Admin",
                    Value = "A"
                });
                _selectionListItem.Add(new SelectListItem()
                {
                    Text = "HR",
                    Value = "H"
                });
                _selectionListItem.Add(new SelectListItem()
                {
                    Text = "Manager",
                    Value = "M"
                });
                //using (DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_ProfileMasterGets", _ddlCommonParameter))
                //{
                //    if (dtResult.Rows.Count > 0)
                //    {
                //        foreach (DataRow item in dtResult.Rows)
                //        {
                //            _selectionListItem.Add(new SelectListItem()
                //            {
                //                Text = item["Profile"].ToString(),
                //                Value = item["ProfileID"].ToString()
                //            });
                //        }
                //    }
                //}

                return _selectionListItem;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("{Message}", ex.StackTrace);
                throw;
            }
            finally
            {
                _selectionListItem = null;
                _ddlCommonParameter = null;
            }
        }

        public List<SelectListItem> GetFullDayList(string? FullDay, bool indexZero = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (indexZero)
            {
                items.Add(new SelectListItem()
                {
                    Text = "--FullDay--",
                    Value = "0"
                });
            }
            items.Add(new SelectListItem()
            {
                Text = "12 Hour",
                Value = "1"
            });
            items.Add(new SelectListItem()
            {
                Text = "24 Hour",
                Value = "2"
            });
            return items;
        }
        public List<SelectListItem> GetStatusList(string? Status, bool indexZero = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (indexZero)
            {
                items.Add(new SelectListItem()
                {
                    Text = "--Select Status--",
                    Value = ""
                });
            }
            items.Add(new SelectListItem()
            {
                Text = "Started",
                Value = "Started"
            });
            items.Add(new SelectListItem()
            {
                Text = "Working",
                Value = "Working"
            });
            items.Add(new SelectListItem()
            {
                Text = "Completed",
                Value = "Completed"
            });
            return items;
        }
        public List<SelectListItem> GetPriortyList(string? Priority, bool indexZero = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (indexZero)
            {
                items.Add(new SelectListItem()
                {
                    Text = "--Select Priority--",
                    Value = "0"
                });
            }
            items.Add(new SelectListItem()
            {
                Text = "High",
                Value = "1"
            });
            items.Add(new SelectListItem()
            {
                Text = "Medium",
                Value = "Medium"
            });
            items.Add(new SelectListItem()
            {
                Text = "Low",
                Value = "Low"
            });
            return items;
        }
        public List<SelectListItem> GetProjectList(string? ProjectId, bool indexZero = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (indexZero)
            {
                items.Add(new SelectListItem()
                {
                    Text = "--Select Project--",
                    Value = "0"
                });
            }
            using (DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_ProjectList", _ddlCommonParameter))
            {
                if (dtResult.Rows.Count > 0)
                {
                    foreach (DataRow item in dtResult.Rows)
                    {
                        items.Add(new SelectListItem()
                        {
                            Text = item["ProjectName"].ToString(),
                            Value = item["ID"].ToString()
                        });
                    }
                }
            }
            return items;
        }
        public List<SelectListItem> GetSubprojectList(string? ProjectID, bool indexZero = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (indexZero)
            {
                items.Add(new SelectListItem()
                {
                    Text = "--Select Sub Project--",
                    Value = "0"
                });
            }
            try
            {
                _ddlCommonParameter = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(ProjectID) && !ProjectID.Equals("0"))
                {
                    _ddlCommonParameter.Add(new MySqlParameter("@P_ID", MySqlDbType.Int32) { Value = ProjectID });
                }

                using (DataTable dtResult = _mySQL.ReturnSqlDataTable("k2d2_SubProjectList", _ddlCommonParameter))
                {
                    if (dtResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtResult.Rows)
                        {
                            items.Add(new SelectListItem()
                            {
                                Text = item["SubProjectName"].ToString(),
                                Value = item["ID"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {

            }
            return items;
        }


    }
}
