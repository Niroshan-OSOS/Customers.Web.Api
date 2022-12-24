using Customers.Web.Api.Models.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Customers.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Customer> Customers { get; set; }

        public async ValueTask<Customer> InsertCustomerAsync(Customer customer)
        {
            using var broker = 
                new StorageBroker(configuration);
            EntityEntry<Customer> customerEntityEntry =
                await broker.Customers.AddAsync(customer);
            await broker.SaveChangesAsync();
            return customerEntityEntry.Entity;
        }
    }
}
