using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BikeToWork.Data.Models
{
    public class BikeRide
    {
        public int Id { get; set; }
        [Display(Name = "Date of Bike Ride")]
        public DateTime Date { get; set; }
        [Display(Name = "Id of Participant")]
        public int ParticipantId { get; set; }
        [Display(Name = "Distance of Bike Ride")]
        public byte Distance { get; set; }
    }
}