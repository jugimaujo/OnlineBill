using System;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;

namespace OnlineBill.Repository
{
    public class BillRepository : IBillRepository
    {
        public void Add(Bill bill)
        {
            string storedProcedure = "spr_bill_add";

            Database.Execute(storedProcedure, bill);
        }

        public void Update(Bill bill)
        {
            string storedProcedure = "spr_bill_update";

            Database.Execute(storedProcedure, bill);
        }

        public void Delete(string id)
        {
            string storedProcedure = "spr_bill_delete";

            Database.Execute(storedProcedure, new { id = id });
        }

        public IEnumerable<Bill> GetAll(string userId)
        {
            string storedProcedure = "spr_bill_get_all";

            return Database.QueryCollection<Bill>(storedProcedure, new { userId = userId });
        }

        public Bill GetById(string id)
        {
            string storedProcedure = "spr_bill_get_by_id";

            return Database.QueryEntity<Bill>(storedProcedure, new { id = id });
        }

        public IEnumerable<BillListItem> GetByUser(string userId)
        {
            string storedProcedure = "spr_bill_get_all";

            return Database.QueryCollection<BillListItem>(storedProcedure, new { userId = userId });
        }

        IEnumerable<BillListItem> IBillRepository.GetByFilter(BillFilter filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
