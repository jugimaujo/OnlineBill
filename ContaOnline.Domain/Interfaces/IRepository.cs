using System;

namespace OnlineBill.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
        T GetById(string id);
        IEnumerable<T> GetAll(string userId);
        IEnumerable<string> Validate();
    }
}
