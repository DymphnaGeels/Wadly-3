using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wadly_3.Database
{
    public class Klant
    {
        public string Voornaam { get; internal set; }
        public string email { get; internal set; }
        public string Achternaam { get; internal set; }
        public int Id { get; internal set; }
    }
}
