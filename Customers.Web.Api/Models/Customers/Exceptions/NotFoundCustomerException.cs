using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class NotFoundCustomerException : Xeption
    {
        public NotFoundCustomerException(Guid customerId)
            : base(message: $"Couldn't find customer with id: {customerId}")
        {

        }
    }
}
