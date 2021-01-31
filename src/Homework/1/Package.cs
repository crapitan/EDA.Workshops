using System.Linq;
using System;
using System.Collections.Generic;

namespace Homework.First
{
    public class Package
    {
        public Package(Place[] destinations)
        {
            this.Destinations = destinations;
        }

        public int TravelTime { get; set; }

        public Place[] Destinations { get; set; }
        public bool Delivered { get; set; }

        public string Contains { get { return this.Destionation.Name; } }
        public Place Destionation { get { return this.Destinations[0]; } }
        public bool IsFinalDestination { get { return Destinations.Count() == 1; } }

        public void CalculateTravelTime()
        {
            switch (Destionation.Name)
            {
                case "A":
                    TravelTime = 4;
                    break;

                case "B":
                    TravelTime = 5;
                    break;

                case "port":
                    TravelTime = 1;
                    break;
            }
        }
    }
}
