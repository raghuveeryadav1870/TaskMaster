using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASN.IRepository
{
    public interface IDropdown
    {
        //List<SelectListItem> CountryList(string? CountryId, bool indexZero = false);
        // List<SelectListItem> StateList(string? CountryId, bool indexZero = false);
        List<SelectListItem> CountryList(string? CountryID, bool indexZero = false);
        List<SelectListItem> StateList(string? StateID, bool indexZero = false);
        List<SelectListItem> GetCompanyList(string? CompanyID, bool indexZero = false);
        List<SelectListItem> GetUserList(string? CustomerID, bool indexZero = false);
        List<SelectListItem> GetFullDayList(string? FullDay, bool indexZero = false);
        List<SelectListItem> GetDays(string? DayID, bool indexZero = false);
        List<SelectListItem> GetProfileList(bool indexZero = false);
        List<SelectListItem> GetStatusList(string? Status, bool indexZero = false);
        List<SelectListItem> GetPriortyList(string? priority, bool indexZero = false);
        List<SelectListItem> GetProjectList(string? ProjectId, bool indexZero = false);
        List<SelectListItem> GetSubprojectList(string? SubProjectID, bool indexZero = false);
    }
}
