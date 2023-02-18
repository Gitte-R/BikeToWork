using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BikeToWork.Data.Models;

namespace BikeToWork.Pages.BikeRides
{
    public class CreateModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public CreateModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }
      
        public IList<Data.Models.Participant> ListOfParticipants { get; set; }

        public IActionResult OnGet()
        {
            ListOfParticipants = _context.Participants.ToList();
            return Page();
        }

        [BindProperty]
        public BikeRide BikeRide { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ListOfParticipants = _context.Participants.ToList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BikeRides.Add(BikeRide);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}