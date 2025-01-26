using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NToastNotify;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.UI.Web.Code;
using OnlineBill.UI.Web.Models;

namespace OnlineBill.UI.Web.Controllers
{
    [Authorize]
    public class CheckingAccountController : Controller
    {
        private readonly ICheckingAccountRepository _repository;
        private readonly IAppHelper _appHelper;
        private readonly IToastNotification _toastr;
        private string? loggedUserId;

        public CheckingAccountController(ICheckingAccountRepository repository, IAppHelper appHelper, IToastNotification toastr)
        {
            _repository = repository;
            _appHelper = appHelper;
            _toastr = toastr;
        }

        public IActionResult Index()
        {
            loggedUserId = _appHelper.GetLoggedUser();

            if (!_appHelper.IsUserLoggedIn())
            {
                return RedirectToAction("Login", "App");
            }

            var checkingAccountList = _repository.GetAll(loggedUserId);

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

            loggedUserId = _appHelper.GetLoggedUser();

            var newCheckingAccount = new CheckingAccount
            {
                Id = Guid.NewGuid().ToString(),
                UserId = loggedUserId,
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

            if (_appHelper.IsDeletable(checkingAccount))
            {
                return View(checkingAccount);
            }

            _toastr.AddErrorToastMessage("Não é possível excluir essa Conta Corrente, pois ela está sendo usada em uma conta existente.",
                new ToastrOptions { Title = "Erro ao excluir"});

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(CheckingAccount checkingAccount)
        {
            _repository.Delete(checkingAccount.Id);

            return RedirectToAction("Index");
        }
    }
}
