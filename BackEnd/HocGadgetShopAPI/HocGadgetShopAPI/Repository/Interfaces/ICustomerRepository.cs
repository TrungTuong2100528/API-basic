using HocGadgetShopAPI.Models.Dtos.Customer;

namespace HocGadgetShopAPI.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        void Create(CustomerRequestDto dto);
        void Update(CustomerRequestDto dto);
        void Delete(int customerId);
        List<CustomerDto> GetAll();
    }
}
