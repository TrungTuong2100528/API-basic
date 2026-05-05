using HocGadgetShopAPI.Models.Dtos.Auth;

namespace HocGadgetShopAPI.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto request);
        Task<string> Login(LoginRequestDto request);
    }
}
