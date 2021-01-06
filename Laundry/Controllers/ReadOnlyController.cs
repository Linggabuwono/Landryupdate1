using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laundry.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laundry.Controllers
{
    public class ReadOnlyController : Controller
    {
        private readonly LaundryContext _context;

        public ReadOnlyController(LaundryContext context)
        {
            _context = context;
        }

        // GET: Pesanans
        public async Task<IActionResult> Index(string ktsd, string searchString, string currentFilter, int? pageNumber, string sortOrder)
        {
            var menu = from m in _context.Pesanan.Include(k => k.IdPesanan).Include(p => p.NamaCustomer).Include(p => p.Tipe).Include(p => p.BeratTotal).Include(p => p.HrgSatuan).Include(p => p.HrgTotal).Include(p => p.TglPesanan) select m;

            //untuk search data
            if (!string.IsNullOrEmpty(searchString))
            {
                menu = menu.Where(s => s.IdPesanan.ToString().Contains(searchString) || s.NamaCustomer.Contains(searchString) || s.Tipe.Contains(searchString) || s.BeratTotal.ToString().Contains(searchString) || s.HrgSatuan.ToString().Contains(searchString) || s.HrgTotal.ToString().Contains(searchString) || s.TglPesanan.ToString().Contains(searchString));
            }
            return View(await _context.Pesanan.ToListAsync());
        }

        // GET: Pesanans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesanan = await _context.Pesanan
                .FirstOrDefaultAsync(m => m.IdPesanan == id);
            if (pesanan == null)
            {
                return NotFound();
            }

            return View(pesanan);
        }

        // GET: Pesanans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pesanans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPesanan,NamaCustomer,Tipe,BeratTotal,HrgSatuan,HrgTotal,TglPesanan")] Pesanan pesanan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pesanan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pesanan);
        }

        // GET: Pesanans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesanan = await _context.Pesanan.FindAsync(id);
            if (pesanan == null)
            {
                return NotFound();
            }
            return View(pesanan);
        }

        // POST: Pesanans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPesanan,NamaCustomer,Tipe,BeratTotal,HrgSatuan,HrgTotal,TglPesanan")] Pesanan pesanan)
        {
            if (id != pesanan.IdPesanan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesanan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesananExists(pesanan.IdPesanan))
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
            return View(pesanan);
        }

        // GET: Pesanans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesanan = await _context.Pesanan
                .FirstOrDefaultAsync(m => m.IdPesanan == id);
            if (pesanan == null)
            {
                return NotFound();
            }

            return View(pesanan);
        }

        // POST: Pesanans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pesanan = await _context.Pesanan.FindAsync(id);
            _context.Pesanan.Remove(pesanan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PesananExists(int id)
        {
            return _context.Pesanan.Any(e => e.IdPesanan == id);
        }
    }
}