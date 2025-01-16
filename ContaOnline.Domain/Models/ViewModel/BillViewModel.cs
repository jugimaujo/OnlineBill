using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class BillViewModel
    {
        public Bill BillInstance { get; set; }
        public IEnumerable<CheckingAccount> CheckingAccountList { get; set; }
        public IEnumerable<BillCategory> BillCategoryList { get; set; }
        public IEnumerable<Contact> ContactList { get; set; }
    }
}
