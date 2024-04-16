using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Final.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        // Foreign key for the associated message
        [ForeignKey("MessageId")] // Specify the foreign key
        public int? MessageId { get; set; }

        // Navigation property for the associated message
        public virtual Message? Message { get; set; }
    }
}
