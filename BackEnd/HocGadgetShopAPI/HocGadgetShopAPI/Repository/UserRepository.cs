using HocGadgetShopAPI.Infrastructure;
using HocGadgetShopAPI.Models.Domain.Entity;
using HocGadgetShopAPI.Repository.Interfaces;
using Dapper;

namespace HocGadgetShopAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _db;

        public UserRepository(DbConnectionFactory db)
        {
            _db = db;
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            using var conn = _db.CreateConnection();

            return await conn.QueryFirstOrDefaultAsync<UserEntity>(
                "SELECT * FROM Users WHERE Email = @Email",
                new { Email = email }
            );
        }

        public async Task<int> Create(UserEntity user)
        {
            using var conn = _db.CreateConnection();

            var sql = @"INSERT INTO Users (Email, PasswordHash)
                    VALUES (@Email, @PasswordHash);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

            return await conn.ExecuteScalarAsync<int>(sql, user);
        }
    }
}
