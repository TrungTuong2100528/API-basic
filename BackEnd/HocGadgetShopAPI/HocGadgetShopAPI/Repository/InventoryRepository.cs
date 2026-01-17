using HocGadgetShopAPI.Infrastructure;
using HocGadgetShopAPI.Models.Dtos.Inventory;
using HocGadgetShopAPI.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HocGadgetShopAPI.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public InventoryRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public void Create(InventoryRequestDto dto)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
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
            using SqlConnection connection = _connectionFactory.CreateConnection();
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

        public void Update(InventoryRequestDto dto)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            SqlCommand command = new SqlCommand("sp_UpdateInventoryData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();

            command.Parameters.AddWithValue("@ProductId", dto.ProductID);
            command.Parameters.AddWithValue("@ProductName", dto.ProductName);
            command.Parameters.AddWithValue("@AvailableQTy", dto.AvailableQTy);
            command.Parameters.AddWithValue("@ReOderPoint", dto.ReOderPoint);

            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete(int productId)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            SqlCommand sqlCommand = new SqlCommand("sp_DeleteInventoryDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            //Mở kết nối & chuẩn bị list
            connection.Open();

            sqlCommand.Parameters.AddWithValue("@ProductId", productId);

            sqlCommand.ExecuteNonQuery();


        }

        public List<InventoryDto> Search(string productName)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();

            SqlCommand command = new SqlCommand("sp_searchInventory", connection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.AddWithValue("@productName", productName);

            connection.Open();
            List<InventoryDto> response = new List<InventoryDto>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    InventoryDto dto = new InventoryDto
                    {
                        ProductID = Convert.ToInt32(reader["ProductId"]),
                        ProductName = Convert.ToString(reader["ProductName"]),
                        AvailableQty = Convert.ToInt32(reader["AvailableQty"]),
                        ReOderPoint = Convert.ToInt32(reader["ReOderPoint"])
                    };

                    response.Add(dto);
                }
            }

            return response;
        }

    }
}
