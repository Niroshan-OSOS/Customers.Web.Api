using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class InvalidCustomerExeption : Xeption
    {
        public InvalidCustomerExeption()
            :base(message: "Invalid Customer. Please correct the errors and try again.")
        {

        }
    }
}
