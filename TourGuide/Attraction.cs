using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace TourGuide
{
    public class Attraction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
        public string OpeningHours { get; set; }
    }

}