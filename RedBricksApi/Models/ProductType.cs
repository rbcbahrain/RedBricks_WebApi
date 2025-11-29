using System.ComponentModel.DataAnnotations;

namespace RedBricksApi.Models
{
    public class ProductType
    {
       
        public int TypeId { get; set; }
     
        public string Name { get; set; } =string.Empty;
        public string Description {  get; set; }=string.Empty;
        public string Filename { get; set; } = string.Empty;
        public int CategoryId { get; set; } 
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
