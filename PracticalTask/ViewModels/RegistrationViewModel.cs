using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PracticalTask
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please enter First name.")]
        [StringLength(20, ErrorMessage = "First name can not exceed {1} characters. ")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Please enter Last name.")]
        [StringLength(20, ErrorMessage = "Last name can not exceed {1} characters. ")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Please select City.")]
        public int? CityId { get; set; }
        public string City { get; set; }

        public string State { get; set; }

        [Required(ErrorMessage = "Please select State.")]
        public int? StateId { get; set; }


        [Required(ErrorMessage = "Please select DOB.")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select Total experience.")]
        public string TotalExperience { get; set; }


        [Range(10000.0, Double.MaxValue, ErrorMessage = "{0} must be greater than {1}.")]
        [Required(ErrorMessage = "Please enter Salary.")]
        public decimal? Salary { get; set; }

        public IEnumerable<SelectListItem> StateList { get; set; }

        public IEnumerable<SelectListItem> CityList { get; set; }

        public IEnumerable<SelectListItem> TotalExperienceList { get; set; }


    }
}
