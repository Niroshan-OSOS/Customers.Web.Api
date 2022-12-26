using Customers.Web.Api.Models.Customers;
using Moq;
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
        public async Task ShouldAddCustomerAsync()
        {
            // given
            Customer randomCustomer = CreateRandomCustomer();
            Customer inputCustomer = randomCustomer;
            Customer insertedCustomer = inputCustomer;
            Customer expectedCustomer = insertedCustomer;

            this.dateTimeBrokerMock.Setup(broker =>
               broker.GetCurrentDateTimeOffset())
                   .Returns(randomCustomer.CreatedDate);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertCustomerAsync(inputCustomer))
                    .ReturnsAsync(insertedCustomer);


            // when
            Customer actualCustomer =
                await this.customerService.AddCustomerAsync(inputCustomer);
            
            //then
            actualCustomer.Should().BeEquivalentTo(expectedCustomer);

            this.dateTimeBrokerMock.Verify(broker =>
            broker.GetCurrentDateTimeOffset(),
            Times.Once);

            this.storageBrokerMock.Verify(broker=>
            broker.InsertCustomerAsync(inputCustomer), 
            Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
