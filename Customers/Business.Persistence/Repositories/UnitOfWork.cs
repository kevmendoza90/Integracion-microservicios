using Business.Core.Interfaces.Repositories;
using Business.Domain.Models;
using Business.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Company> CompanyRepository { get; }

        public IRepository<Customer> CustomerRepository { get; }

        public IRepository<CustomerType> CustomerTypeRepository { get; }

        public IRepository<CustomerContact> CustomerContactRepository { get; }

      
        public UnitOfWork(ApplicationDbContext context)
        {
            CompanyRepository = new Repository<Company>(context);
            CustomerRepository = new Repository<Customer>(context);
            CustomerTypeRepository = new Repository<CustomerType>(context);
            CustomerContactRepository = new Repository<CustomerContact>(context);
        }
    }
}
