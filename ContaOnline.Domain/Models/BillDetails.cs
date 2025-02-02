using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class BillDetails
    {
        public string Id { get; set; }
        public PayReceive Type { get; set; }
        public string CheckingAccountDescription { get; set; }
        public string CategoryName { get; set; }
        public string ContactName { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Increase { get; set; }
        public decimal? PaidValue { get; set; }
    }
}
