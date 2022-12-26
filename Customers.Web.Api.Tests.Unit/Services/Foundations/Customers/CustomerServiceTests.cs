using Customers.Web.Api.Brokers.DateTimes;
using Customers.Web.Api.Brokers.Loggings;
using Customers.Web.Api.Brokers.Storages;
using Customers.Web.Api.Models.Customers;
using Customers.Web.Api.Services.Customers;
using Moq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Customers.Web.Api.Tests.Unit.Services.Foundations.Customers
{
    public partial class CustomerServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;

        private readonly ICustomerService customerService;

        public CustomerServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.customerService = new CustomerService(
                loggingBroker: this.loggingBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                storageBroker: this.storageBrokerMock.Object);
        }

        private static Customer CreateRandomCustomer() =>
            CreateCustomerFiller(dateTime: GetRandomDateTime()).Create();

        private static DateTimeOffset GetRandomDateTime()=>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Customer> CreateCustomerFiller(DateTimeOffset dateTime)
        {
            var Filler = new Filler<Customer>();
            Filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTime); 
            return Filler;
        }
        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
           actualException => actualException.SameExceptionAs(expectedException);

        private static int GetRandomNumber() =>
           new IntRange(min: 2, max: 10).GetValue();

        public static TheoryData MinutesBeforeOrAfter()
        {
            int randomNumber = GetRandomNumber();
            int randomNegativeNumber = GetRandomNegativeNumber();

            return new TheoryData<int>
            {
                randomNumber,
                randomNegativeNumber
            };
        }
        private static int GetRandomNegativeNumber() =>
           -1 * new IntRange(min: 2, max: 10).GetValue();

        private static NpgsqlException GetNpsqlException() =>
            (NpgsqlException)FormatterServices.GetSafeUninitializedObject(typeof(NpgsqlException));
    }
}
