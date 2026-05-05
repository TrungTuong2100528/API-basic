using HocGadgetShopAPI.Infrastructure;
using HocGadgetShopAPI.Models.Domain.Entity;
using HocGadgetShopAPI.Models.Dtos.Auth;
using HocGadgetShopAPI.Repository.Interfaces;
using BCrypt.Net;
using HocGadgetShopAPI.Service.Interfaces;
namespace HocGadgetShopAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        private readonly JwtService _jwt;

        public AuthService(IUserRepository repo, JwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public async Task<string> Register(RegisterRequestDto request)
        {
            var existing = await _repo.GetByEmail(request.Email);
            if (existing != null)
                throw new Exception("Email đã tồn tại");

            var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new UserEntity
            {
                Email = request.Email,
                PasswordHash = hash,
                Role = "Staff"
            };

            var id = await _repo.Create(user);
            user.Id = id;

            return _jwt.GenerateToken(user);
        }

        public async Task<string> Login(LoginRequestDto request)
        {
            var user = await _repo.GetByEmail(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Sai tài khoản");

            return _jwt.GenerateToken(user);
        }
    }
}
