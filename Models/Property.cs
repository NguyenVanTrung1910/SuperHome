using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestVersion.Models
{
    public class Property
    {
        [Key]
        [Range(0, 100)]

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Address { get; set; }
        public int  Quantity { get; set; }
        public string? Description { get; set; }
        public int Acreage { get; set; }
        public int Bed { get; set; }
        public int Bath { get; set; }
        [ForeignKey("Type")]
        public int TypeId {  get; set; }
        public virtual Type? Type { get; set; }


    }
}
