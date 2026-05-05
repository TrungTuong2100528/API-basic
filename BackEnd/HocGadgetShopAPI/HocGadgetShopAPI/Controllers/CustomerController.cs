using HocGadgetShopAPI.Business.Interfaces;
using HocGadgetShopAPI.Models.Dtos.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;

namespace HocGadgetShopAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create(CustomerRequestDto dto)
        {
            _service.Save(dto);
            return Ok(new { message = "Customer created successfully" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPut]
        public IActionResult Update(CustomerRequestDto dto)
        {
            _service.Update(dto);
            return Ok(new { message = "Update successfully" });
        }

        [HttpDelete]
        public IActionResult Delete(int customerId)
        {
            _service.Delete(customerId);
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
