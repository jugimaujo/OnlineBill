using System;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;

namespace OnlineBill.Repository
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            string storedProcedure = "spr_user_add";

            Database.Execute(storedProcedure, user);
        }

        public void Update(User user)
        {
            string storedProcedure = "spr_user_update";

            Database.Execute(storedProcedure, user);
        }

        public void Delete(string id)
        {
            string storedProcedure = "spr_user_delete";

            Database.Execute(storedProcedure, new { id = id });
        }

        public IEnumerable<User> GetAll()
        {
            string storedProcedure = "spr_user_get_all";

            return Database.QueryCollection<User>(storedProcedure, null);
        }

        public User GetById(string id)
        {
            string storedProcedure = "spr_user_get_by_id";

            return Database.QueryEntity<User>(storedProcedure, new { id = id });
        }

        public User GetByEmailPassword(string email, string password)
        {
            string storedProcedure = "spr_user_get_by_email_password";

            return Database.QueryEntity<User>(storedProcedure, new { email = email, password = password });
        }

        public IEnumerable<string> Validate()
        {
            throw new NotImplementedException();
        }

        #region Non used methods
        public IEnumerable<User> GetAll(string userId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
