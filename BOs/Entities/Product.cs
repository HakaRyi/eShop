namespace BOs.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string Weight { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; } 
    }
}