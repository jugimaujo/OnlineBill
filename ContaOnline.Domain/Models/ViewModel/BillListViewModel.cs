using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class BillListViewModel
    {
        public IEnumerable<BillListItem> BillList { get; set; }
        public BillFilter Filter { get; set; }
        public IEnumerable<CheckingAccount> CheckingAccountList { get; set; }
        public IEnumerable<BillCategory> BillCategoryList { get; set; }
    }
}
