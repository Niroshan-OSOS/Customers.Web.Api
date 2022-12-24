namespace Customers.Web.Api.Models.Customers
{
    public class Customer : IAuditable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CustomerStatus Status { get; set;}
        public DateTimeOffset CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
