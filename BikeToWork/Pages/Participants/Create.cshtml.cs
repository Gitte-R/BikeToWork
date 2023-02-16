using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BikeToWork.Data;
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

        public IList<BikeClassEnum> listOfBikeClasses { get; set; }


        public IActionResult OnGet()
        {
            listOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));

            return Page();
        }

        [BindProperty]
        public Data.Models.Participant participant { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            listOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

//få dropdown til at blive på valgt item 