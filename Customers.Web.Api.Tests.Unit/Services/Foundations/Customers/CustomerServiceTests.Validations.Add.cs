using Customers.Web.Api.Models.Customers;
using Customers.Web.Api.Models.Customers.Exceptions;
using FluentAssertions;
using Moq;
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
        public async Task ShouldThrowValidationExeptionOnAddIfCustomerIsNullAndLogItAsync()
        {
            //given
            Customer nullCustomer = null;

            var nullCustomerExeption =
                new NullCustomerException();

            var expectedCustomerValidationExeption =
                new CustomerValidationException(nullCustomerExeption);

            //when
            ValueTask<Customer> addCustomerTask = this.customerService.AddCustomerAsync(nullCustomer);

            // then
            await Assert.ThrowsAsync<CustomerValidationException>(() =>
                addCustomerTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCustomerValidationExeption))),
                        Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();

        }
    }
}
