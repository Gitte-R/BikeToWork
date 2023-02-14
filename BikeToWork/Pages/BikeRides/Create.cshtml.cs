using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BikeToWork.Data;
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

        public IList<Data.Models.Participant> listOfParticipants { get; set; }
      

        public IActionResult OnGet()
        {
            listOfParticipants = _context.Participants.ToList();
            return Page();
        }


        [BindProperty]
        public BikeRide bikeRide { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BikeRides.Add(bikeRide);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}


//Når cyeklturen er større end 255 så fejler programmet.
//Lave en validering i formen så den allerede der sender en besked til brugeren om at km ikke kan være større end 255 km.
// brug asp-validation til dette.