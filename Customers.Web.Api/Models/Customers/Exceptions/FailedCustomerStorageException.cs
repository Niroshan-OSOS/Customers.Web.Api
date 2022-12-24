using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class FailedCustomerStorageException : Xeption
    {
        public FailedCustomerStorageException(Exception innerException) 
            : base(message: "Failed customer storage error occured. contact support.", innerException)
        {
                
        }
    }
}
