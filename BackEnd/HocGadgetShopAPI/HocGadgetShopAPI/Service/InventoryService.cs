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
            _repository.Create(dto);
        }

        public List<InventoryDto> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(InventoryRequestDto dto)
        {
            _repository.Update(dto);
        }
        public void Delete(int productId)
        {
            _repository.Delete(productId);
        }

        public List<InventoryDto> Search(string productName)
        {
            return _repository.Search(productName);
        }
    }
}
