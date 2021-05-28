using System.ComponentModel.DataAnnotations;

namespace SendAndStore.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Gelieve uw voornaam in te vullen")]
        [Display(Name = "voornaam")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Gelieve uw achternaam in te vullen")]
        [Display(Name = "achternaam")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gelieve uw Emailadres in te vullen")]
        [EmailAddress(ErrorMessage = "Geen geldig email adres")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Gelieve uw beschrijving in te vullen")]
        [Display(Name ="Bericht")]
        public string Description { get; set; }
    }
}