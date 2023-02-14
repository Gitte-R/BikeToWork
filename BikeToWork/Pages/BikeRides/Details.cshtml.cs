﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeToWork.Data;
using BikeToWork.Data.Models;

namespace BikeToWork.Pages.BikeRides
{
    public class DetailsModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public DetailsModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public BikeRide BikeRide { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BikeRides == null)
            {
                return NotFound();
            }

            var bikeride = await _context.BikeRides.FirstOrDefaultAsync(m => m.id == id);
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
    }
}