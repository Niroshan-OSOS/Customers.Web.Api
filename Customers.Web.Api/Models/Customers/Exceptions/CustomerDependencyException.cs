using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class CustomerDependencyException : Xeption
    {
        public CustomerDependencyException(Xeption innerException)
            : base(message: "Customer dependency errors occured, please try again.",
                  innerException)
        {

        }
    }
}
