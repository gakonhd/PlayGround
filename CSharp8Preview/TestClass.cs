using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8Preview
{
    public class TestClass
    {
        public void PrintSomething(string s) => Console.WriteLine(s);

        /*
         * Normal sync with collection 
         */
        public string SyncPrint(string s)
        {
            Console.WriteLine("SyncPrint is called");
            var item = handle(s);

            foreach (var c in item.Item1)
            {
                item.Item2.Append(c);
            }

            return item.Item2.ToString();
        }

        /*
         * Sync call w yield
         */
        public IEnumerable<string> YieldSyncPrint(string s)
        {
            Console.WriteLine("YieldSyncPrint is called");
            var item = handle(s);

            foreach (var c in item.Item1)
            {
                yield return item.Item2.Append(c).ToString();
            }
        }

        /*
         * Async call starts
         */
        public async Task<string> AsyncPrint(string s)
        {
            Console.WriteLine("AsyncPrint is called");
            var item = handle(s);

            var res = await Task.Run(() =>
            {
                foreach (var c in item.Item1)
                {
                    item.Item2.Append(c);
                }

                return item.Item2.ToString();
            });
            return res;
        }

        /*
         * Async call with Enumeration
         */
        public async Task<IEnumerable<string>> AsyncEnumPrint(string s)
        {
            Console.WriteLine("AsyncEnumPrint is called");
            var item = handle(s);
            var stringCollection = new List<string>();

            var res = await Task.Run(() =>
            {
                foreach (var c in item.Item1)
                {
                    stringCollection.Add(item.Item2.Append(c).ToString());
                }

                return stringCollection;
            });

            return res;
        }

        /*
         * The name says it all
         */
        //public async IAsyncEnumerable<string> AsyncWithYield(string s)
        //{
        //    Console.WriteLine("AsyncWithYield is called");
        //    var item = handle(s);

        //    foreach (var c in item.Item1)
        //    {
        //        await Task.Delay(1234);
        //        yield return item.Item2.Append(c).ToString();
        //    }
        //}

        private Tuple<char[], StringBuilder> handle(string s)
        {
            return new Tuple<char[], StringBuilder>(s.ToCharArray(), new StringBuilder());
        }
    }
}
