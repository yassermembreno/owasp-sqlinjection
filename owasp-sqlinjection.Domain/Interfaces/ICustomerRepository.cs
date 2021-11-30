using System;
using owasp_sqlinjection.Domain.Entities;

namespace owasp_sqlinjection.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer FindByDNI(string dni);
        IEnumerable<Customer> FindByNames(string names);
    }
}

