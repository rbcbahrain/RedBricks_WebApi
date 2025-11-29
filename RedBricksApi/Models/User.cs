using System.ComponentModel.DataAnnotations;

namespace RedBricksApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Isdcode { get; set; }
        public string PhoneNo { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}