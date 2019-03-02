using System;
using System.Collections.Generic;
using System.Text;

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
                Truck { Type: 2 } t => $"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs",
                //Truck t => $"Truck Id {t.Id} \r\nTruck Name: {t.Name}",
                //Truck (var x, var y, var z) => $"Truck Id {x} \r\nTruck Name: {y}\r\nTruck type: {z}", //wrong position
                //Truck (var x, var y, var z) => $"Truck Id {x} \r\nTruck Name: {z}\r\nTruck type: {y}",
                Truck(var (x, y, z)) => $"Truck Id {x} \r\nTruck Name: {z}\r\nTruck type: {y}",
                { } => string.Empty, //not null
                _ => "NullVehicle" //null case only
            };

            if (string.IsNullOrEmpty(truckDetails))
            {
                truckDetails = tr switch
                {
                    Car { SeatCapacity: 4 } c => $"Compact Car has {c.SeatCapacity} seats. Affordable",
                    Car c when c.SeatCapacity < 4 => $"Car has {c.SeatCapacity} seats. Small Car",
                    Car { SeatCapacity: var S } => $"Car has {S} seats. Large Car",
                    { } => "N/A",
                    null => "N/A"
                    //_ => "No Information" //no need
                };
            }

            if (truckDetails.Equals("NullVehicle") || truckDetails.Equals("N/A"))
            {
                truckDetails = tr switch
                {
                    //SuperVehicle (var truck, var (car1, _)) => $"SuperVehicle details: {truck.t1?.Price ?? "Lorem"}{truck.t2.Price ?? "Posem"}\r\nCapacity{car1.SeatCapacity}",
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
                //SuperVehicle(var truck, var (car1, _)) => $"SuperVehicle details: {truck.t1?.Price ?? "Lorem"}{truck.t2.Price ?? "Posem"}\r\nCapacity{car1.SeatCapacity}",
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
                    Console.WriteLine($"Truck Id {t.SerialNumber} \r\nTruck Name: {t.Price}");
                    break;
                default:
                    Console.WriteLine("No truck to display");
                    break;
            }
        }

        public string GetTruckDetailsNewV3(IVehicle tr) => tr switch
        {
            Truck t when t.Type == 2 => $"Truck Id: {t.SerialNumber} \r\nSmall Truck can carry only 1500 lbs",
            Truck t => $"Truck Id {t.SerialNumber} \r\nTruck Name: {t.Price}",
            _ => "No truck to display"
        };

        public string GetVehicleInfoClean(IVehicle v)
        {
            /*
            * Property Pattern
            * {} - handles not null
            * _ - handles the rest. a.k.a Discard Pattern
            * null - handles null case
            */
            var vehicleInfo = v switch
            {
                //ERROR: The pattern has already been handled by the previous arm of the switch expression
                //Car { SeatCapacity: var c, CarId: var id } => $"Car has {c}. Car Id is {id}. Big car.",
                Car c when c.SeatCapacity < 5 & c.SeatCapacity > 2 => $"Car has {c.SeatCapacity}. Small Car", //old pattern
                Car { SeatCapacity: 1 } => $"Car has 1 seat. Mono car",
                Car { SeatCapacity: 2 } c => $"Car has two seats. Car Id is {c.CarId}. Mini car.",
                Car { SeatCapacity: var c, CarId: var id } => $"Car has {c}. Car Id is {id}. Big car.",
                { } => string.Empty,
                null => default,
                //ERROR: The pattern has already been handled by the previous arm of the switch expression
                //_ => "ErrorHere"
            };
            /*
             * positional pattern
             * discard pattern
             * var pattern
             */
            if (string.IsNullOrEmpty(vehicleInfo))
            {
                vehicleInfo = v switch
                {
                    Truck(_, 1, _) t => $"This is a big truck.\r\nTruck Id: {t.SerialNumber}",
                    Truck(var s, var t, var p) => $"Truck serial number is {s}\r\nTruck type is {t}\r\nPrice is {p}",
                    _ => default
                };
            }
            /*
             * positional pattern with deconstructor
             */
            if (string.IsNullOrEmpty(vehicleInfo))
            {
                vehicleInfo = v switch
                {
                    SuperVehicle (var truck, var (car1, _)) => ((Func<string>)(() =>
                    {
                        var sb = new StringBuilder();
                        sb.Append($"New Serial Number {truck.t1.SerialNumber} {truck.t2.SerialNumber}\r\n");
                        sb.Append($"New Seat Capacity {car1.SeatCapacity}");
                        return sb.ToString();
                    }))(),
                    { } => "Not an eligible car",
                    null => "Null object",
                };
            }

            return vehicleInfo;
        }
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

        public void Deconstruct(out (Car c1, Car c2) carPartTuple) => carPartTuple = CarPart;
    }

    public class Truck : IVehicle
    {

        public Truck()
        {
            SerialNumber = CreateUniqueIds();
            Price = NextDecimal();
        }

        //C# 7.0 Deconstructor
        public void Deconstruct(out string serialNumber, out int type, out decimal price)
        {
            serialNumber = SerialNumber;
            type = Type;
            price = Price;
        }

        public void Deconstruct(out (string serialNumber, int type, decimal price) truckTuple)
        => truckTuple = (SerialNumber, Type, Price);

        public string SerialNumber { get; }
        public int Type { get; set; }
        public decimal Price { get; set; }
        public enum TruckType
        {
            BigTruck = 1,
            SmallTruck = 2,
            MediumTruck = 3
        }

        private string CreateUniqueIds()
        {
            var rIdx = new Random().Next(1, 10000000);
            while (_uniqueIds.Contains(rIdx))
            {
                rIdx = new Random().Next(1, 10000000);
            }
            _uniqueIds.Add(rIdx);
            return Convert.ToString(rIdx);
        }

        private decimal NextDecimal()
        {
            var rng = new Random();
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.Next(),
                               rng.Next(),
                               rng.Next(),
                               sign,
                               scale);
        }

        private readonly HashSet<int> _uniqueIds = new HashSet<int>();
    }

    public class Car : IVehicle
    {
        public Car()
        {
            //SeatCapacity = new Random().Next(2, 10);
            CarId = Guid.NewGuid().ToString();
        }
        public int SeatCapacity { get; set; }
        public string CarId { get; }
    }

    public class Bicycle : IVehicle { }
    public interface IVehicle { }
}
