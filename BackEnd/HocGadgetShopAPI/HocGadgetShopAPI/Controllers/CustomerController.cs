using HocGadgetShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HocGadgetShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection CreateConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );
        }


        [HttpPost]
        public ActionResult SaveInventoryData(CustomerRequestDto requestDto)
        {
            using SqlConnection connection = CreateConnection();

            //Tạo SqlCommand
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_SaveCustomerDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection
            };

            //Truyền tham số cho Stored Procedure
            command.Parameters.AddWithValue("@CustomerId", requestDto.CustomerId);
            command.Parameters.AddWithValue("@FirstName", requestDto.FirstName);
            command.Parameters.AddWithValue("@LastName", requestDto.LastName);
            command.Parameters.AddWithValue("@Email", requestDto.Email);
            command.Parameters.AddWithValue("@Phone", requestDto.Phone);
            command.Parameters.AddWithValue("@RegistrationDate", requestDto.RegistrationDate);

            //Thực thi SQL
            connection.Open();
            //ExecuteNonQuery() dùng để INSERT / UPDATE / DELETE
            command.ExecuteNonQuery();
            connection.Close();
            //B4 Controller xử lý logic
            return Ok();
        }

    }
}
