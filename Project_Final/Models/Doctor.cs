using System.ComponentModel;

namespace Project_Final.Models
{
    public class Doctor
    {

        public int Id { get; set; }

        [DisplayName("Doctor")]
        public string Name { get; set; }
        public string Office { get; set; }
        public string Email { get; set; }
        public int TelePhone { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
    }
}
