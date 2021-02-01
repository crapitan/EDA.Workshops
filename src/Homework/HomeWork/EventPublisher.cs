using System.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Homework.First
{

    public static class EventPublisher
    {
        public static void Publish(EventPost log)
        {
            var json = JsonConvert.SerializeObject(log);
            Console.WriteLine(json);
        }
    }

    public class EventPost
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        
        [JsonProperty("time")]
        public int Time { get; set; }
        
        [JsonProperty("transport_id")]
        public string TransportId { get; set; }
        
        [JsonProperty("kind")]
        public string Kind { get; set; }
        
        [JsonProperty("location")]
        public string Location { get; set; }
        
        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("cargo")]
        public List<Cargo> Cargo { get; set; }
    }

    public class Cargo
    {

        [JsonProperty("cargo_id")]
        public string Id { get; set; }
        
        [JsonProperty("destination")]
        public string Destination { get; set; }
 
        [JsonProperty("origin")]
        public string Origin { get; set; } = "FACTORY";
    }

}