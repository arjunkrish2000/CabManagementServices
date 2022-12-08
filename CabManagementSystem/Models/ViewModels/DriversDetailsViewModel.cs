namespace CabManagementSystem.Models.ViewModels
{

    public class DriverDetailsViewModel
    {

        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNo { get; set; }

        [Required]
        [Display(Name = "License Number")]
        public string LicenceNo { get; set; }

        public string CarModel { get; set; }

        [Required]
        [Display(Name = "Cab Name")]
        public string CabName { get; set; }


    }
}