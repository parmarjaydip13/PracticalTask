using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace PracticalTask
{
    public class HomeViewModel
    {
        public int? CityId { get; set; }
        public string City { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
        public decimal? Salary { get; set; }
        public string TotalExperience { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
