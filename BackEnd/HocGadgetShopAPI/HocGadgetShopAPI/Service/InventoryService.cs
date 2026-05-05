using HocGadgetShopAPI.Business.Interfaces;
using HocGadgetShopAPI.Models.Dtos.Inventory;
using HocGadgetShopAPI.Models.Domain.Entity;
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
            var entity = new InventoryEntity();

                entity.SetProductInfo(dto.ProductID, dto.ProductName);
                entity.SetInitialQuantity(dto.AvailableQTy);
                entity.SetReOrderPoint(dto.ReOderPoint);

            _repository.Create(entity);
        }

        public List<InventoryDto> GetAll()
        {
            var entities = _repository.GetAll();
            //LINQ
            return entities.Select(e => new InventoryDto
            {
                ProductID = e.ProductId,
                ProductName = e.ProductName,
                AvailableQty = e.AvailableQty,
                ReOderPoint = e.ReOrderPoint
            }).ToList(); // duyệt từng phần tử
           
        }

        public void Update(InventoryRequestDto dto)
        {
            var entity = new InventoryEntity();

            entity.SetProductInfo(dto.ProductID, dto.ProductName);
            entity.SetInitialQuantity(dto.AvailableQTy);
            entity.SetReOrderPoint(dto.ReOderPoint);

            _repository.Update(entity);
        }
        public void Delete(int productId)
        {
            _repository.Delete(productId);  
        }

        public List<InventoryDto> Search(string productName)
        {
            var entities = _repository.Search(productName);

            return entities.Select(e => new InventoryDto
            {
                ProductID = e.ProductId,
                ProductName = e.ProductName,
                AvailableQty = e.AvailableQty,
                ReOderPoint = e.ReOrderPoint
            }).ToList();
        }
    }
}
