using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class BillListItem
    {
        public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public PayReceive Type { get; set; }
        public string Description { get; set; }
        public string? Contact {  get; set; }
        public string Category { get; set; }
        public decimal Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidValue { get; set; }

        public string CategoryId { get; set; }
        public string CheckingAccountId { get; set; }
    }
}
