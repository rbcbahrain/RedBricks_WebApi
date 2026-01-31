namespace RedBricksApi.Models
{
    public class OrderItems
    {
        public int OrderItemId { get; set; }
        public int  OrderId { get; set; }
        public int ServiceId { get; set; }  
        public decimal Quantity { get; set; }   
        public decimal Price { get; set; } = 0;
        public int AddressId { get; set; }
        public string AddressName { get; set; } = string.Empty;
        public DateTime ServiceDate { get; set; }
        public int UserId { get; set; }


    }
}
