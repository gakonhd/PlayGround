using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp8Preview
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var check = true;
            while (check)
            {
                Console.Clear();
                Console.WriteLine("Things to demo");
                Console.WriteLine("1 - Legacy Code with Null Reference Pointer Error");
                Console.WriteLine("2 - New Code handle Null Reference");
                Console.WriteLine("3 - First Preview");
                Console.WriteLine("4 - Second Preview");
                var key = Console.ReadKey();
                Console.WriteLine();
                MyClass myClass = new MyClass();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    try
                    {
                        SomeMethod(myClass);
                        _ = myClass.LegacyRandom.Next(1, 10);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    SomeMethod(myClass);
                    var rdx = myClass.NewRandom?.Next(1, 10);
                    Console.WriteLine($"NewRandom is called with return value {rdx?.ToString() ?? "[No value returned]"}");
                    Console.ReadLine();
                }
                else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    await HandleD1();
                }
                else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
                    HandleD3();
                }
                else
                {
                    check = false;
                }
            }

        }
        public class MyClass
        {
            public Random? NewRandom = new Random();
            public Random LegacyRandom = new Random();
        }

        static void SomeMethod(MyClass myClass)
        {
            myClass.LegacyRandom = null;
            myClass.NewRandom = null;
        }

        private static async Task HandleD1()
        {
            const string fastenal = "FASTENAL";
            var tc = new TestClass();
            string myNullString = null;
            //Console.WriteLine($"Get something from null string {myNullString[0]}");
            var check = true;
            while (check)
            {
                Console.WriteLine("***********************************************");
                Console.WriteLine("Things to demo for nullable ref and IAsyncEnum");
                Console.WriteLine("1 - Access a property of nullable ref type");
                Console.WriteLine("2 - Invoke a method from a null object");
                Console.WriteLine("3 - Synchronously Printout the item  from a string");
                Console.WriteLine("4 - Synchronously Printout the item in a lazy enumerations from a string with yield");
                Console.WriteLine("5 - Asynchronously Printout the item  from a string wrapped inside async call");
                Console.WriteLine("6 - Asynchronously Printout the item from a collection");
                Console.WriteLine("7 - Asynchronously Printout the item from collection by interating asynchronously");
                Console.WriteLine("***********************************************");

                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    Console.Clear();
                    string? myNullableString = null; // make string nullable
                    Console.WriteLine("string? myNullableString = null; ");
                    Console.WriteLine("Console.WriteLine($\"Get something from nullable string {myNullableString?[0]}\")");
                    Console.WriteLine($"Get something from nullable string {myNullableString?[0]}");
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    Console.Clear();
                    TestClass test = null;
                    test?.PrintSomething("Hello WOrld");

                    TestClass? testNull = null; // make all reference type nullable now
                    testNull?.PrintSomething("Hello WOrld");
                    Console.WriteLine($"TestClass test = null;\r\ntest?.PrintSomething(\"Hello WOrld\");\r\nTestClass? testNull = null;\r\ntestNull?.PrintSomething(\"Hello WOrld\");");
                }
                else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    Console.Clear();
                    //Calling the Sync Print
                    Console.WriteLine("Starting with the current thread");
                    Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Result is {tc.SyncPrint(fastenal)}");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                }
                else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
                    Console.Clear();
                    //Calling the Sync Print W Yield
                    Console.WriteLine("Starting with the current thread");
                    Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
                    foreach (var c in tc.YieldSyncPrint(fastenal))
                    {
                        Console.WriteLine($"Current string is {c} - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    }
                    Console.WriteLine($"Finished with the yield sync - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                }
                else if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5)
                {
                    Console.Clear();
                    //Calling the Async Print
                    Console.WriteLine("Starting with the current thread");
                    Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Result is {await tc.AsyncPrint(fastenal)} - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Finished with the async print - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                }
                else if (key.Key == ConsoleKey.D6 || key.Key == ConsoleKey.NumPad6)
                {
                    Console.Clear();
                    //Calling the Async Print
                    Console.WriteLine("Starting with the current thread");
                    Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
                    var res = await tc.AsyncEnumPrint(fastenal);
                    Console.WriteLine($"Finished with the async enum print - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    foreach (var c in res)
                    {
                        Console.WriteLine($"Result is {c} - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    }
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                }
                else if (key.Key == ConsoleKey.D7 || key.Key == ConsoleKey.NumPad7)
                {
                    Console.Clear();
                    //Calling the Async Print
                    Console.WriteLine("Starting with the current thread");
                    Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
                    var res2 = tc.AsyncWithYield(fastenal);
                    Console.WriteLine($"Finished with the async enum print with yield - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    await foreach (var c in res2)
                    {
                        Console.WriteLine($"Result is {c} - Thread id is {Thread.CurrentThread.ManagedThreadId}");
                    }
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                }
                else
                {
                    check = false;
                }
            }
        }

        private static void HandleD3()
        {
            var check = true;
            var preview2 = new PreviewTwoWithPatterns();
            while (check)
            {
                Console.Clear();
                Console.WriteLine("PATTERN DEMO");
                Console.WriteLine("1 - Car");
                Console.WriteLine("2 - Truck");
                Console.WriteLine("3 - Super Car");
                Console.WriteLine("4 - Bicycle");
                Console.WriteLine("5 - Null");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    Console.WriteLine("New Car is created");
                    var car = new Car();
                    Console.WriteLine("Please enter seat capacity: ");
                    var input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        int capacity;
                        while (!int.TryParse(input, out capacity))
                        {
                            Console.WriteLine("This is not an int");
                            input = Console.ReadLine();
                        }
                        car.SeatCapacity = capacity;
                        Console.WriteLine(preview2.GetVehicleInfoClean(car));
                        Console.ReadLine();
                    }
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    Console.WriteLine("New Truck is created");
                    var truck = new Truck();
                    Console.WriteLine("Please enter truck Type (1-3): ");
                    var input = Console.ReadLine();
                    if (!string.IsNullOrEmpty(input))
                    {
                        int type;
                        while (!int.TryParse(input, out type) || type > 3)
                        {
                            Console.WriteLine("This is not a valid type");
                            input = Console.ReadLine();
                        }
                        truck.Type = type;
                        Console.WriteLine(preview2.GetVehicleInfoClean(truck));
                        Console.ReadLine();
                    }
                }
                else if (key.Key == ConsoleKey.D3|| key.Key == ConsoleKey.NumPad3)
                {
                    Console.WriteLine("New Super Vehicle is created");
                    var rdn = new Random();
                    var superVehicle = new SuperVehicle((new Truck(), new Truck()), (new Car() { SeatCapacity = rdn.Next(2, 10) }, new Car() { SeatCapacity = rdn.Next(2, 10) }));
                    Console.WriteLine(preview2.GetVehicleInfoClean(superVehicle));
                    Console.ReadLine();
                }
                else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
                    Console.WriteLine("New Bicycle is created");
                    var bike = new Bicycle();
                    Console.WriteLine(preview2.GetVehicleInfoClean(bike));
                    Console.ReadLine();
                }
                else if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5)
                {
                    Console.WriteLine("New NULL is created");
                    IVehicle? nullVehicle = default;
                    Console.WriteLine(preview2.GetVehicleInfoClean(nullVehicle));
                    Console.ReadLine();
                }
                else
                {
                    check = false;
                }
            }
        }

        private static void HandleD2()
        {
            Random r = new Random();
            var collection = new List<IVehicle?>();
            for (var i = 0; i < 10; i++)
            {
                var idx = r.Next(1, 4);
                if (idx % 2 == 0)
                {
                    collection.Add(new Car());
                }
                else if (idx % 3 == 0)
                {
                    collection.Add(new SuperVehicle((new Truck(), new Truck()), (new Car(), new Car())));
                }
                else
                {
                    collection.Add(new Truck());
                }
            }

            //adding null case
            collection.Add(null);

            //adding a null truck
            Truck nullTruck = null;
            collection.Add(nullTruck);

            var pr2 = new PreviewTwoWithPatterns();
            foreach (var v in collection)
            {
                //pr2.GetTruckDetailsOld(v);
                Console.WriteLine(pr2.GetVehicleDetails(v));
                //Console.WriteLine(pr2.GetVehicleDetailsConsolidated(v)); //same thing but more consolidation 
                Console.WriteLine("---------------------------------------");
            }
            Console.ReadLine();
        }

    }
}
