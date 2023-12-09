using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicShop.Data;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class MusicLibraryController : Controller
    {
        private readonly MusicShopContext _context;

        public MusicLibraryController(MusicShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Home()
        {
            return View();
        }

        // GET: MusicLibrary
        public async Task<IActionResult> Index()
        {
              return _context.MusicLibrary != null ? 
                          View(await _context.MusicLibrary.ToListAsync()) :
                          Problem("Entity set 'MusicShopContext.MusicLibrary'  is null.");
        }

        // GET: MusicLibrary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MusicLibrary == null)
            {
                return NotFound();
            }

            var musicLibrary = await _context.MusicLibrary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicLibrary == null)
            {
                return NotFound();
            }

            return View(musicLibrary);
        }

        // GET: MusicLibrary/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicLibrary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Genre,ArtistName,SongName,Price")] MusicLibrary musicLibrary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicLibrary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musicLibrary);
        }

        // GET: MusicLibrary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MusicLibrary == null)
            {
                return NotFound();
            }

            var musicLibrary = await _context.MusicLibrary.FindAsync(id);
            if (musicLibrary == null)
            {
                return NotFound();
            }
            return View(musicLibrary);
        }

        // POST: MusicLibrary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Genre,ArtistName,SongName,Price")] MusicLibrary musicLibrary)
        {
            if (id != musicLibrary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicLibrary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicLibraryExists(musicLibrary.Id))
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
            return View(musicLibrary);
        }

        // GET: MusicLibrary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MusicLibrary == null)
            {
                return NotFound();
            }

            var musicLibrary = await _context.MusicLibrary
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicLibrary == null)
            {
                return NotFound();
            }

            return View(musicLibrary);
        }

        // POST: MusicLibrary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MusicLibrary == null)
            {
                return Problem("Entity set 'MusicShopContext.MusicLibrary'  is null.");
            }
            var musicLibrary = await _context.MusicLibrary.FindAsync(id);
            if (musicLibrary != null)
            {
                _context.MusicLibrary.Remove(musicLibrary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicLibraryExists(int id)
        {
          return (_context.MusicLibrary?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
