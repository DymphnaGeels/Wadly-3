﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wadly_3.Database
{
    public class Film
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Beschikbaarheid { get; internal set; }
        public string Naam { get; internal set; }
        public string Prijs { get; internal set; }

        public string Img { get; set; }
    }
}