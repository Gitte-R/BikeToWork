﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeToWork.Data;
using BikeToWork.Data.Models;

namespace BikeToWork.Pages.Participant
{
    public class DeleteModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public DeleteModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Data.Models.Participant Participant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Participants == null)
            {
                return NotFound();
            }

            var participant = await _context.Participants.FirstOrDefaultAsync(m => m.Id == id);

            if (participant == null)
            {
                return NotFound();
            }
            else 
            {
                Participant = participant;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Participants == null)
            {
                return NotFound();
            }
            var participant = await _context.Participants.FindAsync(id);

            if (participant != null)
            {
                Participant = participant;
                _context.Participants.Remove(Participant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
