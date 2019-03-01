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
                Console.WriteLine("1 - First Preview");
                Console.WriteLine("2 - Second Preview");
                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    await HandleD1();
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    HandleD2();
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
            collection.Add(null);
            Truck nullTruck = null;
            collection.Add(nullTruck);

            var pr2 = new PreviewTwoWithPatterns();
            foreach (var v in collection)
            {
                //pr2.GetTruckDetailsOld(v);
                //Console.WriteLine(pr2.GetVehicleDetails(v));
                Console.WriteLine(pr2.GetVehicleDetailsConsolidated(v));
                Console.WriteLine("---------------------------------------");
            }
            Console.ReadLine();
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
                    string? myNullableString = null; // make string nullable
                    Console.WriteLine("string? myNullableString = null; ");
                    Console.WriteLine("Console.WriteLine($\"Get something from nullable string {myNullableString?[0]}\")");
                    Console.WriteLine($"Get something from nullable string {myNullableString?[0]}");
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    TestClass test = null;
                    test?.PrintSomething("Hello WOrld");

                    TestClass? testNull = null; // make all reference type nullable now
                    testNull?.PrintSomething("Hello WOrld");
                    Console.WriteLine($"TestClass test = null;\r\ntest?.PrintSomething(\"Hello WOrld\");\r\nTestClass? testNull = null;\r\ntestNull?.PrintSomething(\"Hello WOrld\");");
                }
                else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    //Calling the Sync Print
                    Console.WriteLine("Starting with the current thread");
                    Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Result is {tc.SyncPrint(fastenal)}");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                }
                else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
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
    }
}
