using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeToWork.Data;
using BikeToWork.Data.Models;

namespace BikeToWork.Pages.BikeRides
{
    public class EditModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public EditModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BikeRide BikeRide { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BikeRides == null)
            {
                return NotFound();
            }

            var bikeride =  await _context.BikeRides.FirstOrDefaultAsync(m => m.Id == id);
            if (bikeride == null)
            {
                return NotFound();
            }
            BikeRide = bikeride;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BikeRide).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BikeRideExists(BikeRide.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BikeRideExists(int id)
        {
          return _context.BikeRides.Any(e => e.Id == id);
        }
    }
}
