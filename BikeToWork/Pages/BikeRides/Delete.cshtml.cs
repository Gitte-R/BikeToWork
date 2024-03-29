﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeToWork.Data.Models;

namespace BikeToWork.Pages.BikeRides
{
    public class DeleteModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public DeleteModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<Data.Models.Participant> ListOfParticipants { get; set; }

        [BindProperty]
        public BikeRide BikeRide { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ListOfParticipants = _context.Participants.ToList();

            if (id == null || _context.BikeRides == null)
            {
                return NotFound();
            }

            var bikeride = await _context.BikeRides.FirstOrDefaultAsync(m => m.Id == id);

            if (bikeride == null)
            {
                return NotFound();
            }
            else 
            {
                BikeRide = bikeride;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            ListOfParticipants = _context.Participants.ToList();

            if (id == null || _context.BikeRides == null)
            {
                return NotFound();
            }
            var bikeride = await _context.BikeRides.FindAsync(id);

            if (bikeride != null)
            {
                BikeRide = bikeride;
                _context.BikeRides.Remove(BikeRide);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
