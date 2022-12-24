using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class FailedCustomerServiceException : Xeption
    {
        public FailedCustomerServiceException(Exception innerException)
             : base(message: "Failed customer service error occured. contact support.", innerException)
        {

        }
    }
}
