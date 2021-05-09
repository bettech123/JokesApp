using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JokeApp.Data;
using JokeApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace JokeApp.Controllers
{
    public class JokeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Joke
        public async Task<IActionResult> Index()
        {
            return View(await _context.JokeModel.ToListAsync());
        }

        // GET: Search Jokes
        [HttpGet]
        public async Task<IActionResult> Index(string JokeSearch)
        {
            ViewData["GetJokeDetails"] = JokeSearch;

            var jokeEquery = from x in _context.JokeModel select x;
            if (!String.IsNullOrEmpty(JokeSearch))
            {
                jokeEquery = jokeEquery.Where(x => x.JokeQuestion.Contains(JokeSearch));
            }
            return View(await jokeEquery.AsNoTracking().ToListAsync());

        }

            // GET: Joke/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokeModel = await _context.JokeModel
                .FirstOrDefaultAsync(m => m.JokeId == id);
            if (jokeModel == null)
            {
                return NotFound();
            }

            return View(jokeModel);
        }

        // GET: Joke/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Joke/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("JokeId,JokeQuestion,JokeAnswer")] JokeModel jokeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jokeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokeModel);
        }

        // GET: Joke/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokeModel = await _context.JokeModel.FindAsync(id);
            if (jokeModel == null)
            {
                return NotFound();
            }
            return View(jokeModel);
        }

        // POST: Joke/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("JokeId,JokeQuestion,JokeAnswer")] JokeModel jokeModel)
        {
            if (id != jokeModel.JokeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jokeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokeModelExists(jokeModel.JokeId))
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
            return View(jokeModel);
        }

        // GET: Joke/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokeModel = await _context.JokeModel
                .FirstOrDefaultAsync(m => m.JokeId == id);
            if (jokeModel == null)
            {
                return NotFound();
            }

            return View(jokeModel);
        }

        // POST: Joke/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jokeModel = await _context.JokeModel.FindAsync(id);
            _context.JokeModel.Remove(jokeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JokeModelExists(int id)
        {
            return _context.JokeModel.Any(e => e.JokeId == id);
        }
    }
}
