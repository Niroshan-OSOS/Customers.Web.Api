using Customers.Web.Api.Models.Customers;

namespace Customers.Web.Api.Services.Customers
{
    public interface ICustomerService
    {
        ValueTask<Customer> AddCustomerAsync(Customer customer);
    }
}
