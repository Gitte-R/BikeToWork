using System;
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
    public class IndexModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public IndexModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewBikeRide viewBikeRide { get; set; }
        public IList<BikeRide> listOfBikeRides { get; set; }
        public IList<ViewBikeRide> listOfViewBikeRides { get; set; }
        public IList<Data.Models.Participant> listOfParticipants { get; set; }
        public IQueryable<ViewBikeRide> viewBikeRidesQueryable { get; set; }


        public IList<ViewBikeRide> ConvertToViewBikeRide()
        {
            listOfParticipants = _context.Participants.ToList();
            listOfViewBikeRides = new List<ViewBikeRide>();


            foreach (var bikeRide in listOfBikeRides)
            {
                viewBikeRide = new ViewBikeRide()
                {
                    id = bikeRide.id,
                    date = bikeRide.date,
                    participantId = bikeRide.participantId,
                    Distance = bikeRide.Distance
                };

                for (int i = 0; i < listOfParticipants.Count; i++)
                {
                    if (bikeRide.participantId == listOfParticipants[i].id)
                    {
                        viewBikeRide.FullName = listOfParticipants[i].firstName + " " + listOfParticipants[i].lastName;
                    }
                }

                listOfViewBikeRides.Add(viewBikeRide);
            }
            return listOfViewBikeRides;
        }

        public async Task OnGetAsync(string filter)
        {
            if (_context.BikeRides != null)
            {
                listOfBikeRides = await _context.BikeRides.ToListAsync();
            }

            viewBikeRidesQueryable = ConvertToViewBikeRide().AsQueryable();

            #region Filtering
            if (!String.IsNullOrEmpty(filter))
            {
                viewBikeRidesQueryable = viewBikeRidesQueryable.Distinct().Where(s => s.FullName.Equals(filter));
            }
            else
            {
                RedirectToAction("List");
            }
            #endregion
        }
    }
}

