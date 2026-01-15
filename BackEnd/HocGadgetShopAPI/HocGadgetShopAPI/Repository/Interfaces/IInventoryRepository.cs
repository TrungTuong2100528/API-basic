using HocGadgetShopAPI.Models.Dtos.Inventory;

namespace HocGadgetShopAPI.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        void Create(InventoryRequestDto dto);
        void Update(InventoryRequestDto dto);
        void Delete(int productId);
        List<InventoryDto> GetAll();
        List<InventoryDto> Search(string productName);
    }
}
