using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Ports
{
    public interface ICustomerService
    {
        Task Add(Customer customer);
        Task<IEnumerable<Customer>> AddCustomersFromFile(IFormFile csvFile);
    }
}
