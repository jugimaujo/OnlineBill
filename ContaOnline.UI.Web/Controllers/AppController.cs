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

namespace OnlineBill.UI.Web.Controllers
{
    public class AppController : Controller
    {   
        private readonly IUserRepository _userRepository;
        private readonly IAppHelper _appHelper;

        public AppController(IUserRepository userRepository, IAppHelper appHelper)
        {
            _userRepository = userRepository;
            _appHelper = appHelper;
        }

        /// <summary>
        /// Initial Screen
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var email = loginViewModel.Email;
            var password = loginViewModel.Password;

            User user = _userRepository.GetByEmailPassword(email, password);

            if (user == null)
            {
                loginViewModel.Message = "Usuário ou senha inexistente";
            }
            else
            {
                //_appHelper.RegisterUser(user);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("userId", user.Id)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal).Wait();

                return RedirectToAction("Home", "App");
            }

            return View(loginViewModel);

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
    }
}
