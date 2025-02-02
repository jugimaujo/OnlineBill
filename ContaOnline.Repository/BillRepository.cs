using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.Domain.Models.ViewModel;

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

        public BillDetails GetDetailsById(string id)
        {
            string storedProcedure = "spr_bill_get_details_by_id";

            return Database.QueryEntity<BillDetails>(storedProcedure, new { id = id });
        }

        public IEnumerable<BillListItem> GetByUser(string userId)
        {
            string storedProcedure = "spr_bill_get_all";

            return Database.QueryCollection<BillListItem>(storedProcedure, new { userId = userId });
        }

        public IEnumerable<BillListItem> GetByFilter(BillFilter filter)
        {
            string storedProcedure = "spr_bill_get_between_dates";

            var filterList = GetByUser(filter.UserId).ToList();

            if (filter.InitialDate != null)
                filterList = filterList.Where(item => item.DueDate >= filter.InitialDate).ToList();

            if (filter.FinalDate != null)
                filterList = filterList.Where(item => item.DueDate <= filter.FinalDate).ToList();

            if (filter.CheckingAccountId != null)
                filterList = filterList.Where(item => item.CheckingAccountId == filter.CheckingAccountId).ToList();

            if (filter.CategoryId != null)
                filterList = filterList.Where(item => item.CategoryId == filter.CategoryId).ToList();

            return filterList;
        }

        public BillExhibitViewModel GetBillExhibitById(string id)
        {
            string storedProcedure = "spr_bill_exhibition_get_by_id";

            return Database.QueryEntity<BillExhibitViewModel>(storedProcedure, new { id = id });
        }

        public IEnumerable<BillGraphItem> GetAllInDetail(string userId)
        {
            string storedProcedure = "spr_bill_get_all_in_detail";

            return Database.QueryCollection<BillGraphItem>(storedProcedure, new { userId = userId });
        }

        public IEnumerable<BillGraphItem> GetByGraphFilter(BillFilter filter)
        {
            string storedProcedure = "spr_bill_get_between_dates";

            var filterList = GetAllInDetail(filter.UserId).ToList();

            if (filter.InitialDate != null)
                filterList = filterList.Where(item => item.DueDate >= filter.InitialDate).ToList();

            if (filter.FinalDate != null)
                filterList = filterList.Where(item => item.DueDate <= filter.FinalDate).ToList();

            if (filter.CategoryId != null)
                filterList = filterList.Where(item => item.CategoryId == filter.CategoryId).ToList();

            return filterList;
        }

        public IEnumerable<string> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
