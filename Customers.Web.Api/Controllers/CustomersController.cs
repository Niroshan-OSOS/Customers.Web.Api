// ----------------------------------------------------------------------
// Copyright (C) 2022, by OSOS. All rights reserved.
// The information and source code contained herein is the exclusive
// property of OSOS and may not be disclosed, examined or reproduced
// in whole or in part without explicit written authorization from OSOS.
// ----------------------------------------------------------------------

using Customers.Web.Api.Models.Customers;
using Customers.Web.Api.Models.Customers.Exceptions;
using Customers.Web.Api.Services.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Customers.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : RESTFulController
    {
        private readonly ICustomerService customerService;
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService; 
        }
        [HttpPost]
        public async ValueTask<ActionResult<Customer>> PostCustomerAsync(Customer customer)
        {
            try
            {
                Customer createdTenant = 
                    await this.customerService.AddCustomerAsync(customer);
                return Created(createdTenant);
            }
            catch (CustomerValidationException customerValidationException)
               when (customerValidationException.InnerException
               is AlreadyExistsCustomerException)
            {
                return Conflict(customerValidationException.InnerException);
            }
            catch (CustomerValidationException customerValidationException)
            {
                return BadRequest(customerValidationException.InnerException);
            }
            catch (CustomerDependencyException customerDependencyException)
            {
                return InternalServerError(customerDependencyException);
            }
            catch (CustomerServiceException customerServiceException)
            {
                return InternalServerError(customerServiceException);
            }
        }
    }
}
