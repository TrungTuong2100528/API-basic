using HocGadgetShopAPI.Models.Dtos.Customer;
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

        public void Create(CustomerRequestDto dto)
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_SaveCustomerDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CustomerId", dto.CustomerId);
            command.Parameters.AddWithValue("@FirstName", dto.FirstName);
            command.Parameters.AddWithValue("@LastName", dto.LastName);
            command.Parameters.AddWithValue("@Email", dto.Email);
            command.Parameters.AddWithValue("@Phone", dto.Phone);
            command.Parameters.AddWithValue("@RegistrationDate", dto.RegistrationDate);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<CustomerDto> GetAll()
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_GetCustomerDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            List<CustomerDto> result = new();

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new CustomerDto
                {
                    CustomerId = (int)reader["CustomerId"],
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    RegistrationDate = reader["RegistrationDate"].ToString()
                });
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

        public void Update(CustomerRequestDto dto)
        {
            using SqlConnection connection = CreateConnection();
            SqlCommand command = new SqlCommand("sp_UpdateCustomerDetails", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@CustomerId", dto.CustomerId);
            command.Parameters.AddWithValue("@FirstName", dto.FirstName);
            command.Parameters.AddWithValue("@LastName", dto.LastName);
            command.Parameters.AddWithValue("@Email", dto.Email);
            command.Parameters.AddWithValue("@Phone", dto.Phone);
            command.Parameters.AddWithValue("@RegistrationDate", dto.RegistrationDate);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

}
