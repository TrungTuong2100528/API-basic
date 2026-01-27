using HocGadgetShopAPI.Models.Domain.Entity;
using HocGadgetShopAPI.Models.Dtos.Customer;

namespace HocGadgetShopAPI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        void Create(CustomerEntity entity);
        void Update(CustomerEntity entity);
        void Delete(int customerId);
        List<CustomerEntity> GetAll();
    }
}
