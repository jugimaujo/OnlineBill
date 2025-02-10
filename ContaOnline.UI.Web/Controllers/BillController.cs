using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.UI.Web.Code;

namespace OnlineBill.UI.Web.Controllers
{
    [Authorize]
    public class BillController : Controller
    {

        private readonly IBillRepository _billRepository;
        private readonly ICheckingAccountRepository _checkingAccountRepository;
        private readonly IBillCategoryRepository _billCategoryRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IAppHelper _appHelper;
        private static string? loggedUserId;

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
            loggedUserId = _appHelper.GetLoggedUser();

            var model = new BillListViewModel();

            model.BillList = _billRepository.GetByUser(loggedUserId);

            model.TotalNotPaid = model.BillList.Sum(bill => (bill.PaymentDate == null && bill.PaidValue == null && (bill.DueDate - DateTime.Now).Days >= 0) ? bill.Value : 0);

            model.TotalExpiredNotPaid = model.BillList.Sum(bill => (bill.PaymentDate == null && bill.PaidValue == null && (bill.DueDate - DateTime.Now).Days < 0) ? bill.Value : 0);

            model.TotalPaid = model.BillList.Sum(bill => (bill.PaymentDate != null || bill.PaidValue != null) ? bill.Value : 0);

            FillBillListViewModel(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(BillListViewModel model)
        {
            model.Filter.UserId = loggedUserId;

            model.BillList = _billRepository.GetByFilter(model.Filter).ToList();

            model.TotalNotPaid = model.BillList.Sum(bill => (bill.PaymentDate == null && bill.PaidValue == null) ? bill.Value : 0);

            model.TotalExpiredNotPaid = model.BillList.Sum(bill => (bill.PaymentDate == null && bill.PaidValue == null && (bill.DueDate - DateTime.Now).Days < 0) ? bill.Value : 0);

            model.TotalPaid = model.BillList.Sum(bill => (bill.PaymentDate != null || bill.PaidValue != null) ? bill.Value : 0);

            FillBillListViewModel(model);

            return View(model);
        }

        // GET: Bill/Details/
        public IActionResult Details(string id)
        {
            var bill = _billRepository.GetDetailsById(id);

            return View(bill);
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
                billViewModel.BillInstance.UserId = loggedUserId;
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
            billViewModel.CheckingAccountList = _checkingAccountRepository.GetAll(loggedUserId);

            billViewModel.BillCategoryList = _billCategoryRepository.GetAll(loggedUserId);

            billViewModel.ContactList = _contactRepository.GetAll(loggedUserId);
        }

        private void FillBillListViewModel(BillListViewModel model)
        {
            model.CheckingAccountList = _checkingAccountRepository.GetAll(loggedUserId);

            model.BillCategoryList = _billCategoryRepository.GetAll(loggedUserId);
        }
    }
}
