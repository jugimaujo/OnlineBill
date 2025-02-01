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
        private readonly IBillCategoryRepository _billCategoryRepository;
        private readonly string loggedUser;

        public ReportController(IAppHelper appHelper, IBillRepository billRepository, IBillCategoryRepository billCategoryRepository)
        {
            _appHelper = appHelper;
            _billRepository = billRepository;
            _billCategoryRepository = billCategoryRepository;
            loggedUser = _appHelper.GetLoggedUser();
        }

        public IActionResult Index()
        {
            var billList = _billRepository.GetAllInDetail(loggedUser);

            BillGraphViewModel billGraphViewModel = new BillGraphViewModel
            {
                BillGroup = [],
                CategoryList = _billCategoryRepository.GetAll(loggedUser)
            };

            LoadBarGraph(billList, billGraphViewModel);

            return View(billGraphViewModel);
        }

        [HttpPost]
        public IActionResult Index(BillGraphViewModel model)
        {
            model.Filter.UserId = loggedUser;

            var billList = _billRepository.GetByGraphFilter(model.Filter);

            model = new BillGraphViewModel
            {
                BillGroup = [],
                CategoryList = _billCategoryRepository.GetAll(loggedUser)
            };

            LoadBarGraph(billList, model);

            return View(model);
        }


        private void LoadBarGraph(IEnumerable<BillGraphItem> billList, BillGraphViewModel billGraphViewModel)
        {
            List<DateTime> months = new();

            List<decimal> dueEarnPerMonth = new();
            List<decimal> dueLostPerMonth = new();
            List<decimal> dueTotalPerMonth = new();

            List<decimal?> paidEarnPerMonth = new();
            List<decimal?> paidLostPerMonth = new();
            List<decimal?> paidTotalPerMonth = new();

            int minYear = billList.Select(bill => bill.DueDate.Year).Min();

            int yearNumbers = GetYearsQuantity(billList);

            for (int y = 1; y <= yearNumbers; y++)
            {
                int year = minYear + (y - 1);

                for (int month = 1; month <= 12; month++)
                {
                    var dueBillsPerMonth = billList.Where(bill => bill.DueDate.Month == month &&
                                                               bill.DueDate.Year == year).ToList();

                    var paidBillsPerMonth = billList.Where(bill => bill.PaymentDate?.Month == month &&
                                                                   bill.PaymentDate?.Year == year).ToList();

                    if (dueBillsPerMonth.Count == 0 && paidBillsPerMonth.Count == 0)
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
                        DueEarnValue = dueBillsPerMonth.Where(bill => bill.Type == PayReceive.Receive).Sum(bill => bill.Value),
                        DueLostValue = dueBillsPerMonth.Where(bill => bill.Type == PayReceive.Pay).Sum(bill => -bill.Value),

                        PaidEarnValue = paidBillsPerMonth.Where(bill => bill.Type == PayReceive.Receive).Sum(bill => bill.PaidValue),
                        PaidLostValue = paidBillsPerMonth.Where(bill => bill.Type == PayReceive.Pay).Sum(bill => -bill.PaidValue),
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
        }

        private IEnumerable<string> GenerateMonths(IEnumerable<BillGraphItem> billList)
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

        private int GetYearsQuantity(IEnumerable<BillGraphItem> billList)
        {
            var maxYear = billList.Select(bill => bill.DueDate.Year).Max();
            var minYear = billList.Select(bill => bill.DueDate.Year).Min();

            return (maxYear - minYear) + 1;
        }
    }
}
