using Customers.Web.Api.Models.Customers;
using Customers.Web.Api.Models.Customers.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
            ValueTask<Customer> addCustomerTask = 
                this.customerService.AddCustomerAsync(nullCustomer);



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

        //[Theory]
        //[MemberData(nameof(MinutesBeforeOrAfter))]
        //public async Task ShouldThrowValidationExceptionOnAddIfCreateDateIsNotRecentAndLogItAsync(
        //    int minutesBeforeOrAfter)
        //{
        //    // given
        //    int randomNumber = GetRandomNumber();
        //    DateTimeOffset randomDateTime = GetRandomDateTime();
        //    DateTimeOffset invalidDateTime =
        //        randomDateTime.AddMinutes(minutesBeforeOrAfter);

        //    Customer randomCustomer = CreateRandomCustomer();
        //    Customer invalidCustomer = randomCustomer;


        //    var invalidCustomerException = new InvalidCustomerException();

        //    invalidCustomerException.AddData(
        //       key: nameof(Customer.CreatedDate),
        //       values: $"Date is not recent");

        //    var expectedCustomerValidationException =
        //        new CustomerValidationException(invalidCustomerException);

        //    // when
        //    ValueTask<Customer> addCustomerAsync =
        //        this.customerService.AddCustomerAsync(invalidCustomer);

        //    // then
        //    var actualCustomerValidationException =
        //        await Assert.ThrowsAsync<CustomerValidationException>(() =>
        //            addCustomerAsync.AsTask());

        //    actualCustomerValidationException.Should().
        //        BeEquivalentTo(expectedCustomerValidationException);

        //    this.loggingBrokerMock.Verify(broker =>
        //        broker.LogError(It.Is(SameExceptionAs(
        //            expectedCustomerValidationException))),
        //                Times.Once);

        //    this.storageBrokerMock.Verify(broker =>
        //        broker.InsertCustomerAsync(randomCustomer),
        //            Times.Never);

        //    this.loggingBrokerMock.VerifyNoOtherCalls();
        //    this.storageBrokerMock.VerifyNoOtherCalls();
        //}

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfCustomerIsInvalidAndLogItAsync(
           string invalidText)
        {
            //given
            var invalidCustomer = new Customer
            {
                FirstName = invalidText,
                LastName = invalidText,
                Email = invalidText,
                Phone = invalidText,
            };
            var invalidCustomerExeption =
                new InvalidCustomerException();

            invalidCustomerExeption.AddData(
                key: nameof(Customer.Id),
                values: "Id is required");

            invalidCustomerExeption.AddData(
               key: nameof(Customer.FirstName),
               values: "Text is required");

            invalidCustomerExeption.AddData(
              key: nameof(Customer.LastName),
              values: "Text is required");

            invalidCustomerExeption.AddData(
             key: nameof(Customer.Email),
             values: "Text is required");

            invalidCustomerExeption.AddData(
             key: nameof(Customer.Phone),
             values: "Text is required");

            invalidCustomerExeption.AddData(
            key: nameof(Customer.CreatedDate),
            values: "Date is required");

            var expectedCustomerValidationException =
               new CustomerValidationException(invalidCustomerExeption);

            // when
            ValueTask<Customer> addTenatAsync =
                this.customerService.AddCustomerAsync(invalidCustomer);

            // then
            await Assert.ThrowsAsync<CustomerValidationException>(() =>
                addTenatAsync.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedCustomerValidationException))),
                        Times.Once());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertCustomerAsync(invalidCustomer),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();

        }
    }
}
