using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class BillFilter
    {
        public DateTime? InitialDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public PayReceive? Type { get; set; }
        public BillCategory BillCategoryId { get; set; }
        public string ContactId { get; set; }
        public string UserId { get; set; }
    }
}
