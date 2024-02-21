using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practica02NARA.Models;

namespace Practica02NARA.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly Practica02NARABDContext _context;

        public PeliculasController(Practica02NARABDContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
            var practica02NARABDContext = _context.Peliculas.Include(p => p.DirectorPeliculaNavigation);
            return View(await practica02NARABDContext.ToListAsync());
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.DirectorPeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewData["DirectorPelicula"] = new SelectList(_context.Directores, "IdDirector", "NombreDirector");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombrePelicula,Genero,AñoPublicada,DirectorPelicula")] Pelicula pelicula, IFormFile ImagenPelicula)
        {
            //Para Guardar La Imagen En El Objeto Creado
            if (ImagenPelicula != null && ImagenPelicula.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImagenPelicula.CopyToAsync(memoryStream);
                    var ImagenData = memoryStream.ToArray();
                    pelicula.ImagenPelicula = ImagenData;
                }
            }

            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    _context.Add(pelicula);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["DirectorPelicula"] = new SelectList(_context.Directores, "IdDirector", "IdDirector", pelicula.DirectorPelicula);
            //return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["DirectorPelicula"] = new SelectList(_context.Directores, "IdDirector", "NombreDirector", pelicula.DirectorPelicula);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombrePelicula,Genero,AñoPublicada,DirectorPelicula")] Pelicula pelicula, IFormFile ImagenPelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ImagenPelicula != null && ImagenPelicula.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImagenPelicula.CopyToAsync(memoryStream);
                    pelicula.ImagenPelicula = memoryStream.ToArray();
                }
                _context.Update(pelicula);
                await _context.SaveChangesAsync();
            }
            else
            {
                var producFind = await _context.Peliculas.FirstOrDefaultAsync(s => s.Id == pelicula.Id);
                if (producFind?.ImagenPelicula?.Length > 0)
                {
                    pelicula.ImagenPelicula = producFind.ImagenPelicula;
                    producFind.NombrePelicula = pelicula.NombrePelicula;
                    producFind.Genero = pelicula.Genero;
                    producFind.AñoPublicada = pelicula.AñoPublicada;
                    producFind.DirectorPelicula = pelicula.DirectorPelicula;
                    _context.Update(producFind);
                    await _context.SaveChangesAsync();
                }
            }
            try
                {
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            //ViewData["DirectorPelicula"] = new SelectList(_context.Directores, "IdDirector", "IdDirector", pelicula.DirectorPelicula);
            //return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.DirectorPeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'Practica02NARABDContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Peliculas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
