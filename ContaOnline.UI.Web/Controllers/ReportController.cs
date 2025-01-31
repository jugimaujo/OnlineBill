using System;
using OnlineBill.UI.Web.Code;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Microsoft.VisualBasic;

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
            var billList = _billRepository.GetAllInDetail(loggedUser);

            List<DateTime> months = new();

            List<decimal> dueEarnPerMonth = new();
            List<decimal> dueLostPerMonth = new();
            List<decimal> dueTotalPerMonth = new();

            List<decimal?> paidEarnPerMonth = new();
            List<decimal?> paidLostPerMonth = new();
            List<decimal?> paidTotalPerMonth = new();

            BillGraphViewModel billGraphViewModel = new BillGraphViewModel
            {
                BillGroup = []
            };

            int minYear = billList.Select(bill => bill.DueDate.Year).Min(); 

            int yearNumbers = GetYearsQuantity(billList);

            for (int y = 1; y <= yearNumbers; y++)
            {
                int year = minYear + (y - 1);

                for (int month = 1; month <= 12; month++)
                {
                    var billsPerMonth = billList.Where(bill => (bill.DueDate.Month == month &&
                                                               bill.DueDate.Year == year) ||
                                                               (bill.PaymentDate?.Month == month &&
                                                               bill.PaymentDate?.Year == year)).ToList();

                    if (billsPerMonth.Count == 0)
                    {
                        dueEarnPerMonth.Add(0);
                        dueLostPerMonth.Add(0);
                        dueTotalPerMonth.Add(0);

                        paidEarnPerMonth.Add(0);
                        paidLostPerMonth.Add(0);
                        paidTotalPerMonth.Add(0);
                        continue;
                    }

                    var billGroup = new BillGraphGroupPerMonth
                    {
                        BillList = billsPerMonth,
                        Month = month,
                        
                        DueEarnValue = billsPerMonth.Where(bill => bill.Type == PayReceive.Receive).Sum(bill => bill.Value),
                        DueLostValue = billsPerMonth.Where(bill => bill.Type == PayReceive.Pay).Sum(bill => -bill.Value),

                        PaidEarnValue = billsPerMonth.Where(bill => bill.Type == PayReceive.Receive).Sum(bill => bill.PaidValue),
                        PaidLostValue = billsPerMonth.Where(bill => bill.Type == PayReceive.Pay).Sum(bill => -bill.PaidValue),
                    };

                    billGroup.DueTotalValue = billGroup.DueEarnValue + billGroup.DueLostValue;
                    billGroup.PaidTotalValue = billGroup.PaidEarnValue + billGroup.PaidLostValue;

                    billGraphViewModel.BillGroup.Add(billGroup);

                    dueEarnPerMonth.Add(billGroup.DueEarnValue);
                    dueLostPerMonth.Add(billGroup.DueLostValue);
                    dueTotalPerMonth.Add(billGroup.DueTotalValue);

                    paidEarnPerMonth.Add(billGroup.PaidEarnValue);
                    paidLostPerMonth.Add(billGroup.PaidLostValue);
                    paidTotalPerMonth.Add(billGroup.PaidTotalValue);
                }
            }
            
            ViewBag.Months = GenerateMonths(billList);

            ViewBag.DueEarns = dueEarnPerMonth;
            ViewBag.DueLosts = dueLostPerMonth;
            ViewBag.DueTotals = dueTotalPerMonth;

            ViewBag.PaidEarns = paidEarnPerMonth;
            ViewBag.PaidLosts = paidLostPerMonth;
            ViewBag.PaidTotals = paidTotalPerMonth;

            return View(billGraphViewModel);
        }

        private IEnumerable<string> GenerateMonths(IEnumerable<Bill> billList)
        {
            List<string> months = new();

            var minYear = billList.Select(bill => bill.DueDate.Year).Min();

            int totalYears = GetYearsQuantity(billList);

            string pattern = totalYears == 1 ? "MMMM" : "MMM/yy";

            for (int y = 1; y <= totalYears; y++)
            {
                int year = minYear + (y - 1);

                for (int month = 1; month <= 12; month++)
                {
                    var date = new DateTime(year, month, 1);

                    months.Add(date.ToString(pattern));
                }
            }

            return months;
        }

        private int GetYearsQuantity(IEnumerable<Bill> billList)
        {
            var maxYear = billList.Select(bill => bill.DueDate.Year).Max();
            var minYear = billList.Select(bill => bill.DueDate.Year).Min();

            return (maxYear - minYear) + 1;
        }
    }
}
