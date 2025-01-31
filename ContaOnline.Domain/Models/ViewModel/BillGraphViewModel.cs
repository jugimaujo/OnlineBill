using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class BillGraphViewModel
    {
        public List<BillGraphGroupPerMonth> BillGroup { get; set; }
        public BillFilter Filter {  get; set; }
        public IEnumerable<CheckingAccount> CheckingAccountList { get; set; }
        public IEnumerable<BillCategory> BillCategoryList { get; set; }
        public IEnumerable<Contact> ContactList { get; set; }
    }

    public class BillGraphGroupPerMonth
    {
        public List<Bill> BillList { get; set; }
        public int Month {  get; set; }
        public decimal DueEarnValue { get; set; }
        public decimal DueLostValue { get; set; }
        public decimal DueTotalValue { get; set; }
        public decimal? PaidEarnValue { get; set; }
        public decimal? PaidLostValue { get; set; }
        public decimal? PaidTotalValue { get; set; }
    }

    public class BillGraphItem
    {
        public string Id { get; set; }
        public PayReceive Type { get; set; }
        public string CheckingAccountId { get; set; }
        public string CategoryId { get; set; }
        public string ContactId { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Value { get; set; }
        public DateTime? PayDay { get; set; }
        public decimal? PaidValue { get; set; }
    }
}
