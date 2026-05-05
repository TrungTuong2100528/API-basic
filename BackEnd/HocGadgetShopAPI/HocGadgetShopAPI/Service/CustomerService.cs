using HocGadgetShopAPI.Business.Interfaces;
using HocGadgetShopAPI.Models.Dtos.Customer;
using HocGadgetShopAPI.Models.Domain.Entity;
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
            var entity = new CustomerEntity();
            entity.SetCustomerInfo(dto.CustomerId, dto.FirstName,dto.LastName,dto.RegistrationDate);
            entity.SetPhone(dto.Phone);
            entity.SetEmail(dto.Email);

            _repository.Create(entity);
        }

        public List<CustomerDto> GetAll()
        {
            var entity = _repository.GetAll();

            return entity.Select(e => new CustomerDto
            {
                CustomerId = e.CustomerId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Phone = e.Phone,
                Email = e.Email,
                RegistrationDate = e.RegistrationDate
            }).ToList();

        }

        public void Delete(int customerId)
        {
            _repository.Delete(customerId);
        }

        public void Update(CustomerRequestDto dto)
        {
            var entity = new CustomerEntity();
            entity.SetCustomerInfo(dto.CustomerId, dto.FirstName, dto.LastName, dto.RegistrationDate);
            entity.SetPhone(dto.Phone);
            entity.SetEmail(dto.Email);

            _repository.Update(entity);
        }
    }
}
