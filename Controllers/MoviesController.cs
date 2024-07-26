// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        // _context: store context of database
        private readonly MvcMovieContext _context;

        // constructor receive an argu type MvcMovieContext and assign to
        // context
        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // Task: Represents an asynchronous operation that can return a value
        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // more about _context in Data/MvcMovieContext
            if (_context.Movie == null)
            {
                // concise way to return structured error information from
                // ASP.NET MVC controllers
                return Problem("Entity set 'MvcMovieContext.Movie' is null.");
            }

            // use LINQ to get list of genres
            // IQueryable: Provides functionality to evaluate queries against a
            // specific data source wherein the type of the data is known
            /*
               from m
               in _context.Movie
               order by m.Genre
               select m.Genre

               => return a list of all genres
            */
            IQueryable<string> genreQuery = from m in _context.Movie orderby m.Genre select m.Genre;

            /*
               from m
               in _context.Movie
               select m

               => return a list of all movies
            */
            var movies = from m in _context.Movie select m;

            // if the searchString query is provided (from a search input)
            if (!String.IsNullOrEmpty(searchString))
            {
                // filter movies if title contain search string query
                movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
            }

            // if the movieGenre query is provided (from a dropdown select)
            if (!string.IsNullOrEmpty(movieGenre))
            {
                // filter movies if movie's genre match the genre query
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            // init an object MovieGenreViewModel contains 2 properties
            var movieGenreVM = new MovieGenreViewModel
            {
                // Genres prop is a SelectList type
                // SelectList: Represents a list that lets users select a single
                // item\. This class is typically rendered as an HTML <select>
                // element with the specified collection of `SelectListItem`
                // objects
                // the genreQuery.Distinct().ToListAsync()) execute LINQ to get
                // a list of Genres, remove duplicate and turn it into a SelectList
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                // turn movies into a normal List
                // use await to ensure that any asynchronous operations have completed before calling another method on this context
                // ToListAsync return a Task that represent the async operation
                // the Task result contain a List<T> that contain an element
                // from the input sequence
                Movies = await movies.ToListAsync()
            };

            // pass that new model to the View
            return View(movieGenreVM);
        }

        // // POST: Movies
        // [HttpPost]
        // public string Index(string searchString, bool notUsed)
        // {
        //     return "From [HttpPost]Index: filter on " + searchString;
        // }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie
        )
        {
            // check whether the movie has any validation errors to save in database
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // if there is errors the Create method redisplay the form
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie
        )
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
