using DataHome.Data;
using Microsoft.AspNetCore.Mvc;

namespace TestVersion.Areas.Admin.Models
{
    public class NewProperty : ViewComponent
    {
        private readonly DataHomeContext _context;
        public NewProperty(DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Property.ToList());
        }
    }
}
