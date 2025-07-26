namespace BOs.Entities
{
    public class Feedbacks
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int Rates { get; set; }
        public string Description { get; set; }

        // Navigation Property
        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
