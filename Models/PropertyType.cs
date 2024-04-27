using DataHome.Data;
using Microsoft.AspNetCore.Mvc;

namespace TestVersion.Models
{
    public class PropertyType : ViewComponent
    {
        private readonly DataHomeContext _context;
        public PropertyType (DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.PropertyList = _context.Property.ToList();
            return View(_context.Type.ToList());
        }

    }
}
