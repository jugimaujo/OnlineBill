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
        public DateOnly Date { get; set; }
        public PayReceive Type { get; set; }
        public string Description { get; set; }
        public string Contact {  get; set; }
        public string Category { get; set; }
        public decimal Value { get; set; }
    }
}
