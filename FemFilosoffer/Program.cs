using System;
using System.Diagnostics;

namespace FemFilosoffer
{
    public class Program
    {
        private readonly string[] names = { "Kevin", "Peter", "Kasper", "Martin", "Allan" };
        private Philosopher[] philosophers;
        private Chopstick[] chopsticks;

        public static void Main(string[] args)
        {
            Program program = new();
            program.Run(args);
        }

        public void Run(string[] args)
        {
            chopsticks = GenerateChopsticks(names.Length);
            philosophers = new Philosopher[names.Length];

            Stopwatch watch = new();

            // MULTITHREADED
            watch.Start();
            for (int i = 0; i < names.Length; i++)
            {
                Chopstick leftChopstick = chopsticks[i == 0 ? names.Length - 1 : i - 1];
                Chopstick rightChopstick = chopsticks[i];

                philosophers[i] = new Philosopher(names[i], leftChopstick, rightChopstick);
            }

            for (int i = 0; i < philosophers.Length; i++)
            {
                philosophers[i].WaitUntilDone();
            }

            watch.Stop();
            long multithreadedTime = watch.ElapsedMilliseconds;

            // SEQUENTIAL
            watch.Restart();
            for (int i = 0; i < names.Length; i++)
            {
                Chopstick leftChopstick = chopsticks[i == 0 ? names.Length - 1 : i - 1];
                Chopstick rightChopstick = chopsticks[i];

                philosophers[i] = new Philosopher(names[i], leftChopstick, rightChopstick);
                philosophers[i].WaitUntilDone();
            }
            watch.Stop();
            long sequentialTime = watch.ElapsedMilliseconds;

            // WRITE TIMES
            Console.WriteLine("Multithreaded: " + multithreadedTime);
            Console.WriteLine("Sequential: " + sequentialTime);

            Console.ReadLine();
        }

        private static Chopstick[] GenerateChopsticks(int count)
        {
            Chopstick[] chopsticks = new Chopstick[count];
            for (int i = 0; i < chopsticks.Length; i++)
            {
                chopsticks[i] = new Chopstick();
            }
            return chopsticks;
        }
    }
}