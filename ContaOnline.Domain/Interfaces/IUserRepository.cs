using System;
using OnlineBill.Domain.Models;

namespace OnlineBill.Domain.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        IEnumerable<User> GetAll();
        User GetByEmailPassword(string email, string password);
    }
}
