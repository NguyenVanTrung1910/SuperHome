using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataHome.Data;
using TestVersion.Models;
using Microsoft.AspNetCore.Authorization;

namespace TestVersion.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AgentsController : Controller
    {
        
        private readonly DataHomeContext _context;

        public AgentsController(DataHomeContext context)
        {
            _context = context;
        }
        
        // GET: Agents
        public async Task<IActionResult> Index()
        {
            ViewBag.agent = "active";
            return _context.Agent != null ?
                        View(await _context.Agent.ToListAsync()) :
                        Problem("Entity set 'ShopHomeContext.Agent'  is null.");
        }

        // GET: Agents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agent == null)
            {
                return NotFound();
            }

            var agent = await _context.Agent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // GET: Agents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Facebook,ImageUrl,Sdt")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agent == null)
            {
                return NotFound();
            }

            var agent = await _context.Agent.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Facebook,ImageUrl,Sdt")] Agent agent, IFormFile FileName)
        {
            if (id != agent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid || FileName == null || FileName.Length == 0)
            {
                try
                {
                    if(FileName !=null && FileName.Length > 0)
                    {
                        var filename = Path.GetFileName(FileName.FileName);
                        var filepath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img", filename);
                        agent.ImageUrl = filename;
                        
                        
                            using (var fileSrteam = new FileStream(filepath, FileMode.Create))
                        {
                            await FileName.CopyToAsync(fileSrteam);
                        }

                        _context.Update(agent);
                        await _context.SaveChangesAsync();
                        
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        // GET: Agents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agent == null)
            {
                return NotFound();
            }

            var agent = await _context.Agent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }

            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agent == null)
            {
                return Problem("Entity set 'ShopHomeContext.Agent'  is null.");
            }
            var agent = await _context.Agent.FindAsync(id);
            if (agent != null)
            {
                _context.Agent.Remove(agent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return (_context.Agent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
