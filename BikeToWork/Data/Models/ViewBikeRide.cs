using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BikeToWork.Data.Models
{
    public class ViewBikeRide : BikeRide
    {
        [Display(Name = "Name of Participant")]
        public string FullName { get; set; }
    }
}
