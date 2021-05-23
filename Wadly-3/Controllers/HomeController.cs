using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Wadly_3.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Wadly_3.Database;

namespace Festivalsite.Controllers
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

            // stop de namen in de html
            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
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
                            Beschrijving = reader["Beschrijving"].ToString()
                        };
                        Film.Add(p);
                    }
                }
            }
            return Film[0];
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
                MySqlCommand cmd = new MySqlCommand("select * from product", conn);

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

        [Route("films/{id}")]
        public IActionResult Films(string id)
        {
          var model = GetNames(id)
            
          return View(model);
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
        public IActionResult Contact(string voornaam, string achternaam)
        {
            ViewData["voornaam"] = voornaam;
            ViewDate["achternaam"] = achternaam;
            
            return View();
        }

        [Route("account")]
        public IActionResult account()
        {
            return View();
        }

    }
}
