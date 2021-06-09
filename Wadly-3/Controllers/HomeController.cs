using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Wadly_3.Models;
using MySql.Data.MySqlClient;
using Wadly_3.Database;
using SendAndStore.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

namespace SendAndStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // stel in waar de database gevonden kan worden
        //string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110533;Uid=110533;Pwd=inf2021sql";
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110533;Uid=110533;Pwd=inf2021sql";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]

        public IActionResult Homepage()
        {
            // alle namen ophalen
            var products = GetNames();

            ViewData["user"] = HttpContext.Session.GetString("user");

            // stop de namen in de html
            return View(products);
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("film/{id}")]
        public IActionResult Details(string id)
        {
            var model = GetFilm(id);

            ViewData["voorstellingen"] = GetVoorstellingen(id);

            return View(model);
        }

        private List<Voorstelling> GetVoorstellingen(string id)
        {
            List<Voorstelling> voorstellingen = new List<Voorstelling>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from voorstelling where film_id = {id}", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Voorstelling p = new Voorstelling
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Film_id = Convert.ToInt32(reader["film_id"]),
                            Voorraad = Convert.ToInt32(reader["Voorraad"]),
                            Datum = DateTime.Parse(reader["Datum"].ToString()),

                        };
                        voorstellingen.Add(p);
                    }
                }
            }
            return voorstellingen;
        }

        public List<Film> GetNames()
        {

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Film> Film = new List<Film>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from film", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Film p = new Film
                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            // Beschikbaarheid = Convert.ToInt32(reader["Beschikbaarheid"]),
                            Naam = reader["Naam"].ToString(),
                            Img = reader["img"].ToString(),
                        };

                        // voeg de naam toe aan de lijst met namen
                        Film.Add(p);
                    }
                }
            }

            // return de lijst met namen
            return Film;
        }

        private Film GetFilm(string id)
        {
            List<Film> Film = new List<Film>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from film where id = {id}", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Film p = new Film
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Prijs = reader["Prijs"].ToString(),
                            Img = reader["Img"].ToString(),
                        };
                        Film.Add(p);
                    }
                }
            }
            return Film[0];
        }

        private List<Film> GetFilms()
        {
            List<Film> films = new List<Film>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from film", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Film p = new Film
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Prijs = reader["Prijs"].ToString(),
                            Img = reader["Img"].ToString(),
                        };
                        films.Add(p);
                    }
                }
            }
            return films;
        }

        public List<string> GetFilmsinfo()

        {

            // maak een lege lijst waar we de namen in gaan opslaan
            List<string> names = new List<string>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from film", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                        string Name = reader["Naam"].ToString();

                        // voeg de naam toe aan de lijst met namen
                        names.Add(Name);
                    }
                }
            }

            // return de lijst met namen
            return names;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        [Route("Films")]
        public IActionResult Films()
        {
            // alle namen ophalen
            var products = GetFilms();

            // stop de namen in de html
            return View(products);
        }

        [Route("genres")]
        public IActionResult genres()
        {
            return View();
        }

        [Route("Events")]
        public IActionResult Events()
        {
            return View();
        }

        [Route("Unlimited")]
        public IActionResult Unlimited()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("Contact")]
        [HttpPost]
        public IActionResult Contact(Person person)
        {
            // hebben we alles goed ingevuld? Dan sturen we de gebruiker door naar de succes pagina
            if (ModelState.IsValid)
            {
                // alle benodigde gegevens zijn aanwezig, we kunnen opslaan!
                SavePerson(person);

                return Redirect("/succes");
            }
            // niet goed? Dan sturen we de gegevens door naar de view zodat we de fouten kunnen tonen
            return View(person);
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }

        [Route("adverteren")]
        public IActionResult adverteren()
        {
            return View();
        }

        [Route("betaal")]
        public IActionResult betaal()
        {
            return View();
        }

        [Route("notfound")]
        public IActionResult notfound()
        {
            return View();
        }

        private void SavePerson(Person person)
        {
            if (person.Password != null)
                person.Password = ComputeSha256Hash(person.Password);
            else
                person.Password = "leeg";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(voornaam, achternaam, wachtwoord, bericht) VALUES(?voornaam, ?achternaam, ?wachtwoord, ?bericht)", conn);

                cmd.Parameters.Add("?voornaam", MySqlDbType.Text).Value = person.FirstName;
                cmd.Parameters.Add("?achternaam", MySqlDbType.Text).Value = person.LastName;
                cmd.Parameters.Add("?wachtwoord", MySqlDbType.Text).Value = person.Password;
                cmd.Parameters.Add("?bericht", MySqlDbType.Text).Value = person.Description;
                cmd.ExecuteNonQuery();
            }
        }

        [Route("account")]
        public IActionResult account(string username, string password)
        {
            // hash voor "wachtwoord"
            string hash = "dc00c903852bb19eb250aeba05e534a6d211629d77d055033806b783bae09937";

            // is er een wachtwoord ingevoerd?
            if (!string.IsNullOrWhiteSpace(password))
            {
                //Er is iets ingevoerd, nu kunnen we het wachtwoord hashen en vergelijken met de hash "uit de database"
                string hashVanIngevoerdWachtwoord = ComputeSha256Hash(password);
                if (hashVanIngevoerdWachtwoord == hash)
                {
                    HttpContext.Session.SetString("User", username);
                    return Redirect("/");
                }               
                return View();
            }

            return View();

        }

        [Route("account")]
        [HttpPost]
        public IActionResult account(Person person)
        {
            // hebben we alles goed ingevuld? Dan sturen we de gebruiker door naar de succes pagina
            if (ModelState.IsValid)
            {
                // alle benodigde gegevens zijn aanwezig, we kunnen opslaan!
                SavePerson(person);

                return Redirect("/succeslogin");
            }
            // niet goed? Dan sturen we de gegevens door naar de view zodat we de fouten kunnen tonen
            return View(person);
        }
    }
}