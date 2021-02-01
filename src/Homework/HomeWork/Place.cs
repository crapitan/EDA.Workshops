using System.Linq;
using System;
using System.Collections.Generic;

namespace Homework.First
{
    public class Place
    {
        public Place(string name)
        {
            this.Name = name;
        }

        public SortedList<int, Package> Storage { get; set; } = new SortedList<int, Package>();
        
        public string Name { get; set; }
    }
}