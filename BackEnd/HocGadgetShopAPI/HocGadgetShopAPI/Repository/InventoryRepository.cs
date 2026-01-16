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
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<InventoryDto> Search(string productName)
        {
            throw new NotImplementedException();
        }

    }
}
