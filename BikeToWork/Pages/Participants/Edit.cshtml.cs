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

namespace BikeToWork.Pages.Participant
{
    public class EditModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public EditModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Models.Participant Participant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Participants == null)
            {
                return NotFound();
            }

            var participant =  await _context.Participants.FirstOrDefaultAsync(m => m.id == id);
            if (participant == null)
            {
                return NotFound();
            }
            Participant = participant;
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

            _context.Attach(Participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(Participant.id))
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

        private bool ParticipantExists(int id)
        {
          return _context.Participants.Any(e => e.id == id);
        }
    }
}
