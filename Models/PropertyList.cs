using DataHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestVersion.Models
{
    public class PropertyList : ViewComponent
    {
        public readonly DataHomeContext _context;
        public PropertyList (DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var form = HttpContext.Session.GetString("loc") ?? "a";
            
            
             if (form == "a")
            {
                ViewBag.a = "active";
                ViewBag.loc = _context.Property.Include(p => p.Type).Take(6).ToList();

            }
            else if (form == "b")
            {
                ViewBag.b = "active";
                ViewBag.loc = _context.Property.Include(p => p.Type).Where(p => p.Type.form == "For Rent").Take(6).ToList();

            }
            else
            {
                ViewBag.c = "active";
                ViewBag.loc = _context.Property.Include(p => p.Type).Where(p => p.Type.form == "For Sale").Take(6).ToList();

            }
            return View();
        }

    }
}
