using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBill.Domain.Models;
using OnlineBill.Domain.Models.ViewModel;

namespace OnlineBill.Domain.Interfaces
{
    public interface IBillRepository : IRepository<Bill>
    {
        BillExhibitViewModel GetBillExhibitById(string id);
        IEnumerable<BillListItem> GetByUser(string userId);
        IEnumerable<BillListItem> GetByFilter(BillFilter filter);
        IEnumerable<BillGraphItem> GetAllInDetail(string userId);
        IEnumerable<BillGraphItem> GetByGraphFilter(BillFilter filter);
    }
}
