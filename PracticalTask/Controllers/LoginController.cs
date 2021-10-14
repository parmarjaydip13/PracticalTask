using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticalTask.ViewModels;
using Service.Interface;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PracticalTask.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHelperService _helperService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IUserRepository userRepository, IHelperService helperService, ILogger<LoginController> logger)
        {
            _userRepository = userRepository;
            _helperService = helperService;
            _logger = logger;
        }

        #region Index View

        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            LoginViewModel vm = new LoginViewModel();
            return View(vm);
        }

        //Post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel viewModel)
        {
            try
            {
                //return view if  posted data model is not valid
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                //get user by email
                var user = await _userRepository.GetByEmail(viewModel.Email);

                if (user == null)
                {
                    ViewBag.Error = "Email address or password is incorrect.";
                    return View(viewModel);
                }

                //check if hash match or not salt is handel by function 
                var isValid = _helperService.VerifiedHash(user.PasswordHash, viewModel.Password);

                if (!isValid)
                {
                    ViewBag.Error = "Email address or password is incorrect.";
                    return View(viewModel);
                }

                HttpContext.Session.SetString("Email", user.EmailAddress);
                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error occured in Post Index", ex);
                return RedirectToAction("Error");
            }
        }

        #endregion

        #region Logout 
        [HttpPost]
        public IActionResult Logout()
        {
            //clear session and redirect back to login page
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
        #endregion
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
