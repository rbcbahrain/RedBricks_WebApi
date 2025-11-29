using System.ComponentModel.DataAnnotations;

namespace RedBricksApi.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public string Description {  get; set; }=string.Empty;
        public string Filename { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
