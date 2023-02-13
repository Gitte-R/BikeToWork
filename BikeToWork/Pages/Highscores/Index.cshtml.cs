using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeToWork.Data;
using BikeToWork.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BikeToWork.Pages.Highscores
{
    public class IndexModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public IndexModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<BikeRide> listOfAllBikeRides { get; set; }
        public IList<BikeRide> listOfParticipantBikeRides { get; set; }
        public Data.Models.Participant participant { get; set; }
        public IList<BikeClassEnum> listOfBikeClasses { get; set; }
        public IList<Data.Models.Participant> listOfParticipants { get; set; } = default!;
        public IList<ViewParticipant> listOfViewParticipant { get; set; }
        public ViewParticipant viewParticipant { get; set; }
        public IList<BikeRide>? listOfAllBikeRidesById { get; set; }
        public IList<ViewParticipant> viewParticipantsSortedByTotalDistance { get; set; }
        public IList<ViewParticipant> viewParticipantsSortedByNumberOfBikeRides { get; set; }
        public string? currentFilter { get; set; }


        public void CreateViewParticipant()
        {

        }

        public IList<BikeRide> GetAllBikeRidesById(int _id, IList<BikeRide> _listOfBikeRides)
        {
            listOfParticipantBikeRides = new List<BikeRide>();

            foreach (var bikeRide in _listOfBikeRides)
            {
                if (bikeRide.participantId == _id)
                {
                    listOfParticipantBikeRides.Add(bikeRide);
                }
            }

            return listOfParticipantBikeRides;
        }

        public int GetTotalDistance(int _viewParticipantId)
        {
            int totalDistance = 0;

            foreach (var bikeRide in listOfAllBikeRides)
            {
                if (bikeRide.participantId == _viewParticipantId)
                {
                    totalDistance = totalDistance + bikeRide.Distance;
                }
            }
            return totalDistance;
        }

        public BikeClassEnum GetBikeClass()
        {
            return BikeClassEnum.Electricbike;
        }


        public async Task OnGetAsync(string sortOrder, string searchId)
        {
            listOfAllBikeRides = _context.BikeRides.ToList();
            listOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));
            listOfViewParticipant = new List<ViewParticipant>();

            currentFilter = searchId;

            if (_context.Participants != null)
            {
                listOfParticipants = await _context.Participants.ToListAsync();
            }
            else
            {
                return;
            }


            //create viewParticipant()
            foreach (var participant in listOfParticipants)
            {
                viewParticipant = new ViewParticipant();

                viewParticipant.id = participant.id;
                viewParticipant.firstName = participant.firstName;
                viewParticipant.lastName = participant.lastName;
                viewParticipant.team = participant.team;
                viewParticipant.bikeClass = participant.bikeClass;

                listOfAllBikeRidesById = GetAllBikeRidesById(participant.id, listOfAllBikeRides);
                viewParticipant.allBikeRides = listOfAllBikeRidesById.ToList();
                viewParticipant.numberOfBikeRides = listOfAllBikeRidesById.Count;

                viewParticipant.totalDistance = GetTotalDistance(viewParticipant.id);
                listOfViewParticipant.Add(viewParticipant);
            }
           
             IQueryable<ViewParticipant> list = listOfViewParticipant.AsQueryable();
            if (!String.IsNullOrEmpty(currentFilter))
            {

                list = list.Where(s => s.bikeClass.ToString().Equals(searchId));
                listOfViewParticipant = list.ToList();
            }
            else
            {
                //RedirectToAction("Index", "Highscores");
                RedirectToAction("List");
            }










            //viewParticipantsSortedByTotalDistance = new List<ViewParticipant>();
            //viewParticipantsSortedByNumberOfBikeRides = new List<ViewParticipant>();

            //viewParticipantsSortedByTotalDistance = listOfViewParticipant.OrderByDescending(x => x.totalDistance).ToList();
            //viewParticipantsSortedByNumberOfBikeRides = listOfViewParticipant.OrderByDescending(x => x.numberOfBikeRides).ToList();
        }
    }
}
// test hvis databasen er tom. Er return tilstrækkeligt?
// ved alle klasser i dropdown, redirect til hovedside

