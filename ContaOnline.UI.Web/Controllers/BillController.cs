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
        public ActionResult Index()
        {
            loggedUser = _appHelper.GetLoggedUser();

            if (loggedUser == null)
                RedirectToAction("Login", "App");

            IEnumerable<BillListItem> billList = _billRepository.GetByUser(loggedUser.Id);

            return View(billList);
        }

        // GET: Bill/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bill/Create
        [HttpGet]
        public ActionResult Create()
        {
            var billViewModel = new BillViewModel();

            FillBillViewModel(billViewModel);

            return View(billViewModel);
        }

        private void FillBillViewModel(BillViewModel billViewModel)
        {
            billViewModel.CheckingAccountList = _checkingAccountRepository.GetAll(loggedUser.Id);

            billViewModel.BillCategoryList = _billCategoryRepository.GetAll(loggedUser.Id);

            billViewModel.ContactList = _contactRepository.GetAll(loggedUser.Id);
        }

        // POST: Bill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillViewModel billViewModel)
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

        // GET: Bill/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bill/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
