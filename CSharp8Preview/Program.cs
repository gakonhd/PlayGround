using System;
using System.Threading;

namespace CSharp8Preview
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            const string fastenal = "FASTENAL";
            //string myNullString = null;
            //Console.WriteLine($"Get something from null string {myNullString[0]}");

            //string? myNullableString = null; // make string nullable
            //Console.WriteLine($"Get something from nullable string {myNullableString[0]}");

            //TestClass test = null;
            //test?.PrintSomething("Hello WOrld");

            //TestClass? testNull = null; // make all reference type nullable now
            //testNull?.PrintSomething("Hello WOrld");

            var tc = new TestClass();
            //Calling the Sync Print
            Console.WriteLine("Starting with the current thread");
            Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Result is {tc.SyncPrint(fastenal)}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            
            //Calling the Sync Print W Yield
            Console.WriteLine("Starting with the current thread");
            Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
            foreach(var c in tc.YieldSyncPrint(fastenal))
            {
                Console.WriteLine($"Current string is {c} - Thread id is {Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine($"Finished with the yield sync - Thread id is {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();

            //Calling the Async Print
            Console.WriteLine("Starting with the current thread");
            Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Result is {await tc.AsyncPrint(fastenal)} - Thread id is {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Finished with the async print - Thread id is {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();

            //Calling the Async Print
            Console.WriteLine("Starting with the current thread");
            Console.WriteLine($"Thread id {Thread.CurrentThread.ManagedThreadId}");
            var res = await tc.AsyncEnumPrint(fastenal);
            Console.WriteLine($"Finished with the async enum print - Thread id is {Thread.CurrentThread.ManagedThreadId}");
            foreach(var c in res)
            {
                Console.WriteLine($"Result is {c} - Thread id is {Thread.CurrentThread.ManagedThreadId}");
            }
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();

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

            Console.Read();
        }
    }
}
