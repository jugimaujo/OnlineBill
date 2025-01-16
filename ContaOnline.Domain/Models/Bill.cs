using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class Bill
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CheckingAccountId { get; set; }
        public PayReceive Type { get; set; }
        public string CategoryId { get; set; }
        public string ContactId { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Increase {  get; set; }
        public decimal? PaidValue { get; set; }
    }
}
