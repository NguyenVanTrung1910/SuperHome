using DataHome.Data;
using Microsoft.AspNetCore.Mvc;

namespace TestVersion.Areas.Admin.Models
{
    public class TotalType : ViewComponent
    {
        private readonly DataHomeContext _context;
        public TotalType(DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var totalType = _context.Type.Distinct().Count();
            return View(totalType);
        }
    }
}
