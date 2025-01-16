using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.UI.Web.Code;
using OnlineBill.UI.Web.Models;

namespace OnlineBill.UI.Web.Controllers
{
    public class CheckingAccountController : Controller
    {
        private readonly ICheckingAccountRepository _repository;
        private readonly IAppHelper _appHelper;
        private User? loggedUser;

        public CheckingAccountController(ICheckingAccountRepository repository, IAppHelper appHelper)
        {
            _repository = repository;
            _appHelper = appHelper;
        }

        public IActionResult Index()
        {
            loggedUser = _appHelper.GetLoggedUser();

            if (!_appHelper.IsUserLoggedIn())
            {
                return RedirectToAction("Login", "App");
            }

            var checkingAccountList = _repository.GetAll(loggedUser.Id);

            return View(checkingAccountList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CheckingAccount checkingAccount)
        {

            if (string.IsNullOrEmpty(checkingAccount.Description))
            {
                return View(checkingAccount);
            }

            loggedUser = _appHelper.GetLoggedUser();

            var newCheckingAccount = new CheckingAccount
            {
                Id = Guid.NewGuid().ToString(),
                UserId = loggedUser.Id,
                Description = checkingAccount.Description,
            };

            _repository.Add(newCheckingAccount);

            return Redirect("Index");
        }

        public IActionResult Edit(string id)
        {
            var checkingAccount = _repository.GetById(id);

            return View(checkingAccount);
        }

        [HttpPost]
        public IActionResult Edit(CheckingAccount checkingAccount)
        {
            if (string.IsNullOrEmpty(checkingAccount.Description))
            {
                return View(checkingAccount);
            }

            _repository.Update(checkingAccount);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            var checkingAccount = _repository.GetById(id);

            return View(checkingAccount);
        }

        [HttpPost]
        public IActionResult Delete(CheckingAccount checkingAccount)
        {
            _repository.Delete(checkingAccount.Id);

            return RedirectToAction("Index");
        }
    }
}
