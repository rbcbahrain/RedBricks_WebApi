namespace RedBricksApi.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int  OrderId { get; set; }
        public int ServiceId { get; set; }  
        public decimal Quantity { get; set; }   
        public decimal Price { get; set; } = 0;
       
    }
}
