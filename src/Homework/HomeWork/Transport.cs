using System.Linq;
using System;
using System.Collections.Generic;

namespace Homework.First
{

    public class Transport
    {
        public Transport(Place home, string name)
        {
            this.Home = home;
            this.Name = name;
        }

        public int DistanceFromHome { get; set; }

        public Place Home { get; set; }
        public string Name { get; private set; }
        public Package InTransport { get; set; }

        public void Move()
        {
            // When home check to load
            if (DistanceFromHome == 0 && Home.Storage.Any() && InTransport is null)
            {
                // Load package 
                this.InTransport = Home.Storage.First().Value;
                this.InTransport.CalculateTravelTime();

                int index = Home.Storage.First().Key;
                this.Home.Storage.Remove(index);
                System.Console.WriteLine($"{this.Name}: Loading package on in {Home.Name}");

                // Lets move out
                this.DistanceFromHome++;
                this.InTransport.TravelTime--;
                System.Console.WriteLine($"{this.Name}: Moving to destination");
            }
            
            // En route To Destination 
            else if (this.InTransport is not null && this.InTransport.TravelTime != 0)
            {
                this.DistanceFromHome++;
                this.InTransport.TravelTime--;
                System.Console.WriteLine($"{this.Name}: Moving to destination");
            }
            else if (this.InTransport is null && DistanceFromHome > 0)
            {
                this.DistanceFromHome--;
                Console.WriteLine($"{this.Name}: Moving back home");
            }

            // Unload if we are there
            if (this.InTransport is not null && this.InTransport.TravelTime == 0)
            {
                Console.WriteLine($"{this.Name}: Unloading");
                
                // Unload
                if (this.InTransport.IsFinalDestination)
                {
                    this.InTransport.Delivered = true;
                } 
                   
                var index = this.InTransport.Destionation.Storage.Any() ? (this.InTransport.Destionation.Storage.Max(k => k.Key) + 1) : 1;
                this.InTransport.Destionation.Storage.Add(index, this.InTransport);
                this.InTransport.Destinations = this.InTransport.Destinations.Skip(1).ToArray();

                this.InTransport = null;
            }
        }
    }
}