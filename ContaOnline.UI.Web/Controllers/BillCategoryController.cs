using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.UI.Web.Code;

namespace OnlineBill.UI.Web.Controllers
{
    [Authorize]
    public class BillCategoryController : Controller
    {
        private readonly IBillCategoryRepository _billCategoryRepository;
        private readonly IAppHelper _appHelper;
        private readonly IToastNotification _toastrService;
        private string? loggedUserId;

        public BillCategoryController(IBillCategoryRepository billCategoryRepository, IAppHelper appHelper, IToastNotification toastrService)
        {
            _billCategoryRepository = billCategoryRepository;
            _appHelper = appHelper;
            _toastrService = toastrService;
        }

        // GET: BillCategoryController
        public IActionResult Index()
        {
            loggedUserId = _appHelper.GetLoggedUser();

            var billCategoryList = _billCategoryRepository.GetAll(loggedUserId);

            return View(billCategoryList);
        }

        // GET: BillCategoryController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillCategoryController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BillCategory billCategory)
        {
            try
            {
                loggedUserId = _appHelper.GetLoggedUser();

                billCategory.Id = Guid.NewGuid().ToString();
                billCategory.UserId = loggedUserId;

                _billCategoryRepository.Add(billCategory);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(billCategory);
            }
        }

        // GET: BillCategoryController/Edit
        public IActionResult Edit(string id)
        {
            var billCategory = _billCategoryRepository.GetById(id);

            return View(billCategory);
        }

        // POST: BillCategoryController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BillCategory billCategory)
        {
            try
            {
                _billCategoryRepository.Update(billCategory);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(billCategory);
            }
        }

        // GET: BillCategoryController/Delete
        public IActionResult Delete(string id)
        {
            var billCategory = _billCategoryRepository.GetById(id);

            if (!_appHelper.IsDeletable(billCategory))
            {
                _toastrService.AddErrorToastMessage("Não é possível excluir essa Categoria, pois ela está sendo usada em suas contas.",
                new ToastrOptions { Title = "Erro ao excluir" });

                return RedirectToAction(nameof(Index));
            }

            return View(billCategory);
        }

        // POST: BillCategoryController/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(BillCategory billCategory)
        {
            try
            {
                _billCategoryRepository.Delete(billCategory.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(billCategory);
            }
        }
    }
}
