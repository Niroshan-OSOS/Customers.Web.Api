using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class CustomerServiceException : Xeption
    {
        public CustomerServiceException(Xeption innerException)
            : base(message: "Customer service errors occured, please try again.",
                  innerException)
        {

        }
    }
}
