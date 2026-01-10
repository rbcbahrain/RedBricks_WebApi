namespace RedBricksApi.Models
{
    public class CartItems
    {
        public int CartItemId { get; set; }
        public int  CartId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }  
        public string ServiceName { get; set; }=string.Empty;
        public int Quantity { get; set; }   
        public int AddressId { get; set; }
        public string AddressName { get; set; } = string.Empty;
        public DateTime ServiceDate { get; set; }
        public decimal Price { get; set; } = 0;
        public DateTime AddedAt { get; set; }

    }
}
