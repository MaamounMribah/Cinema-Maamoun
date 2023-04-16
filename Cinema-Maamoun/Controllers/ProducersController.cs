using Cinema_Maamoun.Models.Cinema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Maamoun.Controllers
{
    public class ProducersController : Controller
    {
        CinemaDbContext _context;
        public ProducersController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: ProducersController
        public ActionResult Index()
        {
            return View(_context.Producers);
        }

        // GET: ProducersController/Details/5
        public ActionResult Details(int id)
        {
            Producer p = _context.Producers.Find(id);
            return View(p);
        }

        // GET: ProducersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producer producer)
        {
            try
            {
                _context.Producers.Add(producer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Edit/5
        public ActionResult Edit(int id)
        {
            Producer p=_context.Producers.Find(id);
            return View(p);
        }

        // POST: ProducersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Producer producer)
        {
            try
            {
                _context.Producers.Update(producer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProducersController/Delete/5
        public ActionResult Delete(int id)
        { 
            return View();
        }

        // POST: ProducersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Producer producer)
        {
            try
            {
                _context.Producers.Remove(producer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ProdsAndTheirMovies()
        {
            return View(_context.Producers.Include(P=>P.Movies));
        }
        public ActionResult ProdsAndTheirMovies_UsingModel()
        {
            var liste = from prod in _context.Producers 
                        join movie in _context.Movies
                        on prod.Id equals movie.ProducerId
                        select new ProdMovie{
                            pName = prod.Name,
                            pNat = prod.Nationality,
                            mTitle = movie.Title,
                            mGenre = movie.Genre,
                        };

            return View(liste);
        }
        
        public ActionResult MyMovies(int id)
        {
            var liste = from prod in _context.Producers
                        join movie in _context.Movies
                        on prod.Id equals movie.Id
                        where prod.Id == id
                        select new ProdMovie
                        {
                            pName = prod.Name,
                            pNat = prod.Nationality,
                            mTitle = movie.Title,
                            mGenre = movie.Genre,
                        };
            return View(liste);

        }
    }
}
