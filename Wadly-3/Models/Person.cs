using System.ComponentModel.DataAnnotations;

namespace SendAndStore.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Gelieve uw voornaam in te vullen")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Gelieve uw achternaam in te vullen")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gelieve uw Email in te vullen")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Gelieve uw beschrijving in te vullen")]
        public string Description { get; set; }
    }
}