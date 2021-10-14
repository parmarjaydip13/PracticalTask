using AutoMapper;
using Domain;
using System;
using System.Globalization;

namespace PracticalTask.Profiles
{
    public class EmployeeMapProfile  :Profile
    {
        public EmployeeMapProfile()
        {
            CreateMap<RegistrationViewModel, Employee>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateTime.ParseExact(src.DateOfBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture)))
                .ReverseMap()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("dd-MM-yyyy")));

            CreateMap<EmployeeViewModel, Employee>()
              .ReverseMap();
        }
    }
}
