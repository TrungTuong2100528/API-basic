using HocGadgetShopAPI.Infrastructure;
using HocGadgetShopAPI.Models.Dtos.Inventory;
using HocGadgetShopAPI.Models.Entity;
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
        public void Create(InventoryEntity entity)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            SqlCommand command = new SqlCommand("sp_SaveinventoryData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ProductID", entity.ProductId);
            command.Parameters.AddWithValue("@ProductName", entity.ProductName);
            command.Parameters.AddWithValue("@AvailableQTy", entity.AvailableQty);
            command.Parameters.AddWithValue("@ReOderPoint", entity.ReOrderPoint);

            connection.Open();
            command.ExecuteNonQuery();

        }

        public List<InventoryEntity> GetAll()
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            SqlCommand command = new SqlCommand("sp_GetInventoryData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            // tạo danh sách lấy dự liệu từ DB và DB sẽ loại có dữ liệu theo điều kiện của entity
            List<InventoryEntity> result = new List<InventoryEntity>();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Tạo object rỗng để gán dần các dữ liệu của các method
                var entity = new InventoryEntity();

                entity.SetProductInfo(
                    (int)reader["ProductId"],
                    reader["ProductName"].ToString()
                );

                entity.SetInitialQuantity((int)reader["AvailableQty"]);
                entity.SetReOrderPoint((int)reader["ReOderPoint"]);

                result.Add(entity);
            }
            return result;
        }

        public void Update(InventoryEntity entity)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            SqlCommand command = new SqlCommand("sp_UpdateInventoryData", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();

            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            command.Parameters.AddWithValue("@ProductName", entity.ProductName);
            command.Parameters.AddWithValue("@AvailableQTy", entity.AvailableQty);
            command.Parameters.AddWithValue("@ReOderPoint", entity.ReOrderPoint);

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

        public List<InventoryEntity> Search(string productName)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();

            SqlCommand command = new SqlCommand("sp_searchInventory", connection)
            {
                CommandType = CommandType.StoredProcedure,
            };

            command.Parameters.AddWithValue("@productName", productName);

            connection.Open();
            // tạo danh sách lấy dự liệu từ DB và DB sẽ loại có dữ liệu theo điều kiện của entity
            List<InventoryEntity> response = new List<InventoryEntity>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var entity = new InventoryEntity();

                    entity.SetProductInfo(
                        Convert.ToInt32(reader["ProductId"]),
                        Convert.ToString(reader["ProductName"])
                    );

                    entity.SetInitialQuantity(Convert.ToInt32(reader["AvailableQty"]));
                    entity.SetReOrderPoint(Convert.ToInt32(reader["ReOderPoint"]));

                    response.Add(entity);
                }

            }

            return response;
        }

    }
}
