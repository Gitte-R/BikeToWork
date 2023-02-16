using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeToWork.Data.Models;

namespace BikeToWork.Pages.Scoreboard
{
    public class IndexModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public IndexModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public ViewParticipant ViewParticipant { get; set; }
        public IList<BikeRide> ListOfBikeRides { get; set; }
        public IList<BikeRide> ListOfParticipantBikeRides { get; set; }
        public IList<Data.Models.Participant> ListOfParticipants { get; set; }
        public IList<ViewParticipant> ListOfViewParticipant { get; set; }

        public void ConvertToViewParticipant()
        {
            ListOfViewParticipant = new List<ViewParticipant>();

            foreach (var participant in ListOfParticipants)
            {
                ViewParticipant = new ViewParticipant()
                {
                    id = participant.id,
                    firstName = participant.firstName,
                    lastName = participant.lastName,
                    team = participant.team,
                    bikeClass = participant.bikeClass,
                };

                ViewParticipant.allBikeRides = GetAllBikeRidesById(ViewParticipant.id);
                ViewParticipant.TotalBikeRides = ViewParticipant.allBikeRides.Count;
                ViewParticipant.TotalDistance = GetTotalDistance(ViewParticipant);
                ViewParticipant.AverageDistance = GetAverageDistance(ViewParticipant);
                ListOfViewParticipant.Add(ViewParticipant);
            }
        }
        public IList<BikeRide> GetAllBikeRidesById(int _id)
        {
            ListOfBikeRides = _context.BikeRides.ToList();
            ListOfParticipantBikeRides = new List<BikeRide>();

            foreach (var bikeRide in ListOfBikeRides)
            {
                if (bikeRide.participantId == _id)
                {
                    ListOfParticipantBikeRides.Add(bikeRide);
                }
            }
            return ListOfParticipantBikeRides;
        }
        public decimal GetAverageDistance(ViewParticipant _viewParticipant)
        {
            if (_viewParticipant.TotalBikeRides != 0)
            {
                _viewParticipant.AverageDistance = (decimal)_viewParticipant.TotalDistance / _viewParticipant.TotalBikeRides;
            }

            _viewParticipant.AverageDistance = Math.Round(_viewParticipant.AverageDistance, 2);
            return _viewParticipant.AverageDistance;
        }
        public int GetTotalDistance(ViewParticipant _viewParticipant)
        {
            int totalDistance = 0;

            foreach (var bikeRide in _viewParticipant.allBikeRides)
            {
                totalDistance += bikeRide.Distance;
            }

            return totalDistance;
        }

        public async Task OnGetAsync(string sortOrder)
        {
            if (_context.Participants != null)
            {
                ListOfParticipants = await _context.Participants.ToListAsync();
            }
            else
            {
                return;
            }

            ConvertToViewParticipant();

            #region Sorting
            switch (sortOrder)
            {
                case "totalDistance":
                    ListOfViewParticipant = ListOfViewParticipant.OrderByDescending(x => x.TotalDistance).ToList();
                    break;
                case "numberOfBikeRides":
                    ListOfViewParticipant = ListOfViewParticipant.OrderByDescending(x => x.TotalBikeRides).ToList();
                    break;
                case "averageDistance":
                    ListOfViewParticipant = ListOfViewParticipant.OrderByDescending(x => x.AverageDistance).ToList();
                    break;
                default:
                    ListOfViewParticipant = ListOfViewParticipant.OrderByDescending(x => x.TotalDistance).ToList();
                    break;
            }
            #endregion
        }
    }
}




// hvad hvis listen med bikerides er tom? allBikeRides = _context.BikeRides.ToList();
