using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASN.Models
{
    public class ProjectModel
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Project { get; set; }
        public string Pic { get; set; }
        public int SubProject { get; set; }
        public int AllocatedBy { get; set; }
        public int TestedBy { get; set; }
        public int LiveBy { get; set; }
        public int TakenCareBy { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TotalHours { get; set; }
        public List<SelectListItem>? lstUser { get; set; }
        public List<SelectListItem>? lstCompany { get; set; }
        public List<SelectListItem>? lstProject { get; set; }
        public List<SelectListItem>? lstSubProject { get; set; }
        public List<SelectListItem>? lstStatus { get; set; }
        public List<SelectListItem>? lstPriority { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

    }
}
