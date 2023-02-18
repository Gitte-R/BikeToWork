using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BikeToWork.Data.Models;

namespace BikeToWork.Pages.Participant
{
    public class CreateModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public CreateModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BikeClassEnum> ListOfBikeClasses { get; set; }

        public IActionResult OnGet()
        {
            ListOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));

            return Page();
        }

        [BindProperty]
        public Data.Models.Participant Participant { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ListOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Participants.Add(Participant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}