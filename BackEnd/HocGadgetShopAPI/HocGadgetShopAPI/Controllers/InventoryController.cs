using Azure.Core;
using HocGadgetShopAPI.Business.Interfaces;
using HocGadgetShopAPI.Models.Dtos.Inventory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;


namespace HocGadgetShopAPI.Controllers
{
    //Báo cho ASP.NET biết: Đây là Web API;Tự động validate model;Tự map JSON body → object
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] InventoryRequestDto dto)
        {
            _service.Save(dto);
            return Ok(new { message = "Created successfully" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

    }
}
