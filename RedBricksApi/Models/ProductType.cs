using System.ComponentModel.DataAnnotations;

namespace RedBricksApi.Models
{
    public class ProductType
    {
       
        public int TypeId { get; set; }
     
        public string Name { get; set; } =string.Empty;
        public string Description {  get; set; }=string.Empty;
        public string FileName { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public int CategoryId { get; set; } 
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
