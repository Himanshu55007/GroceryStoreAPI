using GroceryStoreAPI.ActionFilters;
using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		public ILogger<CustomersController> Logger { get; }
		public CustomerService CustomerService { get; }

		public CustomersController(ILogger<CustomersController> logger, CustomerService customerService)
		{
			Logger = logger;
			CustomerService = customerService;
		}

		// GET: api/<CustomersController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IEnumerable<Customer>> GetAsync()
		{
			var customers = await CustomerService.GetCustomersAsync();

			Logger.LogInformation("Customer Retrieved");

			return customers;
		}

		// GET api/<CustomersController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<Customer>> GetAsync(int id)
		{
			var customer = await CustomerService.GetCustomerByIdAsync(id);

			if (customer == null)
			{
				Logger.LogInformation($"Customer id {id} not found");
				return NotFound();
			}

			return customer;
		}

		// POST api/<CustomersController>
		[HttpPost]
		[ServiceFilter(typeof(ValidationFilterAttribute))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult> PostAsync([FromBody] Customer customer)
		{
			var lookUpExistingCustomer = await CustomerService.GetCustomerByIdAsync(customer.Id);

			if (lookUpExistingCustomer != null)
			{
				Logger.LogInformation($"Customer id {customer.Id} already Present");

				return StatusCode(StatusCodes.Status409Conflict, $"Customer with the same id {customer.Id} already present");
			}

			await CustomerService.AddCustomerAsync(customer);

			Logger.LogInformation($"Customer Added");

			return StatusCode(StatusCodes.Status201Created, "Customer Created");
		}

		// PUT api/<CustomersController>/5
		[HttpPut("{id}")]

		public async Task<ActionResult> PutAsync(int id, [FromBody] string name)
		{
			var customer = await CustomerService.GetCustomerByIdAsync(id);

			if (customer == null)
			{
				Logger.LogInformation($"Customer id {id} not found");
				return NotFound();
			}

			await CustomerService.UpdateCustomerAsync(id, name);

			return StatusCode(StatusCodes.Status200OK);
		}
	}
}
