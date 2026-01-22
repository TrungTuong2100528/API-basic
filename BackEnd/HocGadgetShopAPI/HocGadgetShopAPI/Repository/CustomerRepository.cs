using HocGadgetShopAPI.Models.Dtos.Customer;
using HocGadgetShopAPI.Models.Entity;
using HocGadgetShopAPI.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HocGadgetShopAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );
        }

        public void Create(CustomerEntity entity)
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_SaveCustomerDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@Phone", entity.Phone);
            command.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<CustomerEntity> GetAll()
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_GetCustomerDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            List<CustomerEntity> result = new();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var entity = new CustomerEntity();

                entity.SetCustomerInfo(
                    (int)reader["CustomerId"],
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                     DateTime.Parse(reader["RegistrationDate"].ToString())
                );
                entity.SetPhone(reader["Phone"].ToString());
                entity.SetEmail(reader["Email"].ToString());

                result.Add(entity);
            }


            return result;
        }

        public void Delete(int customerId)
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_DeleteCustomerDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CustomerId", customerId);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Update(CustomerEntity entity)
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_UpdateCustomerDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@Phone",entity.Phone);
            command.Parameters.AddWithValue("@RegistrationDate", entity.RegistrationDate);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

}
