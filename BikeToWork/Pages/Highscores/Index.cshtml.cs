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
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Data.SqlClient;

namespace BikeToWork.Pages.Highscores
{
    public class IndexModel : PageModel
    {
        private readonly BikeToWork.Data.ApplicationDbContext _context;

        public IndexModel(BikeToWork.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public Data.Models.Participant participant { get; set; }
        public ViewParticipant viewParticipant { get; set; }

        public IList<BikeRide> totalBikeRides { get; set; }
        public IList<BikeRide> listOfParticipantBikeRides { get; set; }
        public IList<BikeRide>? listOfAllBikeRidesById { get; set; }

        public IList<BikeClassEnum> listOfBikeClasses { get; set; }
        public IList<Data.Models.Participant> listOfParticipants { get; set; } = default!;
        public IList<ViewParticipant> listOfViewParticipant { get; set; }
        public IQueryable<ViewParticipant> viewParticipantsQueryable { get; set; }


        public void ConvertToViewParticipant()
        {
            foreach (var participant in listOfParticipants)
            {
                viewParticipant = new ViewParticipant
                {
                    id = participant.id,
                    firstName = participant.firstName,
                    lastName = participant.lastName,
                    team = participant.team,
                    bikeClass = participant.bikeClass
                };

                listOfAllBikeRidesById = GetAllBikeRidesById(participant.id, totalBikeRides);
                viewParticipant.allBikeRides = listOfAllBikeRidesById.ToList();
                viewParticipant.numberOfBikeRides = viewParticipant.allBikeRides.Count;

                viewParticipant.totalDistance = GetTotalDistance(viewParticipant.id);
                viewParticipant.averageDistance = GetAverageDistance(viewParticipant);
                listOfViewParticipant.Add(viewParticipant);
            }
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
        public decimal GetAverageDistance(ViewParticipant _viewParticipant)
        {
            if (_viewParticipant.numberOfBikeRides != 0)
            {
                _viewParticipant.averageDistance = (Decimal)_viewParticipant.totalDistance / _viewParticipant.numberOfBikeRides;

            }

            _viewParticipant.averageDistance = Math.Round(_viewParticipant.averageDistance, 2);
            return _viewParticipant.averageDistance;
        }
        public int GetTotalDistance(int _viewParticipantId)
        {
            int totalDistance = 0;

            foreach (var bikeRide in totalBikeRides)
            {
                if (bikeRide.participantId == _viewParticipantId)
                {
                    totalDistance += bikeRide.Distance;
                }
            }
            return totalDistance;
        }


        public async Task OnGetAsync(string sortOrder, string filter)
        {
            totalBikeRides = _context.BikeRides.ToList();
            listOfBikeClasses = (IList<BikeClassEnum>)Enum.GetValues(typeof(BikeClassEnum));
            listOfViewParticipant = new List<ViewParticipant>();
            viewParticipantsQueryable = listOfViewParticipant.AsQueryable();

            if (_context.Participants != null)
            {
                listOfParticipants = await _context.Participants.ToListAsync();
            }
            else
            {
                return;
            }

            ConvertToViewParticipant();

            #region Filtering
            if (!String.IsNullOrEmpty(filter))
            {
                viewParticipantsQueryable = viewParticipantsQueryable.Where(s => s.bikeClass.ToString().Equals(filter));

            }
            else
            {
                RedirectToAction("List");
            }
            #endregion

            #region Sorting
            switch (sortOrder)
            {
                case "totalDistance":
                    viewParticipantsQueryable = viewParticipantsQueryable.OrderByDescending(x => x.totalDistance);
                    break;
                case "numberOfBikeRides":
                    viewParticipantsQueryable = viewParticipantsQueryable.OrderByDescending(x => x.numberOfBikeRides);
                    break;
                case "averageDistance":
                    viewParticipantsQueryable = viewParticipantsQueryable.OrderByDescending(x => x.averageDistance);
                    break;
                default:
                    viewParticipantsQueryable = viewParticipantsQueryable.OrderByDescending(x => x.totalDistance);
                    break;
            }
            #endregion

            listOfViewParticipant = viewParticipantsQueryable.ToList();
        }
    }
}



//redirekte til Index?
 //custom tag helper med billeder af bike class. Måske også på andre sider som edit/create.
// test hvis databasen er tom. Er return tilstrækkeligt?
// ved alle klasser i dropdown, redirect til hovedside
//få dropdown til at blive på valgt item.
//Kan man lave en lille pil, når en sort kolonne er valgt? Ligesom nedenfor.
//Få tabel til at have scroll i højre side?

//private void AddSortError(ColumnHeader head, SortOrder order)
//{
//    const string ascArrow = " ▲";
//    const string descArrow = " ▼";

//     remove arrow
//    if (head.Text.EndsWith(ascArrow) || head.Text.EndsWith(descArrow))
//        head.Text = head.Text.Substring(0, head.Text.Length - 2);

//     add arrow
//    switch (order)
//    {
//        case SortOrder.Ascending: head.Text += ascArrow; break;
//        case SortOrder.Descending: head.Text += descArrow; break;
//    }
//}
//SetSortArrow(listView1.Columns[0], SortOrder.None);       // remove arrow from first column if present
//SetSortArrow(listView1.Columns[1], SortOrder.Ascending);  // set second column arrow to ascending
//SetSortArrow(listView1.Columns[1], SortOrder.Descending); // set second column arrow to descending
