using System.Linq;
using System;
using System.Collections.Generic;

namespace Homework.First
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new Place("factory");
            var port = new Place("port");
            var a = new Place("A");
            var b = new Place("B");

            var lorry1 = new Transport(factory, "Lorry 1");
            var lorry2 = new Transport(factory, "Lorry 2");
            var ferry = new Transport(port, "Ferry 1");

            Console.WriteLine(@"Factory ACME INC");
            Console.WriteLine(@"Produce new goods by entering destionation");
            string productList = Console.ReadLine();

            var sortedProductionList = new SortedList<int, Package>();
         
            int index = 1;

            for (var i = 0; i < productList.Length; i++)
            {
                string productType = productList[i] + string.Empty;

                switch (productType.ToLower())
                {
                    case "a":
                        sortedProductionList.Add(index, new Package(new Place[] { port, a }));
                        index ++;
                        break;

                    case "b":
                        sortedProductionList.Add(index, new Package(new Place[] { b }));
                        index ++;
                        break;

                    default:
                        break;
                }

            }

            var allPackages = sortedProductionList.ToList();
            factory.Storage = sortedProductionList;

            for (int i = 1; i < 100; i++)
            {
                ferry.Move();
                lorry1.Move();
                lorry2.Move();

                if (allPackages.All(pckge => pckge.Value.Delivered))
                {
                    System.Console.WriteLine($"All packages deliverd at {i}");
                    break;
                }
            }

        }
    }
}
