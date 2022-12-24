using Xeptions;

namespace Customers.Web.Api.Models.Customers.Exceptions
{
    public class NullCustomerException : Xeption
    {
        public NullCustomerException()
          : base(message: "Customer is null.")
        { }
    }
}
