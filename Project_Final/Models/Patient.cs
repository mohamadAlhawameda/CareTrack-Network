using System.ComponentModel;

namespace Project_Final.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [DisplayName("Patient")]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public int TelePhone { get; set; }

        [DisplayName("Date of birth")]
        public string DateOfBirth { get; set; }

        public string AddimisionDate { get; set; }

        public string? DischargeDate { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public int? DoctorId { get; set; }
    }
}
