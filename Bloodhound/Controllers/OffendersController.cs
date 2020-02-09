using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bloodhound.Core.Model;
using Bloodhound.Models;
using Bloodhound.Core;

namespace Bloodhound.Controllers
{
    public class OffendersController : Controller
    {
        private readonly BloodhoundContext _context;

        public OffendersController(BloodhoundContext context)
        {
            _context = context;
        }

        // GET: Offenders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Offenders.ToListAsync());
        }

        // GET: Offenders/Details/5
        public IActionResult Details(OffenderDetailModel model)
        {
            try
            {
                model.Initialize(this._context);
                 return View(model);
            }
            catch(EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Offenders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offenders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OffenderId,OffenderName,OffenderSummary")] Offender offender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(offender);
        }

        // GET: Offenders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offender = await _context.Offenders.FindAsync(id);
            if (offender == null)
            {
                return NotFound();
            }
            return View(offender);
        }

        // POST: Offenders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OffenderId,OffenderName,OffenderSummary")] Offender offender)
        {
            if (id != offender.OffenderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffenderExists(offender.OffenderId))
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
            return View(offender);
        }

        // GET: Offenders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offender = await _context.Offenders
                .FirstOrDefaultAsync(m => m.OffenderId == id);
            if (offender == null)
            {
                return NotFound();
            }

            return View(offender);
        }

        // POST: Offenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var offender = await _context.Offenders.FindAsync(id);
            _context.p_Offender_Delete(offender.OffenderId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffenderExists(long id)
        {
            return _context.Offenders.Any(e => e.OffenderId == id);
        }
    }
}
