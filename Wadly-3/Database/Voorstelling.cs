using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wadly_3.Database
{
    public class Voorstelling
    {
        public int Voorraad { get; internal set; }
        public int Id { get; internal set; }
        public int Film_id { get; internal set; }
        public DateTime Datum { get; internal set; }
    }
}
