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
    public class PropertiesController : Controller
    {
        private readonly DataHomeContext _context;

        public PropertiesController(DataHomeContext context)
        {
            _context = context;
        }
        // GET: Properties
        public async Task<IActionResult> Index()
        {
            ViewBag.property = "active";
            return _context.Property != null ?
                        View(await _context.Property.Include(p => p.Type).ToListAsync()) :
                        Problem("Entity set 'DataHomeContext.Property'  is null.");
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Property == null)
            {
                return NotFound();
            }

            var @property = await _context.Property.Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Properties/Create
        public IActionResult Create()
        {
            ViewBag.TypeId = new SelectList(_context.Type, "Id", "Name");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,Address,Quantity,Description,Acreage,Bed,Bath,TypeId")] Property @property, IFormFile uploadfile)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileName(uploadfile.FileName);
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", filename);
                property.ImageUrl = filename;

                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    uploadfile.CopyTo(fileStream);
                }
                _context.Add(@property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@property);
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Property == null)
            {
                return NotFound();
            }

            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            ViewBag.TypeId = new SelectList(_context.Type, "Id", "Name");
            return View(@property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,Address,Quantity,Description,Acreage,Bed,Bath,TypeId")] Property @property, IFormFile uploadfile)
        {
            if (id != @property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid ||uploadfile ==null|| uploadfile.Length ==0)
            {
                try
                {
                    if (uploadfile != null && uploadfile.Length > 0)
                    {
                        var filename = Path.GetFileName(uploadfile.FileName);
                        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", filename);
                        property.ImageUrl = filename;
                        
                        using (var fileStream = new FileStream(filepath, FileMode.Create))
                        {
                            uploadfile.CopyTo(fileStream);
                        }
                    }
                    _context.Update(@property);
                        await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(@property.Id))
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
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Property == null)
            {
                return NotFound();
            }

            var @property = await _context.Property
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Property == null)
            {
                return Problem("Entity set 'DataHomeContext.Property'  is null.");
            }
            var @property = await _context.Property.FindAsync(id);
            if (@property != null)
            {
                _context.Property.Remove(@property);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return (_context.Property?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
