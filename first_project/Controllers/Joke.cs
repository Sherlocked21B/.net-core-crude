using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using first_project.Models;

namespace first_project.Controllers.Joke
{
    public class Joke : Controller
    {
        private readonly jokesDbContext _context;

        public Joke(jokesDbContext context)
        {
            _context = context;
        }

        // GET: Joke
        public async Task<IActionResult> Index()
        {
            return View(await _context.jokes.ToListAsync());
        }

        // GET: Joke/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // GET: Joke/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Joke/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,jokeQuestion,jokeAnswer")] jokes jokes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jokes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokes);
        }

        // GET: Joke/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes.FindAsync(id);
            if (jokes == null)
            {
                return NotFound();
            }
            return View(jokes);
        }

        // POST: Joke/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,jokeQuestion,jokeAnswer")] jokes jokes)
        {
            if (id != jokes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jokes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!jokesExists(jokes.Id))
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
            return View(jokes);
        }

        // GET: Joke/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokes = await _context.jokes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jokes == null)
            {
                return NotFound();
            }

            return View(jokes);
        }

        // POST: Joke/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var jokes = await _context.jokes.FindAsync(id);
            _context.jokes.Remove(jokes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool jokesExists(string id)
        {
            return _context.jokes.Any(e => e.Id == id);
        }
    }
}
