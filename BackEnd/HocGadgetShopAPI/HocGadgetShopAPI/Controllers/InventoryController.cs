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
        //gọi service
        private readonly IInventoryService _service;
        //readonly: chỉ được gán 1 lần 
        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpPost]
        //IActionResult: đại diện cho kết quả HTTP response mà API trả về cho client
        //[FromBody] Dữ liệu của tham số này được lấy từ BODY của HTTP request
        public IActionResult Create([FromBody] InventoryRequestDto dto)
        {
            _service.Save(dto);
            return Ok("Created successfully");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

    }
}
