using System;
using System.Collections.Generic;

namespace CSharp8Preview
{
    public class PreviewTwoWithPatterns
    {
        public void GetTruckDetails(object tr)
        {
            switch (tr)
            {
                case Truck t when t.Type == 2:
                    Console.WriteLine($"Truck Id: {t.Id} \r\nSmall Truck can carry only 1500 lbs");
                    break;
                case Truck t:
                    Console.WriteLine($"Truck Id {t.Id} \r\nTruck Name: {t.Name}");
                    break;
                default:
                    Console.WriteLine("No truck to display");
                    break;
            }
        }
    }

    public class Truck
    {

        public Truck()
        {
            Id = CreateUniqueIds();
        }

        private int CreateUniqueIds()
        {
            var rIdx = new Random().Next(1, 10000000);
            while (_uniqueIds.Contains(rIdx))
            {
                rIdx = new Random().Next(1, 10000000);
            }
            _uniqueIds.Add(rIdx);
            return rIdx;
        }
        private readonly HashSet<int> _uniqueIds = new HashSet<int>();

        public int Id { get; }
        public int Type { get; set; }
        public string? Name { get; set; }
        public enum TruckType
        {
            BigTruck = 1,
            SmallTruck = 2,
            MediumTruck = 3
        }
    }

    public class Car
    {
        public int SeatCapacity { get; set; }
    }
}
