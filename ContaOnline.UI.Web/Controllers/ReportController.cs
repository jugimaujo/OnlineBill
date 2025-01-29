using System;
using OnlineBill.UI.Web.Code;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;

namespace OnlineBill.UI.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IAppHelper _appHelper;
        private readonly IBillRepository _billRepository;
        private readonly string loggedUser;

        public ReportController(IAppHelper appHelper, IBillRepository billRepository)
        {
            _appHelper = appHelper;
            _billRepository = billRepository;
            loggedUser = _appHelper.GetLoggedUser();
        }

        public IActionResult Index()
        {
            var billList = _billRepository.GetAll(loggedUser);

            List<decimal> earnPerMonth = new();
            List<decimal> lostPerMonth = new();
            //List<decimal> totalPerMonth = new();

            BillGraphViewModel billGraphViewModel = new BillGraphViewModel
            {
                BillGroup = []
            };

            for (int month = 1; month <= 12; month++)
            {
                var billsPerMonth = billList.Where(bill => bill.DueDate.Month == month).ToList();

                if (billsPerMonth.Count == 0)
                {
                    earnPerMonth.Add(0);
                    lostPerMonth.Add(0);
                    continue;
                }

                var billGroup = new BillGraphGroupPerMonth
                {
                    BillList = billsPerMonth,
                    Month = month,
                    EarnValue = billsPerMonth.Where(bill => bill.Type == PayReceive.Receive).Sum(bill => bill.Value),
                    LostValue = billsPerMonth.Where(bill => bill.Type == PayReceive.Pay).Sum(bill => -bill.Value)
                };

                billGroup.TotalValue = billGroup.EarnValue - billGroup.LostValue;

                earnPerMonth.Add(billGroup.EarnValue);
                lostPerMonth.Add(billGroup.LostValue);
                //totalPerMonth.Add(billGroup.TotalValue);

                ViewBag.Earns = earnPerMonth;
                ViewBag.Losts = lostPerMonth;
                //ViewBag.Totals = totalPerMonth;

                billGraphViewModel.BillGroup.Add(billGroup);
            }

            return View(billGraphViewModel);
        }
    }
}
