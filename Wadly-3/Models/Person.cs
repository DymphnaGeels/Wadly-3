using System.ComponentModel.DataAnnotations;

namespace SendAndStore.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Gelieve uw voornaam in te vullen")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Gelieve uw achternaam in te vullen")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gelieve uw Emailadres in te vullen")]
        [EmailAddress(ErrorMessage = "Geen geldig email adres")]
        public string Email { get; set; }
        public string Phone { get; set; }

   
        [Display(Name = "Description")]
        public string Description { get; set; }        

        public string Password { get; set; }
    }
}