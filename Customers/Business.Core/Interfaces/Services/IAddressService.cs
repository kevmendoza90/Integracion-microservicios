using Business.Core.Dtos;
using Common.Wrappers;
using System.Threading.Tasks;

namespace Business.Core.Interfaces.Services
{
    public interface IAddressService
    {
        Task<HttpResponse<AddressDto>> AddAsync(AddressDto addressDto);
    }
}