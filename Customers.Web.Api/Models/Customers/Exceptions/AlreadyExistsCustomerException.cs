using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class AlreadyExistsCustomerException : Xeption
    {
        public AlreadyExistsCustomerException(Exception innerException)
            : base(message: "Customer with the same id already exists.", innerException) 
        {

        }
    }
}
