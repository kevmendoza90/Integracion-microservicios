using Business.Core.Config;
using Business.Core.Dtos;
using Business.Core.Interfaces.Services;
using Common.Wrappers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NugetPackage_Rest.Exceptions;
using NugetPackage_Rest.Interfaces.IServices;
using System;
using System.Threading.Tasks;

namespace Business.Infraestructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRest _rest;
        private readonly ApiAddressesConfig _config;

        public AddressService(IRest rest, IOptions<ApiAddressesConfig> options)
        {
            _rest = rest;
            _config = options.Value;   // BaseUrl = https://localhost:5003/api/
        }

        public async Task<HttpResponse<AddressDto>> AddAsync(AddressDto addressDto)
        {
            try
            {
                // POST https://localhost:5003/api/Position  (microservicio Addresses)
                return await _rest.Post
                    .WithoutAuth()
                    .WithUri(_config.BaseUrl, "Position")
                    .WithBody(addressDto)
                    .DeserializeWithAsync<HttpResponse<AddressDto>>();
            }
            catch (ApiException ex) when (ex.Reason == ApiFailureReason.HttpError)
            {
                // Addresses respondió 400/404/500: la librería trae el body del error.
                return LeerError(ex);
            }
            catch (ApiException ex)
            {
                // Red, timeout, etc.: Addresses no está levantado o no responde.
                return new HttpResponse<AddressDto>
                {
                    Succeeded = false,
                    ErrorCode = 503,
                    ErrorMessage = $"No se pudo contactar a Addresses: {ex.Message}"
                };
            }
        }

        private static HttpResponse<AddressDto> LeerError(ApiException ex)
        {
            try
            {
                var error = JsonConvert.DeserializeObject<HttpResponse<AddressDto>>(ex.ResponseBody ?? "");
                if (error != null)
                {
                    error.Succeeded = false;
                    return error;
                }
            }
            catch (JsonException) { }

            return new HttpResponse<AddressDto>
            {
                Succeeded = false,
                ErrorCode = ex.StatusCode ?? 500,
                ErrorMessage = ex.Message
            };
        }
    }
}