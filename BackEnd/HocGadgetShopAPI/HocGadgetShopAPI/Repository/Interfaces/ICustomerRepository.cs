using HocGadgetShopAPI.Models.Dtos.Customer;
using HocGadgetShopAPI.Models.Entity;

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
