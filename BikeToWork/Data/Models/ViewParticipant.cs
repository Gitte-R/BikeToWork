using System.ComponentModel.DataAnnotations;
using System.Composition;
using System.Xml.Linq;

namespace BikeToWork.Data.Models
{
    public class ViewParticipant : Participant
    {
        [Display(Name = "Number of Bike Rides")]
        public int numberOfBikeRides { get; set; }
        [Display(Name = "Total distance")]
        public int totalDistance { get; set; }
        public decimal averageDistance { get; set; }
    }
}
