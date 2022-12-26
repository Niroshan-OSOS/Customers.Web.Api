using Customers.Web.Api.Models.Customers;
using Customers.Web.Api.Models.Customers.Exceptions;

namespace Customers.Web.Api.Services.Customers
{
    public partial class CustomerService
    {
        private void ValidateCustomerOnAdd(Customer customer) 
        {
            ValidateCustomerIsNotNull(customer);
            
            Validate(
                (Rule: IsInvalid(customer.Id), Parameter: nameof(Customer.Id)),
                (Rule: IsInvalid(customer.FirstName), Parameter: nameof(Customer.FirstName)),
                (Rule: IsInvalid(customer.LastName), Parameter: nameof(Customer.LastName)),
                (Rule: IsInvalid(customer.Email), Parameter: nameof(Customer.Email)),
                (Rule: IsInvalid(customer.Phone), Parameter: nameof(Customer.Phone)),
                (Rule: IsValidX(customer.CreatedDate), Parameter: nameof(Customer.CreatedDate)),
                (Rule: IsNotRecent(customer.CreatedDate), Parameter: nameof(Customer.CreatedDate)));
            
        }


        private void ValidateCustomerIsNotNull(Customer customer)
        {
            if (customer is null)
            {
                throw new NullCustomerException();
            }
        }
        private static dynamic IsInvalid(Guid customerId) => new
        {
            Condition = customerId == Guid.Empty,
            Message = "Id is required"
        };
        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };
        private static dynamic IsValidX(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };
        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };
        private bool IsDateNotRecent(DateTimeOffset date)
        {
            //DateTimeOffset currentDateTime =
            //    this.dateTimeBroker.GetCurrentDateTimeOffset();

            //TimeSpan timeDifference = currentDateTime.Subtract(date);
            //TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            //return timeDifference.Duration() > oneMinute;
            return false;
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTenantException =
                new InvalidCustomerException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTenantException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTenantException.ThrowIfContainsErrors();
        }
    }
}
