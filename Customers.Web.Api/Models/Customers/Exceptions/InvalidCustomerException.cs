
using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class InvalidCustomerException : Xeption
    {
        public InvalidCustomerException()
           : base(message: "Invalid Customer. Please correct the errors and try again.")
        { }
    }
}
