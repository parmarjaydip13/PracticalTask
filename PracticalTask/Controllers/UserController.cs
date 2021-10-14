using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PracticalTask.Controllers
{
    public class UserController : Controller
    {
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IStateRepository stateRepository, ICityRepository cityRepository, IEmployeeRepository employeeRepository, IMapper mapper, ILogger<UserController> logger)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        #region Registration

        public async Task<IActionResult> Registration()
        {
            //check if registratio success or not and display alert for that
            var tempData = TempData["Success"];
            if (tempData != null)
            {
                ViewBag.Success = Convert.ToString(TempData["Success"]);
            }

            RegistrationViewModel viewModel = new RegistrationViewModel();
            await BindDropdown(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            try
            {
                await BindDropdown(viewModel);
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                var employee = _mapper.Map<Employee>(viewModel);
                var insertedId = _employeeRepository.Add(employee);

                TempData["Success"] = "Registration successful.";
                return RedirectToAction("Registration", "User");
            }
            catch (Exception ex)
            {
                //Log error 
                _logger.LogError(ex, "Error occured in Registration");
                ViewBag.Error = "An error occurred while processing Request.Please try again.";
                return View(viewModel);
            }
        }

        #endregion

        #region Private Method
        private async Task BindDropdown(RegistrationViewModel viewModel)
        {

            viewModel.TotalExperienceList = new List<SelectListItem>
            {
                new SelectListItem(){ Text = "6 Months" , Value ="6 Months"},
                new SelectListItem(){ Text = "1 Years" , Value ="1 Years"},
                new SelectListItem(){ Text = "2 Years" , Value ="2 Years"},
                new SelectListItem(){ Text = "3 Years" , Value ="3 Years"},
                new SelectListItem(){ Text = "4 Years" , Value ="4 Years"},
                new SelectListItem(){ Text = "More then 4 Years" , Value ="More then 4 Years"},
            };


            var states = await _stateRepository.Get();
            viewModel.StateList = states.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            viewModel.CityList = new List<SelectListItem>();

            if (viewModel.StateId.HasValue)
            {
                var cities = await _cityRepository.GetByState(viewModel.StateId.Value);
                viewModel.CityList = cities.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            }
        }

        #endregion

        #region API
        //cascade city dropdown binding API called
        public async Task<IActionResult> GetCity(int state)
        {
            try
            {
                var cities = await _cityRepository.GetByState(state);
                var cityList = cities.Select(s => new { Name = s.Name, Value = s.Id });
                return new JsonResult(cityList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in GetCity API.");
                return new JsonResult("Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
        #endregion
    }
}
