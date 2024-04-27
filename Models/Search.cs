using DataHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestVersion.Models
{
    public class Search :ViewComponent
    {
        public readonly DataHomeContext _context;
        public Search (DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.TypeList = _context.Type.ToList();
            var Location = _context.Property.FromSqlRaw($"SELECT * from Property  ")
                .Select( x => x.Address )
                .Distinct()
                .ToList();
            return View(Location);
        }
    }
}
