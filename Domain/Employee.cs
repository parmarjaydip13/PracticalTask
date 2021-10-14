using System;

namespace Domain
{
    public class Employee
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int StateId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TotalExperience { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
