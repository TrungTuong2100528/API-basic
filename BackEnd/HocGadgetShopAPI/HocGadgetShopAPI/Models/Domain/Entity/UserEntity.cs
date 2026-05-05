namespace HocGadgetShopAPI.Models.Domain.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "Staff";
    }
}
