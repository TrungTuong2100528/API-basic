using HocGadgetShopAPI.Business.Interfaces;
using HocGadgetShopAPI.Models.Dtos.Customer;
using HocGadgetShopAPI.Repository.Interfaces;

namespace HocGadgetShopAPI.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public void Save(CustomerRequestDto dto)
        {
            _repository.Create(dto);
        }

        public List<CustomerDto> GetAll()
        {
            return _repository.GetAll();
        }

        public void Delete(int customerId)
        {
            _repository.Delete(customerId);
        }

        public void Update(CustomerRequestDto dto)
        {
            _repository.Update(dto);
        }
    }
}
