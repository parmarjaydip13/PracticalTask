using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PracticalTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PracticalTask.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IEmployeeRepository employeeRepository, ICityRepository cityRepository, IMapper mapper)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            try
            {
               //get current user session
               //curently only admin can login role is not defined
                var session = HttpContext.Session.GetString("Email");

                if (session == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                HomeViewModel viewModel = new HomeViewModel();
                await BindDropdown(viewModel);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured in Index");
                return RedirectToAction("Error");
            }
            
        }
        #endregion

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API
        [HttpGet]
        //API method for binding Emplyoee datatable
        public async Task<IActionResult> GetEmployeedata()
        {
            try
            {
                var data = await _employeeRepository.Get();
                var responseData = _mapper.Map<List<EmployeeViewModel>>(data);
                return Ok(new { responseCode = "Ok", responseData = responseData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error occured in GetEmployeedata");
                return new JsonResult("Error") { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
        #endregion

        #region Private Method
        private async Task BindDropdown(HomeViewModel viewModel)
        {
            var cities = await _cityRepository.Get();
            viewModel.CityList = cities.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
        }
        #endregion

    }
}
