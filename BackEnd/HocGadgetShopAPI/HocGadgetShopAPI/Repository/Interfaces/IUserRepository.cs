using HocGadgetShopAPI.Models.Domain.Entity;

namespace HocGadgetShopAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmail(string email);
        Task<int> Create(UserEntity user);
    }
}
