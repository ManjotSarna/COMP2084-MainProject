using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084_MAINPROJECT.Data;
using COMP2084_MAINPROJECT.Models;
using Microsoft.AspNetCore.Authorization;

namespace COMP2084_MAINPROJECT.Controllers
{
    public class OriginsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OriginsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Origins
        public async Task<IActionResult> Index()
        {
              return _context.Origin != null ? 
                          View(await _context.Origin.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Origin'  is null.");
        }

        [Authorize]
        // GET: Origins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Origin == null)
            {
                return NotFound();
            }

            var origin = await _context.Origin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (origin == null)
            {
                return NotFound();
            }
            var recipes = _context.Recipe.Where(x => x.OriginId == id).OrderBy(recipe => recipe.Origin);

            var viewModel = new OriginViewModel()
            {
                Name = origin.Name,
                Id = origin.Id,
                Recipes = recipes.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        // GET: Origins/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Origins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Origin origin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(origin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(origin);
        }
       
        [Authorize]
        // GET: Origins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Origin == null)
            {
                return NotFound();
            }

            var origin = await _context.Origin.FindAsync(id);
            if (origin == null)
            {
                return NotFound();
            }
            return View(origin);
        }

        // POST: Origins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Origin origin)
        {
            if (id != origin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(origin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OriginExists(origin.Id))
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
            return View(origin);
        }

        [Authorize]
        // GET: Origins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Origin == null)
            {
                return NotFound();
            }

            var origin = await _context.Origin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (origin == null)
            {
                return NotFound();
            }

            return View(origin);
        }

        // POST: Origins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Origin == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Origin'  is null.");
            }
            var origin = await _context.Origin.FindAsync(id);
            if (origin != null)
            {
                _context.Origin.Remove(origin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OriginExists(int id)
        {
          return (_context.Origin?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

    public class OriginViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
