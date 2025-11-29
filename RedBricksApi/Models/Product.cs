using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RedBricksApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; }=string.Empty;
        public int Type { get; set; }
        public Decimal Price { get; set; }
        public Decimal Rating { get; set; }
        public string FileName { get; set; }=string.Empty;
        public IFormFile? Image { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
