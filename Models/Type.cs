using System.ComponentModel.DataAnnotations;

namespace TestVersion.Models
{
    public class Type
    {
        [Key]
        [Range(0, 100)]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? form { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
