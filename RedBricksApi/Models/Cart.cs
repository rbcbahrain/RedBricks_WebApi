namespace RedBricksApi.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int  UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
