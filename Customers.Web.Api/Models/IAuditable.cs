namespace Customers.Web.Api.Models
{
    public interface IAuditable
    {
        DateTimeOffset CreatedDate { get; set; }
        Guid CreatedBy { get; set; }
    }
}
