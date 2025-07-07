using System;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;

namespace OnlineBill.Repository
{
    public class CheckingAccountRepository : ICheckingAccountRepository
    {
        public void Add(CheckingAccount checkingAccount)
        {
            string storedProcedure = "spr_checking_account_add";

            Database.Execute(storedProcedure, checkingAccount);
        }

        public void Update(CheckingAccount checkingAccount)
        {
            string storedProcedure = "spr_checking_account_update";

            Database.Execute(storedProcedure, param: new { id = checkingAccount.Id, description = checkingAccount.Description });
        }

        public void Delete(string id)
        {
            string storedProcedure = "spr_checking_account_delete";

            Database.Execute(storedProcedure, new { id = id });
        }

        public IEnumerable<CheckingAccount> GetAll(string userId)
        {
            string storedProcedure = "spr_checking_account_get_all";

            return Database.QueryCollection<CheckingAccount>(storedProcedure, new { userId = userId });
        }

        public CheckingAccount GetById(string id)
        {
            string storedProcedure = "spr_checking_account_get_by_id";

            return Database.QueryEntity<CheckingAccount>(storedProcedure, new { id = id });
        }


        public IEnumerable<string> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
