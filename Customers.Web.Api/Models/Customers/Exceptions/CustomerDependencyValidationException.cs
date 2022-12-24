using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class CustomerDependencyValidationException : Xeption
    {
        public CustomerDependencyValidationException(Xeption innerException)
            : base(message: "Customer dependency validation occurred, fix the errors and try again.", innerException)
        {

        }
    }
}
