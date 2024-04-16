namespace Project_Final.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public ICollection<Reply>? Replies { get; set; }

    }
}
