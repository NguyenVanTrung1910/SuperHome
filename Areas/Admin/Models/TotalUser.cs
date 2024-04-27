using DataHome.Data;
using Microsoft.AspNetCore.Mvc;

namespace TestVersion.Areas.Admin.Models
{
    public class TotalUser :ViewComponent
    {
        private readonly DataHomeContext _context;
        public TotalUser(DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.User.ToList());
        }
    }
}
