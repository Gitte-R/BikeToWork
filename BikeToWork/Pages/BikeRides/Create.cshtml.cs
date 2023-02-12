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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BikeRide BikeRide { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
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
