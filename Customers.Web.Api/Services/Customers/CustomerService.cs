// ----------------------------------------------------------------------
// Copyright (C) 2022, by OSOS. All rights reserved.
// The information and source code contained herein is the exclusive
// property of OSOS and may not be disclosed, examined or reproduced
// in whole or in part without explicit written authorization from OSOS.
// ----------------------------------------------------------------------


using Customers.Web.Api.Brokers.DateTimes;
using Customers.Web.Api.Brokers.Loggings;
using Customers.Web.Api.Brokers.Storages;
using Customers.Web.Api.Models.Customers;

namespace Customers.Web.Api.Services.Customers
{
    public partial class CustomerService : ICustomerService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public CustomerService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker
            )
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }
        //public ValueTask<Customer> AddCustomerAsync(Customer customer)=>
        // TryCatch(async () =>
        //    {
        //    ValidateCustomerOnAdd(customer);

        //    return await storageBroker.InsertCustomerAsync(customer);
        //});

        public async ValueTask<Customer> AddCustomerAsync(Customer customer)
        {
           throw new NotImplementedException();
        }

    }
}
