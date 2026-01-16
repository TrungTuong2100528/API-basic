using HocGadgetShopAPI.Business.Interfaces;
using HocGadgetShopAPI.Models.Dtos.Inventory;
using HocGadgetShopAPI.Repository.Interfaces;

namespace HocGadgetShopAPI.Business
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public void Save(InventoryRequestDto dto)
        {
            if (dto.AvailableQTy < 0)
                throw new Exception("Quantity must be >= 0");

            _repository.Create(dto);
        }

        public List<InventoryDto> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
