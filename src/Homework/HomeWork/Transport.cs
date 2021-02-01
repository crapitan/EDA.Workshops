using System.Linq;
using System;
using System.Collections.Generic;

namespace Homework.First
{
    public class Lorry : Transport
    {
        public Lorry(Place home, string name) : base (home, name, "LORRY")
        {
           
        }
    }

    public class Ferry : Transport
    {
        public Ferry(Place home, string name) : base(home, name, "FERRY")
        {
           
        }
    }

    public class Transport
    {
        public Transport(Place home, string name, string type)
        {
            this.Home = home;
            this.Name = name;
            this.TransportType = type;
        }

        public int DistanceFromHome { get; set; }

        public Place Home { get; set; }
        public string Name { get; private set; }
        public string TransportType { get; }
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
           
                // Lets move out
                EventPublisher.Publish(new EventPost { Event  = "DEPART", Time = DistanceFromHome, Destination = this.InTransport.Destionation.Name, TransportId = this.Name, Kind = TransportType, Location = Home.Name, Cargo = new List<Cargo> { new Cargo { Destination = this.InTransport.Destinations.Last().Name, Id = this.InTransport.Id.ToString() } } });
                this.DistanceFromHome++;
                this.InTransport.TravelTime--;
            }

            // En route To Destination 
            else if (this.InTransport is not null && this.InTransport.TravelTime != 0)
            {
                this.DistanceFromHome++;
                this.InTransport.TravelTime--;
                
            }
            else if (this.InTransport is null && DistanceFromHome > 0)
            {
                this.DistanceFromHome--;

                if (DistanceFromHome == 0)
                {
                    // We have arrived at home
                    EventPublisher.Publish(new EventPost { Event = "ARRIVE", Time = DistanceFromHome, TransportId = this.Name, Kind = TransportType, Location = this.Home.Name, Cargo = new List<Cargo>() });
                }
            }

            // Unload if we are there
            if (this.InTransport is not null && this.InTransport.TravelTime == 0)
            {

                EventPublisher.Publish(new EventPost { Event = "ARRIVE", Time = DistanceFromHome, Destination = this.InTransport.Destionation.Name, TransportId = this.Name, Kind = TransportType, Location = this.InTransport.Destionation.Name, Cargo = new List<Cargo> { new Cargo { Destination = this.InTransport.Destinations.Last().Name, Id = this.InTransport.Id.ToString() } } });

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