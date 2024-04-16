using System.ComponentModel;

namespace Project_Final.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }

        public string Medicine { get; set; }

        public int Price { get; set; }
        public string Description { get; set; }

        public virtual Doctor? doctor { get; set; }

        public virtual Patient? patient { get; set; }
    }
}
