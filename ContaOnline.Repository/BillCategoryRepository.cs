using System;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;

namespace OnlineBill.Repository
{
    public class BillCategoryRepository : IBillCategoryRepository
    {
        public void Add(BillCategory billCategory)
        {
            string storedProcedure = "spr_bill_category_add";

            Database.Execute(storedProcedure, billCategory);
        }

        public void Update(BillCategory billCategory)
        {
            string storedProcedure = "spr_bill_category_update";

            Database.Execute(storedProcedure, billCategory);
        }

        public void Delete(string id)
        {
            string storedProcedure = "spr_bill_category_delete";

            Database.Execute(storedProcedure, new { id = id });
        }

        public IEnumerable<BillCategory> GetAll(string userId)
        {
            string storedProcedure = "spr_bill_category_get_all";

            return Database.QueryCollection<BillCategory>(storedProcedure, new { userId = userId });
        }

        public BillCategory GetById(string id)
        {
            string storedProcedure = "spr_bill_category_get_by_id";

            return Database.QueryEntity<BillCategory>(storedProcedure, new { id = id });
        }

        public IEnumerable<string> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
