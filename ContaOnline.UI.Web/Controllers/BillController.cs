using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.UI.Web.Code;

namespace OnlineBill.UI.Web.Controllers
{
    public class BillController : Controller
    {

        private readonly IBillRepository _billRepository;
        private readonly ICheckingAccountRepository _checkingAccountRepository;
        private readonly IBillCategoryRepository _billCategoryRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IAppHelper _appHelper;
        private static User? loggedUser;

        public BillController(
            IBillRepository billRepository,
            ICheckingAccountRepository checkingAccountRepository,
            IBillCategoryRepository billCategoryRepository,
            IContactRepository contactRepository,
            IAppHelper appHelper)
        {
            _billRepository = billRepository;
            _checkingAccountRepository = checkingAccountRepository;
            _billCategoryRepository = billCategoryRepository;
            _contactRepository = contactRepository;
            _appHelper = appHelper;
        }



        // GET: Bill
        public IActionResult Index()
        {
            loggedUser = _appHelper.GetLoggedUser();

            if (loggedUser == null)
                RedirectToAction("Login", "App");

            var model = new BillListViewModel();

            model.BillList = _billRepository.GetByUser(loggedUser.Id);

            FillBillListViewModel(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BillListViewModel model)
        {
            if (loggedUser == null)
                RedirectToAction("Login", "App");

            model.Filter.UserId = loggedUser.Id;

            model.BillList = _billRepository.GetByFilter(model.Filter).ToList();

            FillBillListViewModel(model);

            return View(model);
        }

        // GET: Bill/Details/
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Bill/Create
        [HttpGet]
        public IActionResult Create()
        {
            var billViewModel = new BillViewModel();

            FillBillViewModel(billViewModel);

            return View(billViewModel);
        }

        // POST: Bill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BillViewModel billViewModel)
        {
            try
            {
                billViewModel.BillInstance.UserId = loggedUser.Id;
                billViewModel.BillInstance.Id = Guid.NewGuid().ToString();

                _billRepository.Add(billViewModel.BillInstance);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Bill/Edit/
        public IActionResult Edit(string id)
        {
            var billViewModel = new BillViewModel();

            billViewModel.BillInstance = _billRepository.GetById(id);

            FillBillViewModel(billViewModel);

            return View(billViewModel);
        }

        // POST: Bill/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BillViewModel billViewModel)
        {
            try
            {
                _billRepository.Update(billViewModel.BillInstance);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(billViewModel);
            }
        }

        // GET: Bill/Delete/
        public IActionResult Delete(string id)
        {
            var billExhibitViewModel = _billRepository.GetBillExhibitById(id);

            billExhibitViewModel.FormatOptionalValues();

            return View(billExhibitViewModel);
        }

        // POST: Bill/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                _billRepository.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void FillBillViewModel(BillViewModel billViewModel)
        {
            billViewModel.CheckingAccountList = _checkingAccountRepository.GetAll(loggedUser.Id);

            billViewModel.BillCategoryList = _billCategoryRepository.GetAll(loggedUser.Id);

            billViewModel.ContactList = _contactRepository.GetAll(loggedUser.Id);
        }

        private void FillBillListViewModel(BillListViewModel model)
        {
            model.CheckingAccountList = _checkingAccountRepository.GetAll(loggedUser.Id);

            model.BillCategoryList = _billCategoryRepository.GetAll(loggedUser.Id);
        }
    }
}
