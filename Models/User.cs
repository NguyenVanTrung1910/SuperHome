using System.ComponentModel.DataAnnotations;

namespace TestVersion.Models
{
    public class User
    {
        [Key]
        [Range(0, 100)]

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
