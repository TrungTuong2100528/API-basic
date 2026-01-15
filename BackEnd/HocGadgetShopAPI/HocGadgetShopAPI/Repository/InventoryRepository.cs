using HocGadgetShopAPI.Models.Dtos.Inventory;
using HocGadgetShopAPI.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HocGadgetShopAPI.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IConfiguration _configuration;

        public InventoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );
        }

        public void Create(InventoryRequestDto dto)
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_SaveinventoryData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ProductID", dto.ProductID);
            command.Parameters.AddWithValue("@ProductName", dto.ProductName);
            command.Parameters.AddWithValue("@AvailableQTy", dto.AvailableQTy);
            command.Parameters.AddWithValue("@ReOderPoint", dto.ReOderPoint);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<InventoryDto> GetAll()
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_GetInventoryData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            List<InventoryDto> result = new();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new InventoryDto
                {
                    ProductID = (int)reader["ProductId"],
                    ProductName = reader["ProductName"].ToString(),
                    AvailableQty = (int)reader["AvailableQty"],
                    ReOderPoint = (int)reader["ReOderPoint"]
                });
            }

            return result;
        }

        // Update / Delete / Search tương tự
    }
}
