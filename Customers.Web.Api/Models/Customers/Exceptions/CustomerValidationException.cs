using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class CustomerValidationException : Xeption
    {
        public CustomerValidationException(Xeption innerException)
         : base(message: "Customer validation errors occured, please try again.",innerException)
        {}
    }
}
