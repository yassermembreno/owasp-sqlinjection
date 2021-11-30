using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using owasp_sqlinjection.Domain.Entities;
using owasp_sqlinjection.Domain.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace owasp_sqlinjection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public IEnumerable<Customer> GetCustomers() => customerRepository.FindAll();

        [HttpGet("/dni/{dni}")]
        public ActionResult<Customer> GetCustomerById(string dni)
        {
            if (string.IsNullOrEmpty(dni))
            {
                return BadRequest();
            }

            Customer customer = customerRepository.FindByDNI(dni);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet("/name/{names}")]
        public ActionResult<IEnumerable<Customer>> GetCustomerByNames(string names)
        {
            if (string.IsNullOrEmpty(names))
            {
                return BadRequest();
            }

            IEnumerable<Customer> customers = customerRepository.FindByNames(names);
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            customerRepository.Create(customer);
            Customer dto = customerRepository.FindByDNI(customer.DNI);
            if (dto == null)
            {
                return BadRequest();
            }

            return Ok(dto);
        }
    }
}

