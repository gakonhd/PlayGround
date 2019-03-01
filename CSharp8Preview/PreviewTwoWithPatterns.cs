using System;
using System.Collections.Generic;

namespace CSharp8Preview
{
    public class PreviewTwoWithPatterns
    {
        public string GetVehicleDetails(IVehicle tr)
        {
            var truckDetails = tr switch
            {
                //Truck t => $"Truck Id {t.Id} \r\nTruck Name: {t.Name}", // error out since it already handled
                //Truck t when t.Type == 2 => $"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs", //oldway
                Truck {Type : 2 } t => $"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs",
                //Truck t => $"Truck Id {t.Id} \r\nTruck Name: {t.Name}",
                //Truck (var x, var y, var z) => $"Truck Id {x} \r\nTruck Name: {y}\r\nTruck type: {z}", //wrong position
                //Truck (var x, var y, var z) => $"Truck Id {x} \r\nTruck Name: {z}\r\nTruck type: {y}",
                Truck(var (x,y,z)) => $"Truck Id {x} \r\nTruck Name: {z}\r\nTruck type: {y}",
                { } => string.Empty, //not null
                _ => "NullVehicle" //null case only
            };

            if (string.IsNullOrEmpty(truckDetails))
            {
                truckDetails = tr switch
                {
                    Car { SeatCapacity : 4} c => $"Compact Car has {c.SeatCapacity} seats. Affordable",
                    Car c when c.SeatCapacity < 4 => $"Car has {c.SeatCapacity} seats. Small Car",
                    Car { SeatCapacity: var S} => $"Car has {S} seats. Large Car",
                    { } => "N/A",
                    null => "N/A"
                    //_ => "No Information" //no need
                };
            }

            if (truckDetails.Equals("NullVehicle") || truckDetails.Equals("N/A"))
            {
                truckDetails = tr switch
                {
                    SuperVehicle (var truck, var (car1, _)) => $"SuperVehicle details: {truck.t1?.Name ?? "Lorem"}{truck.t2.Name ?? "Posem"}\r\nCapacity{car1.SeatCapacity}",
                    _ => "N/A even for superCar"
                };
            }

            return truckDetails;
        }

        public string GetVehicleDetailsConsolidated(IVehicle tr)
        {
            var truckDetails = tr switch
            {
                //Truck t => $"Truck Id {t.Id} \r\nTruck Name: {t.Name}", // error out since it already handled
                //Truck t when t.Type == 2 => $"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs", //oldway
                Truck { Type: 2 } t => $"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs",
                //Truck t => $"Truck Id {t.Id} \r\nTruck Name: {t.Name}",
                //Truck (var x, var y, var z) => $"Truck Id {x} \r\nTruck Name: {y}\r\nTruck type: {z}", //wrong position
                //Truck (var x, var y, var z) => $"Truck Id {x} \r\nTruck Name: {z}\r\nTruck type: {y}",
                Truck(var (x, y, z)) => $"Truck Id {x} \r\nTruck Name: {z}\r\nTruck type: {y}",
                Car { SeatCapacity: 4 } c => $"Compact Car has {c.SeatCapacity} seats. Affordable",
                Car c when c.SeatCapacity < 4 => $"Car has {c.SeatCapacity} seats. Small Car",
                Car { SeatCapacity: var S } => $"Car has {S} seats. Large Car",
                SuperVehicle(var truck, var (car1, _)) => $"SuperVehicle details: {truck.t1?.Name ?? "Lorem"}{truck.t2.Name ?? "Posem"}\r\nCapacity{car1.SeatCapacity}",
                _ => "N/A even for superCar"
            };
            return truckDetails;
        }

        public void GetTruckDetailsOld(IVehicle tr)
        {
            //oldway
            switch (tr)
            {
                case Truck t when t.Type == 2:
                    Console.WriteLine($"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs");
                    break;
                case Truck t:
                    Console.WriteLine($"Truck Id {t.SerialNumber} \r\nTruck Name: {t.Name}");
                    break;
                default:
                    Console.WriteLine("No truck to display");
                    break;
            }
        }

        public string GetTruckDetailsNewV3(IVehicle tr) => tr switch
        {
            Truck t when t.Type == 2 => $"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs",
            Truck t => $"Truck Id {t.SerialNumber} \r\nTruck Name: {t.Name}",
            _ => "No truck to display"
        };
    }

    public class SuperVehicle : IVehicle
    {
        public (Truck t1, Truck t2) TruckPart { get; }
        public (Car c1, Car c2) CarPart { get; }
        public SuperVehicle((Truck t1, Truck t2) truck, (Car c1, Car c2) car) => (TruckPart, CarPart) = (truck, car);
        public void Deconstruct(out (Truck t1, Truck t2) truckPart, out (Car c1, Car c2) carPart)
        //{
        //    truckPart = TruckPart;
        //    carPart = CarPart;
        //}
        => (truckPart, carPart) = (TruckPart, CarPart);
    }

    public class Truck : IVehicle
    {

        public Truck()
        {
            SerialNumber = CreateUniqueIds();
            Name = Guid.NewGuid().ToString();
            Type = new Random().Next(1, 3);
        }

        //C# 7.0 Deconstructor
        public void Deconstruct(out int serialNumber, out int type, out string? name)
        {
            serialNumber = SerialNumber;
            type = Type;
            name = Name;
        }

        public void Deconstruct(out (int serialNumber, int type, string? name) truckTuple)
        => truckTuple = (SerialNumber, Type, Name);

        public int SerialNumber { get; }
        public int Type { get; set; }
        public string? Name { get; set; }
        public enum TruckType
        {
            BigTruck = 1,
            SmallTruck = 2,
            MediumTruck = 3
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
    }

    public class Car : IVehicle
    {
        public Car()
        {
            SeatCapacity = new Random().Next(2, 10);
        }
        public int SeatCapacity { get; }
    }

    public interface IVehicle { }
}
