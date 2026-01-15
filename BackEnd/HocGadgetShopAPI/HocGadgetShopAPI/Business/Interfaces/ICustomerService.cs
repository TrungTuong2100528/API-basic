using HocGadgetShopAPI.Models.Dtos.Customer;

namespace HocGadgetShopAPI.Business.Interfaces
{
    public interface ICustomerService
    {
        void Save(CustomerRequestDto dto);
        void Update(CustomerRequestDto dto);
        void Delete(int customerId);
        List<CustomerDto> GetAll();
    }
}
