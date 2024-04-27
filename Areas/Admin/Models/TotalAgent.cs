using DataHome.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestVersion.Areas.Admin.Models
{
    public class TotalAgent : ViewComponent
    {
        private readonly DataHomeContext _context;
        public TotalAgent(DataHomeContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var totalAgents = _context.Agent.FromSql($"select * from Agent")
                .Distinct()
                .Count();
            return View( totalAgents );
        }
    }
}
