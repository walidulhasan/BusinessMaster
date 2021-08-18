using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessMaster.Models;

namespace BusinessMaster.Controllers
{
    public class ServicesNamesController : Controller
    {
        private readonly ProductDbContext _context;

        public ServicesNamesController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: ServicesNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.servicesNames.ToListAsync());
        }

        // GET: ServicesNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesName = await _context.servicesNames
                .FirstOrDefaultAsync(m => m.ServicesNameId == id);
            if (servicesName == null)
            {
                return NotFound();
            }

            return View(servicesName);
        }

        // GET: ServicesNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServicesNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicesNameId,ServiceName")] ServicesName servicesName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicesName);
                await _context.SaveChangesAsync();
                return PartialView("_success");
               
            }
            else
            {
                if (servicesName.ServiceName==null)
                {
                    return PartialView("__Faild");
                }
            }
            //ModelState.Clear();
            return View(servicesName);
        }

        // GET: ServicesNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesName = await _context.servicesNames.FindAsync(id);
            if (servicesName == null)
            {
                return NotFound();
            }
            return View(servicesName);
        }

        // POST: ServicesNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicesNameId,ServiceName")] ServicesName servicesName)
        {
            if (id != servicesName.ServicesNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicesName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesNameExists(servicesName.ServicesNameId))
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
            return View(servicesName);
        }

        // GET: ServicesNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicesName = await _context.servicesNames
                .FirstOrDefaultAsync(m => m.ServicesNameId == id);
            if (servicesName == null)
            {
                return NotFound();
            }

            return View(servicesName);
        }

        // POST: ServicesNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicesName = await _context.servicesNames.FindAsync(id);
            _context.servicesNames.Remove(servicesName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesNameExists(int id)
        {
            return _context.servicesNames.Any(e => e.ServicesNameId == id);
        }
    }
}
