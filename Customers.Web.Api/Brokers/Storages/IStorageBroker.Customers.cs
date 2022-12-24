using Customers.Web.Api.Models.Customers;

namespace Customers.Web.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Customer> InsertCustomerAsync(Customer customer);
    }
}
