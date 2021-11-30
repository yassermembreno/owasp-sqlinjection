using System;
namespace owasp_sqlinjection.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T t);
        int Update(T t);
        bool Delete(T t);
        IEnumerable<T> FindAll();
    }
}

