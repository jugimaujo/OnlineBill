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
        private readonly IAppHelper _appHelper;

        public BillController(IBillRepository billRepository, IAppHelper appHelper)
        {
            _billRepository = billRepository;
            _appHelper = appHelper;
        }



        // GET: Bill
        public ActionResult Index()
        {
            var loggedUser = _appHelper.GetLoggedUser();

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
