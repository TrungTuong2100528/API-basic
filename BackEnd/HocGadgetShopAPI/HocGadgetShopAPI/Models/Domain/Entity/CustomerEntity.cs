namespace HocGadgetShopAPI.Models.Domain.Entity
{
    public class CustomerEntity
    {
        public int CustomerId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        public CustomerEntity()
        {
        }

        public void SetCustomerInfo(int customerId, string firstName, string lastName, DateTime registrationDate)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            RegistrationDate = registrationDate;
        }

        public void SetEmail(string email)
        {
            if (!email.Contains("@"))
                throw new Exception("Email phải chứa ký tự @");

            Email = email;
        }

        public void SetPhone(string phone)
        {
            if (phone.Length > 10)
                throw new Exception("Phone tối đa 10 chữ số");
            Phone = phone;
           
        }
    }
}
