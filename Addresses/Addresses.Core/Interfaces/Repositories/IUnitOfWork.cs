

using Addresses.Domain.Models;

namespace Addresses.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Country> CountryRepository { get; }
        IRepository<City> CityRepository { get; }
        IRepository<Position> PositionRepository { get; }
        IRepository<State> StateRepository { get; }

    }
}
