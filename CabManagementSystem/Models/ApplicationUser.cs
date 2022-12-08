namespace CabManagementSystem.Models
{
    public enum UserType
    {
        Cab_Driver,
        Cab_User
    }
    public class ApplicationUser : IdentityUser
    {
        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        public UserType UserTypes { get; set; }
    }
}
