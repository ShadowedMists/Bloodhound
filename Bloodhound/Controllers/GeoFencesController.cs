using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bloodhound.Core.Model;

namespace Bloodhound.Controllers
{
    public class GeoFencesController : Controller
    {
        private readonly BloodhoundContext _context;

        public GeoFencesController(BloodhoundContext context)
        {
            _context = context;
        }

        // GET: GeoFences
        public async Task<IActionResult> Index(long? id)
        {
            var offender = await _context.Offenders.FindAsync(id);
            if (offender == null)
            {
                return NotFound();
            }
            ViewData["Offender"] = offender;

            var bloodhoundContext = _context.OffenderGeoFences.Include(o => o.GeoFenceType).Where(x => x.OffenderId == offender.OffenderId);
            return View(await bloodhoundContext.ToListAsync());
        }

        // GET: GeoFences/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offenderGeoFence = await _context.OffenderGeoFences
                .Include(o => o.Offender)
                .Include(o => o.GeoFenceType)
                .FirstOrDefaultAsync(m => m.OffenderGeoFenceId == id);
            if (offenderGeoFence == null)
            {
                return NotFound();
            }

            return View(offenderGeoFence);
        }

        // GET: GeoFences/Create
        public IActionResult Create(long? id)
        {
            var offender = _context.Offenders.FirstOrDefault(x => x.OffenderId == id);
            if (offender == null)
            {
                return NotFound();
            }
            ViewData["Offender"] = offender;
            ViewData["GeoFenceTypeId"] = this._context.GeoFenceTypes.ToList().Select(x => new SelectListItem() { Text = x.GeoFenceTypeName, Value = x.GeoFenceTypeId.ToString() }).ToList();

            return View(new OffenderGeoFence() { OffenderId = offender.OffenderId, GeoFenceTypeId = 1 });
        }

        // POST: GeoFences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OffenderGeoFenceId,OffenderId,GeoFenceName,Address,GeoFenceTypeId,NorthEastLatitude,NorthEastLongitude,SouthWestLatitude,SouthWestLongitude")] OffenderGeoFence offenderGeoFence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offenderGeoFence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {id = offenderGeoFence.OffenderId });
            }

            var offender = _context.Offenders.FirstOrDefault(x => x.OffenderId == offenderGeoFence.OffenderId);
            if (offender == null)
            {
                return NotFound();
            }
            ViewData["Offender"] = offender;
            ViewData["GeoFenceTypeId"] = this._context.GeoFenceTypes.ToList().Select(x => new SelectListItem() { Text = x.GeoFenceTypeName, Value = x.GeoFenceTypeId.ToString() }).ToList();

            return View(offenderGeoFence);
        }

        // GET: GeoFences/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offenderGeoFence = await _context.OffenderGeoFences.FindAsync(id);
            if (offenderGeoFence == null)
            {
                return NotFound();
            }

            var offender = _context.Offenders.FirstOrDefault(x => x.OffenderId == offenderGeoFence.OffenderId);
            if (offender == null)
            {
                return NotFound();
            }
            ViewData["Offender"] = offender;
            ViewData["GeoFenceTypeId"] = this._context.GeoFenceTypes.ToList().Select(x => new SelectListItem() { Text = x.GeoFenceTypeName, Value = x.GeoFenceTypeId.ToString() }).ToList();


            return View(offenderGeoFence);
        }

        // POST: GeoFences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OffenderGeoFenceId,OffenderId,GeoFenceName,Address,GeoFenceTypeId,NorthEastLatitude,NorthEastLongitude,SouthWestLatitude,SouthWestLongitude")] OffenderGeoFence offenderGeoFence)
        {
            if (id != offenderGeoFence.OffenderGeoFenceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offenderGeoFence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffenderGeoFenceExists(offenderGeoFence.OffenderGeoFenceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = offenderGeoFence.OffenderId });
            }
            ViewData["OffenderId"] = new SelectList(_context.Offenders, "OffenderId", "OffenderName", offenderGeoFence.OffenderId);
            return View(offenderGeoFence);
        }

        // GET: GeoFences/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offenderGeoFence = await _context.OffenderGeoFences
                .Include(o => o.Offender)
                .Include(o => o.GeoFenceType)
                .FirstOrDefaultAsync(m => m.OffenderGeoFenceId == id);
            if (offenderGeoFence == null)
            {
                return NotFound();
            }

            return View(offenderGeoFence);
        }

        // POST: GeoFences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var offenderGeoFence = await _context.OffenderGeoFences.FindAsync(id);
            _context.p_OffenderGeoFence_Delete(offenderGeoFence.OffenderGeoFenceId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = offenderGeoFence.OffenderId });
        }

        private bool OffenderGeoFenceExists(long id)
        {
            return _context.OffenderGeoFences.Any(e => e.OffenderGeoFenceId == id);
        }
    }
}
