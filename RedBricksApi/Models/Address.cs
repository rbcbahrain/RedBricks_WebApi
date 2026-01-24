using System.ComponentModel.DataAnnotations;

namespace RedBricksApi.Models
{
    public class Address
    {

        [Key]
        public int AddressId { get; set; }
        public string? ContactName { get; set; }
        public string? ContactNo { get; set; }
        public string? Line1 { get; set; } = string.Empty;
        public string? Line2 { get; set; } = string.Empty;
        public string? Line3 { get; set; } = string.Empty;
        public int City { get; set; }
        public int Country { get; set; }
        public int UserId { get; set; }
        public string? Location { get; set; }=string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
