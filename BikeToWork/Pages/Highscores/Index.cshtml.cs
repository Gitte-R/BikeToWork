using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeToWork.Data;
using BikeToWork.Data.Models;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;

namespace BikeToWork.Pages.Highscores
{
    public class IndexModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public IndexModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BikeRide> BikeRide { get;set; } = default!;

        public async Task OnGetAsync() 
        {
            if (_context.BikeRides != null)
            {
                BikeRide = await _context.BikeRides.ToListAsync();
            }
        }
    }
}
