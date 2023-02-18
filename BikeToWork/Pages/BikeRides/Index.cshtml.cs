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
        public string DateSort { get; set; }
        public string NameSort { get; set; }
        public string DistanceSort { get; set; }
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

        public async Task OnGetAsync(string sortOrder)
        {
            DateSort = sortOrder == "Date" ? "Date_desc" : "Date";
            NameSort = sortOrder == "FullName" ? "FullName_desc" : "FullName";
            DistanceSort = sortOrder == "Distance" ? "Distance_desc" : "Distance";

            if (_context.BikeRides != null)
            {
                ListOfBikeRides = await _context.BikeRides.ToListAsync();
            }

            ViewBikeRidesQueryable = ConvertToViewBikeRide().AsQueryable();

            #region Column-Sorting
            ViewBikeRidesQueryable = sortOrder switch
            {
                "Date" => ViewBikeRidesQueryable.OrderBy(s => s.Date) 
                                                .ThenBy(s => s.FullName)
                                                .ThenBy(s => s.Distance),
                "Date_desc" => ViewBikeRidesQueryable.OrderByDescending(s => s.Date)
                                                .ThenByDescending(s => s.FullName)
                                                .ThenByDescending(s => s.Distance),
                "FullName" => ViewBikeRidesQueryable.OrderBy(s => s.FullName)
                                                .ThenBy(s => s.Date)
                                                .ThenBy(s => s.Distance),
                "FullName_desc" => ViewBikeRidesQueryable.OrderByDescending(s => s.FullName)
                                                .ThenByDescending(s => s.Date)
                                                .ThenByDescending(s => s.Distance),
                "Distance" => ViewBikeRidesQueryable.OrderBy(s => s.Distance)
                                                .ThenBy(s => s.Date)
                                                .ThenBy(s => s.Distance),
                "Distance_desc" => ViewBikeRidesQueryable.OrderByDescending(s => s.Distance)
                                                .ThenByDescending(s => s.Date)
                                                .ThenByDescending(s => s.Distance),
                _ => ViewBikeRidesQueryable.OrderBy(s => s.Date)
                                                .ThenBy(s => s.FullName)
                                                .ThenBy(s => s.Distance),
            };
            #endregion
        }
    }
}

