using BikeToWork.Data.Models;
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

        public ViewParticipant ViewParticipant { get; set; }            
        public IList<BikeClassEnum> ListOfBikeClasses { get; set; }
        public IList<BikeRide> ListOfBikeRides { get; set; }
        public IList<BikeRide> ListOfParticipantBikeRides { get; set; }
        public IList<Data.Models.Participant> ListOfParticipants { get;set; }
        public IList<ViewParticipant> ListOfViewParticipant { get; set; }
        public IQueryable<ViewParticipant> ViewParticipantQueryable { get; set; }

        public void ConvertToViewParticipant()
        {
            ListOfViewParticipant = new List<ViewParticipant>();

            foreach (var participant in ListOfParticipants)
            {
                ViewParticipant = new ViewParticipant()
                {
                    Id = participant.Id,
                    FirstName = participant.FirstName,
                    LastName = participant.LastName,
                    Team = participant.Team,
                    BikeClass = participant.BikeClass,
                };

                ViewParticipant.AllBikeRides = GetAllBikeRidesById(ViewParticipant.Id);
                ViewParticipant.TotalBikeRides = ViewParticipant.AllBikeRides.Count;
                ViewParticipant.TotalDistance = GetTotalDistance(ViewParticipant);
                ViewParticipant.AverageDistance = GetAverageDistance(ViewParticipant);
                ListOfViewParticipant.Add(ViewParticipant);
            }
        }
        public IList<BikeRide> GetAllBikeRidesById(int id)
        {
            ListOfBikeRides = _context.BikeRides.ToList();
            ListOfParticipantBikeRides = new List<BikeRide>();

            foreach (var bikeRide in ListOfBikeRides)
            {
                if (bikeRide.ParticipantId == id)
                {
                    ListOfParticipantBikeRides.Add(bikeRide);
                }
            }
            return ListOfParticipantBikeRides;
        }
        public decimal GetAverageDistance(ViewParticipant viewParticipant)
        {
            if (viewParticipant.TotalBikeRides != 0)
            {
                viewParticipant.AverageDistance = (decimal)viewParticipant.TotalDistance / viewParticipant.TotalBikeRides;
            }

            viewParticipant.AverageDistance = Math.Round(viewParticipant.AverageDistance, 2);
            return viewParticipant.AverageDistance;
        }
        public int GetTotalDistance(ViewParticipant viewParticipant)
        {
            int totalDistance = 0;

            foreach (var bikeRide in viewParticipant.AllBikeRides)
            {
                totalDistance += bikeRide.Distance;
            }

            return totalDistance;
        }

        public async Task OnGetAsync(string filter)
        {
            if (_context.Participants != null)
            {
                ListOfParticipants = await _context.Participants.ToListAsync();
            }

            ListOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));

            ConvertToViewParticipant();

            #region Filter
            if (!String.IsNullOrEmpty(filter))
            {
                ListOfViewParticipant = ListOfViewParticipant.Where(s => s.BikeClass.ToString().Equals(filter)).ToList();
            }
            #endregion
        }
    }
}