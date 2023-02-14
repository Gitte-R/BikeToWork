using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Xml.Linq;

namespace BikeToWork.Data.Models
{
    public class ViewParticipant : Participant
    {
        [Display(Name = "Number of Bike Rides")]
        public int numberOfBikeRides { get; set; }
        [Display(Name = "Total Distance")]
        public int totalDistance { get; set; }
        [Display(Name = "Average Distance")]
        public decimal averageDistance { get; set; }
    }
}
