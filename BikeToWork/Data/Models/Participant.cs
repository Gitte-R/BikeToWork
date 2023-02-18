using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BikeToWork.Data.Models
{
    public class Participant
    {
        public int Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Name of Team")]
        public string? Team { get; set; }
        [Display(Name = "Type of Bike")]
        public BikeClassEnum BikeClass { get; set; }
        public IList<BikeRide>? AllBikeRides { get; set; }
    }
}