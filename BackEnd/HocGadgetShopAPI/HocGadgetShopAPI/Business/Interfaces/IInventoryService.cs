using HocGadgetShopAPI.Models.Dtos.Inventory;

namespace HocGadgetShopAPI.Business.Interfaces
{
    public interface IInventoryService
    {
        void Save(InventoryRequestDto dto);
        List<InventoryDto> GetAll();

    }
}
