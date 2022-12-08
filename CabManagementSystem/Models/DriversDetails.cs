namespace CabManagementSystem.Models
{
    [Index("RegistrationNo", IsUnique = true)]
    [Index("LicenceNo", IsUnique = true)]


    public class DriverDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNo { get; set; }

        [Required]
        [Display(Name = "License Number")]
        public string LicenceNo { get; set; }

        [Required]
        [Display(Name = "Cab Name")]
        public string CabName { get; set; }

        public string CarModel { get; set; }

        public ApplicationUser CabDriver { get; set; }
        [ForeignKey(nameof(CabDriver))]
        public string DriverId { get; set; }
    }
}