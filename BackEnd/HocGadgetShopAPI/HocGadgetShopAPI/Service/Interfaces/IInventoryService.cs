using HocGadgetShopAPI.Models.Dtos.Inventory;

namespace HocGadgetShopAPI.Business.Interfaces
{
    public interface IInventoryService
    {
        void Save(InventoryRequestDto dto);
        List<InventoryDto> GetAll();

        void Update(InventoryRequestDto dto);
        void Delete(int productId);

        List<InventoryDto> Search(string productName);

    }
}
