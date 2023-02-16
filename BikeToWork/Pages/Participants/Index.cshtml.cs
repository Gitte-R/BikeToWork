using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeToWork.Pages.Participant
{
    public class IndexModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public IndexModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Models.Participant> ListOfParticipants { get;set; } 

        public async Task OnGetAsync()
        {
            if (_context.Participants != null)
            {
                ListOfParticipants = await _context.Participants.ToListAsync();
            }
        }
    }
}

//Hvorfor er sidst række højere end de andre?
//Ændre alle mine properties til at starte med stort
// ved alle klasser i dropdown, redirect til hovedside.//redirekte til Index? RedirectToAction("List"); 
