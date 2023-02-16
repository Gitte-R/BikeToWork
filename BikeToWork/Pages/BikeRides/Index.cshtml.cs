using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public ViewBikeRide ViewBikeRide { get; set; }
        public IList<BikeRide> ListOfBikeRides { get; set; }
        public IList<ViewBikeRide> ListOfViewBikeRides { get; set; }
        public IList<Data.Models.Participant> ListOfParticipants { get; set; }
        public IQueryable<ViewBikeRide> ViewBikeRidesQueryable { get; set; }


        public IList<ViewBikeRide> ConvertToViewBikeRide()
        {
            ListOfParticipants = _context.Participants.ToList();
            ListOfViewBikeRides = new List<ViewBikeRide>();


            foreach (var bikeRide in ListOfBikeRides)
            {
                ViewBikeRide = new ViewBikeRide()
                {
                    Id = bikeRide.Id,
                    Date = bikeRide.Date,
                    ParticipantId = bikeRide.ParticipantId,
                    Distance = bikeRide.Distance
                };

                for (int i = 0; i < ListOfParticipants.Count; i++)
                {
                    if (bikeRide.ParticipantId == ListOfParticipants[i].Id)
                    {
                        ViewBikeRide.FullName = ListOfParticipants[i].FirstName + " " + ListOfParticipants[i].LastName;
                    }
                }

                ListOfViewBikeRides.Add(ViewBikeRide);
            }
            return ListOfViewBikeRides;
        }

        public async Task OnGetAsync(string filter)
        {
            if (_context.BikeRides != null)
            {
                ListOfBikeRides = await _context.BikeRides.ToListAsync();
            }

            ViewBikeRidesQueryable = ConvertToViewBikeRide().AsQueryable();

            #region Filtering
            if (!String.IsNullOrEmpty(filter))
            {
                ViewBikeRidesQueryable = ViewBikeRidesQueryable.Distinct().Where(s => s.FullName.Equals(filter));
            }
            else
            {
                RedirectToAction("List");
            }
            #endregion
        }
    }
}

