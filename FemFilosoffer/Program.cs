using System;
using System.Threading;
using System.Diagnostics;

namespace FemFilosoffer
{
    public class Program
    {
        private static Thread[] threads;
        private static Philosopher[] philosophers;
        private static readonly string[] names = { "Kevin", "Peter", "Kasper", "Martin", "Allan" };
        private static object[] chopsticks;

        public static void Main(string[] args)
        {
            chopsticks = new object[names.Length];
            threads = new Thread[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                chopsticks[i] = new object();
            }

            philosophers = new Philosopher[names.Length];

            Stopwatch watch = new();

            // MULTITHREADED
            watch.Start();
            for (int i = 0; i < names.Length; i++)
            {
                object leftChopstick = chopsticks[i == 0 ? names.Length-1 : i-1];
                object rightChopstick = chopsticks[i];

                philosophers[i] = new Philosopher(names[i], leftChopstick, rightChopstick);
                threads[i] = new(philosophers[i].Eat);
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            watch.Stop();
            long multithreadedTime = watch.ElapsedMilliseconds;

            // SEQUENTIAL
            watch.Restart();
            for (int i = 0; i < names.Length; i++)
            {
                object leftChopstick = chopsticks[i == 0 ? names.Length - 1 : i - 1];
                object rightChopstick = chopsticks[i];

                philosophers[i] = new Philosopher(names[i], leftChopstick, rightChopstick);
                philosophers[i].Eat();
            }
            watch.Stop();
            long sequentialTime = watch.ElapsedMilliseconds;

            // WRITE TIMES
            Console.WriteLine("Multithreaded: " + multithreadedTime);
            Console.WriteLine("Sequential: " + sequentialTime);

            Console.ReadLine();
        }
    }
}