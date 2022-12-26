using Customers.Web.Api.Models.Customers;
using Customers.Web.Api.Models.Customers.Exceptions;
using Moq;
using Npgsql;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Web.Api.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependancyExeptionOnAddIfSqlErrorsOccursAndLogItAsync()
        {
            Customer someCustomer = CreateRandomCustomer();
            NpgsqlException npgsqlException = GetNpsqlException();

            var failedCustomerStorageException =
                new FailedCustomerStorageException(npgsqlException);

            var expectedCustomerDependencyException =
                new CustomerDependencyException(failedCustomerStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(npgsqlException);

            // when
            ValueTask<Customer> addCustomerTask =
                this.customerService.AddCustomerAsync(someCustomer);

            CustomerDependencyException actualCustomerDependencyException =
               await Assert.ThrowsAsync<CustomerDependencyException>(
                   addCustomerTask.AsTask);

            // then
            actualCustomerDependencyException.Should().BeEquivalentTo(
                expectedCustomerDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCustomerAsync(It.IsAny<Customer>()),
                    Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedCustomerDependencyException))),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
