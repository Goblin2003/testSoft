using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serverStatic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread writer = new Thread(() => Server.AddToCount(10));
            Thread reader1 = new Thread(() => Console.WriteLine("Читатель1:" + Server.GetCount()));
            Thread reader2 = new Thread(() => Console.WriteLine("Читатель2:" + Server.GetCount()));
            writer.Start();
            reader1.Start();
            reader2.Start();
        }
    }
    public static class Server
    {
        private static int count = 0;
        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        public static int GetCount()
        {
            _lock.EnterReadLock();
            try
            {
                return count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        public static void AddToCount(int value)
        {
            _lock.EnterWriteLock();
            try
            {
                count += value;
                Console.WriteLine($"Писатель добавил {value}");
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

    }
}

