using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestVersion.Data;
namespace TestVersion.Models
{
    public class Agent 
    {
        [Key]
        [Range(0, 100)]

        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
       
        public string? Facebook { get; set; }
        public string? ImageUrl { get; set; }
        public Int64 Sdt {  get; set; }
    }
}
