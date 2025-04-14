namespace CouponAPI.Domain.Entities
{
    public class LocalUser
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string PasswordSalt { get; set; }
        public required string Role { get; set; }
    }
}
