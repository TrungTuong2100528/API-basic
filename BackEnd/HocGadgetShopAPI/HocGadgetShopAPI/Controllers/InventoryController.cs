using Azure.Core;
using HocGadgetShopAPI.Models;
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
       
        [HttpPost]
        //B3. Model Binding bắt đầu làm việc
        public ActionResult SaveInventoryData(InventoryRequestDto requestDto)
        {
            //Tạo kết nối SQL
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=DESKTOP-IBRTGQQ\\MSSQLSERVER01;Database=gadgetShop;User Id=project;Password=project;TrustServerCertificate=True;"

            };
            //Tạo SqlCommand
            SqlCommand command = new SqlCommand
            {
                CommandText= "sp_SaveinventoryData",
                CommandType=CommandType.StoredProcedure,
                Connection=connection
            };
            //Truyền tham số cho Stored Procedure
            command.Parameters.AddWithValue("@ProductID", requestDto.ProductID);
            command.Parameters.AddWithValue("@ProductName", requestDto.ProductName);
            command.Parameters.AddWithValue("@AvailableQTy", requestDto.AvailableQTy);
            command.Parameters.AddWithValue("@ReOderPoint", requestDto.ReOderPoint);

            //Thực thi SQL
            connection.Open();
            //ExecuteNonQuery() dùng để INSERT / UPDATE / DELETE
            command.ExecuteNonQuery();
            connection.Close();
            //B4 Controller xử lý logic
            return Ok();
        }

        [HttpGet]
        //Lấy danh sách inventory
        public ActionResult GetInventoryData()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=DESKTOP-IBRTGQQ\\MSSQLSERVER01;Database=gadgetShop;User Id=project;Password=project;TrustServerCertificate=True;"

            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_GetInventoryData",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            //Mở kết nối & chuẩn bị list
            connection.Open();
            List<InventoryDto> respone = new List<InventoryDto>();

            //Đọc dữ liệu bằng SqlDataReader
            using (SqlDataReader sqlDataReader = command.ExecuteReader()) //ExecuteReader dùng để SELECT(trả về nhiều dòng)
            {
                //Vòng lặp đọc từng dòng
                while (sqlDataReader.Read())
                {
                    //Map dữ liệu SQL → DTO
                    InventoryDto inventoryDto = new InventoryDto();
                    inventoryDto.ProductID = Convert.ToInt32(sqlDataReader["ProductId"]);
                    inventoryDto.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                    inventoryDto.AvailableQty = Convert.ToInt32(sqlDataReader["AvailableQty"]);
                    inventoryDto.ReOderPoint = Convert.ToInt32(sqlDataReader["ReOderPoint"]);

                    //Thêm vào list
                    respone.Add(inventoryDto);
                }
            }
            //Đóng kết nối & trả kết quả
            connection.Close();
            return Ok(respone);
        }

        [HttpDelete]
        //Lấy danh sách inventory
        public ActionResult DeleteInventoryData( int productId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=DESKTOP-IBRTGQQ\\MSSQLSERVER01;Database=gadgetShop;User Id=project;Password=project;TrustServerCertificate=True;"

            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteInventoryDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            //Mở kết nối & chuẩn bị list
            connection.Open();

            command.Parameters.AddWithValue("@ProductId", productId);

            command.ExecuteNonQuery();

            //Đóng kết nối & trả kết quả
            connection.Close();
            return Ok();
        }

        [HttpPut]
        //Lấy danh sách inventory
        public ActionResult UpdateInventoryData(InventoryRequestDto inventoryRequest)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=DESKTOP-IBRTGQQ\\MSSQLSERVER01;Database=gadgetShop;User Id=project;Password=project;TrustServerCertificate=True;"

            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteInventoryDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };
            //Mở kết nối & chuẩn bị list
            connection.Open();

            command.Parameters.AddWithValue("@ProductId", inventoryRequest);

            command.ExecuteNonQuery();

            //Đóng kết nối & trả kết quả
            connection.Close();
            return Ok();
        }
    }
}
