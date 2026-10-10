using Business.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Company> CompanyRepository { get; }
        IRepository<Customer> CustomerRepository { get; }
        IRepository<CustomerType> CustomerTypeRepository { get; }
        IRepository<CustomerContact> CustomerContactRepository { get; }
        
    }
}
