
using Addresses.Core.Interfaces.Repositories;
using Addresses.Domain.Models;
using Addresses.Persistence.Data;
using Addresses.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Country> CountryRepository { get; }

        public IRepository<City> CityRepository { get; }

        public IRepository<Position> PositionRepository { get; }

        public IRepository<State> StateRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            CountryRepository = new Repository<Country>(context);
            CityRepository = new Repository<City>(context);
            PositionRepository = new Repository<Position>(context);
            StateRepository = new Repository<State>(context);
        }
    }
}
