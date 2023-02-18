using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public IList<BikeClassEnum> ListOfBikeClasses { get; set; }
        [BindProperty]
        public Data.Models.Participant Participant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ListOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));

            if (id == null || _context.Participants == null)
            {
                return NotFound();
            }

            var participant =  await _context.Participants.FirstOrDefaultAsync(m => m.Id == id);
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
            ListOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));

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
                if (!ParticipantExists(Participant.Id))
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
          return _context.Participants.Any(e => e.Id == id);
        }
    }
}
