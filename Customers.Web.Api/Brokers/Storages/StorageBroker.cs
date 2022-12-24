using Microsoft.EntityFrameworkCore;

namespace Customers.Web.Api.Brokers.Storages
{
    public partial class StorageBroker :DbContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration["DBPostgreSQL:ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
