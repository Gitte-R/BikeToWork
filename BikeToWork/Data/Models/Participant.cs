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

//Hvis id er unit (0 til 4 mia) så giver det problemer med funktionen i onpostAsync med funktionen: ParticipantExists(Participant.id) fordi den er lavet på en int. I så fald skal den convertTiInt

// Typen af cykel er oprettet på brugeren. Så tager brugeren en anden cykel skal denne have en yderligere profil. Antager at de fleste har én cykel. Man tilmelder sig en kategori på cykel klasse.