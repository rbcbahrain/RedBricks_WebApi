namespace RedBricksApi.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int  CartId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }  
        public decimal Quantity { get; set; }   
        public decimal Price { get; set; } = 0;
        public DateTime AddedAt { get; set; }

    }
}
