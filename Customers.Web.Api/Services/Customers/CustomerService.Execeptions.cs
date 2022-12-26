// ----------------------------------------------------------------------
// Copyright (C) 2022, by OSOS. All rights reserved.
// The information and source code contained herein is the exclusive
// property of OSOS and may not be disclosed, examined or reproduced
// in whole or in part without explicit written authorization from OSOS.
// ----------------------------------------------------------------------

using Customers.Web.Api.Models.Customers;
using Customers.Web.Api.Models.Customers.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Xeptions;

namespace Customers.Web.Api.Services.Customers
{
    public partial class CustomerService
    {
        private delegate ValueTask<Customer> ReturningCustomerFunction();
      

        private async ValueTask<Customer> TryCatch(ReturningCustomerFunction returningCustomerFunction)
        {
            try
            {
                return await returningCustomerFunction();
            }
            catch (NullCustomerException nullCustomerException)
            {
                throw CreateAndLogValidationException(nullCustomerException);
            }
            catch (InvalidCustomerException invalidCustomerException)
            {
                throw CreateAndLogValidationException(invalidCustomerException);
            }
            catch (NotFoundCustomerException notFoundCustomerException)
            {
                throw CreateAndLogValidationException(notFoundCustomerException);
            }
            catch (NpgsqlException npgsqlException)
            {
                var failedCustomerStorageException =
                     new FailedCustomerStorageException(npgsqlException);

                throw CreateAndLogCriticalDependencyException(failedCustomerStorageException);

            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedCustomerException =
                    new LockedCustomerException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedCustomerException);

            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedCustomerStorageException =
                    new FailedCustomerStorageException(dbUpdateException);

                throw CreateAndLogDependencyException(failedCustomerStorageException);
            }

            catch (Exception serviceException)
            {
                var failedCustomerServiceException =
                    new FailedCustomerServiceException(serviceException);

                throw CreateAndLogServiceException(failedCustomerServiceException);
            }
        }
        private CustomerValidationException CreateAndLogValidationException(Xeption exception)
        {
            var customerValidationException = new CustomerValidationException(exception);
            this.loggingBroker.LogError(customerValidationException);

            throw customerValidationException;
        }

        private CustomerDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var customerDependencyException = new CustomerDependencyException(exception);
            this.loggingBroker.LogCritical(customerDependencyException);

            throw customerDependencyException;
        }

        private CustomerDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var customerDependencyValidationException = new CustomerDependencyValidationException(exception);
            this.loggingBroker.LogError(customerDependencyValidationException);

            return customerDependencyValidationException;
        }
        private CustomerDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var customerDependencyException = new CustomerDependencyException(exception);
            this.loggingBroker.LogError(customerDependencyException);

            return customerDependencyException;
        }

        private CustomerServiceException CreateAndLogServiceException(Xeption exception)
        {
            var customerServiceException = new CustomerServiceException(exception);
            this.loggingBroker.LogError(customerServiceException);

            throw customerServiceException;
        }
    }
}
