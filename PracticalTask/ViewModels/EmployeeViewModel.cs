using System;

namespace PracticalTask
{
    public class EmployeeViewModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public int? CityId { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int? StateId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string TotalExperience { get; set; }

        public decimal? Salary { get; set; }
    }
}
