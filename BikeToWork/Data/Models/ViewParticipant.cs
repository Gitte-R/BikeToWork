using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Xml.Linq;

namespace BikeToWork.Data.Models
{
    public class ViewParticipant : Participant
    {
        [Display(Name = "Total Bike Rides")]
        public int TotalBikeRides { get; set; }
        [Display(Name = "Total Distance")]
        public int TotalDistance { get; set; }
        [Display(Name = "Average Distance")]
        public decimal AverageDistance { get; set; }
    }
}
