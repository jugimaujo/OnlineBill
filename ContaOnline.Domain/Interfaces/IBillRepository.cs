using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBill.Domain.Models;

namespace OnlineBill.Domain.Interfaces
{
    public interface IBillRepository : IRepository<Bill>
    {
        IEnumerable<BillListItem> GetByUser(string userId);
        IEnumerable<BillListItem> GetByFilter(BillFilter filter);
    }
}
