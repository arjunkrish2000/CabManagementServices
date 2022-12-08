namespace CabManagementSystem.Models
{
  
    public class book
    {
        
        public int Id { get; set; }
        public DateTime BookDate { get; set; }

        [StringLength(25)]
        public string from { get; set; }


        [StringLength(25)]
        public string to { get; set; }
        public string time { get; set; }

        public int Fee { get; set; }

        [StringLength(25)]
        public string userEmail { get; set; }

    }

}
