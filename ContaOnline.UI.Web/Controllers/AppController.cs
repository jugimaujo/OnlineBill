using System;
using Microsoft.AspNetCore.Mvc;
using OnlineBill.UI.Web.Models;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.UI.Web.Code;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using NToastNotify;

namespace OnlineBill.UI.Web.Controllers
{
    public class AppController : Controller
    {   
        private readonly IUserRepository _userRepository;
        private readonly IToastNotification _toastrService;

        public AppController(IUserRepository userRepository, IToastNotification toastrService)
        {
            _userRepository = userRepository;
            _toastrService = toastrService;
        }

        /// <summary>
        /// Initial Screen
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            var logins = LoadLoginViewModelList();

            return View(logins);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var email = loginViewModel.Email;
            var password = loginViewModel.Password;

            User user = _userRepository.GetByEmailPassword(email, password);

            if (user == null)
            {
                var logins = LoadLoginViewModelList();

                _toastrService.AddErrorToastMessage("Email ou senha foram digitados incorretamente.",
                    new ToastrOptions { Title = "Erro ao logar" });

                return View(logins);
            }

            if (loginViewModel.NotRememberMe != null && loginViewModel.NotRememberMe == true)
                _userRepository.UpdateRememberMe(user.Id, false);

            else if (loginViewModel.RememberMe == true && user.RememberMe != true)
                _userRepository.UpdateRememberMe(user.Id, true);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("userId", user.Id)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

            _toastrService.AddSuccessToastMessage("Você fez login com sucesso",
                new ToastrOptions { Title = "Login" });

            return RedirectToAction("Index", "App");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return View();
        }
        
        /// <summary>
        /// User Registration
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = register.Name,
                    Email = register.Email,
                    Password = register.Password
                };

                _userRepository.Add(user);

                if(User.Identity.IsAuthenticated)
                {
                    HttpContext.SignOutAsync();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("userId", user.Id)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                return Redirect("/");
            }
            
            return View(register);
        }

        /// <summary>
        /// About this Application
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            return View();
        }

        private IEnumerable<LoginViewModel> LoadLoginViewModelList()
        {
            IEnumerable<User> usersList = _userRepository.GetAll();

            if (usersList.Any())
            {
                var rememberedUsers = usersList.Where(user => user.RememberMe == true);

                var logins = new List<LoginViewModel>();

                foreach (var user in rememberedUsers)
                {
                    logins.Add(new LoginViewModel
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        RememberMe = user.RememberMe ?? false
                    });
                }

                return logins;
            }

            return [];
        }
    }
}
