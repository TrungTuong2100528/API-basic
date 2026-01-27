using HocGadgetShopAPI.Models.Domain.Entity;
using HocGadgetShopAPI.Models.Dtos.Inventory;

namespace HocGadgetShopAPI.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        void Create(InventoryEntity entity);
        void Update(InventoryEntity entity);
        void Delete(int productId);
        List<InventoryEntity> GetAll();
        List<InventoryEntity> Search(string productName);
    }
}
