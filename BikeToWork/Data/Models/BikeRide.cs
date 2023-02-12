using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BikeToWork.Data.Models
{
    public class BikeRide
    {
        public int id { get; set; }
        [Display(Name = "Date of Bike ride")]
        public DateTime date { get; set; }
        [Display(Name = "Id of Participant")]
        public int participantId { get; set; }
        [Display(Name = "Distance of Bike ride")]
        public byte Distance { get; set; }
    }
}

//Byte kan kun være mellem 0 og 255. Jeg antager at der ikke køres længere på én tur.
