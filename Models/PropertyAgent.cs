using DataHome.Data;
using Microsoft.AspNetCore.Mvc;

namespace TestVersion.Models
{
    public class PropertyAgent : ViewComponent
    {
        public readonly DataHomeContext _context;
        public PropertyAgent (DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Agent.Take(4).ToList());
        }
    }
}
