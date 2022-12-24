using Customers.Web.Api.Brokers.DateTimes;
using Customers.Web.Api.Brokers.Loggings;
using Customers.Web.Api.Brokers.Storages;
using Customers.Web.Api.Services.Customers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StorageBroker>();

builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();

builder.Services.AddTransient<ICustomerService, CustomerService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
