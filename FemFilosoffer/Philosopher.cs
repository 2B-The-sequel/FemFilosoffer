using System;
using System.Threading;

namespace FemFilosoffer
{
    internal class Philosopher
    {
        private readonly object leftChopstick;
        private readonly object rightChopstick;

        private readonly string name;
        private int eatCount = 0;

        public Philosopher(string name, object left, object right)
        {
            this.name = name;
            leftChopstick = left;
            rightChopstick = right;
        }

        public void Eat()
        {
            while (eatCount < 10)
            {
                Random random = new();
                TimeSpan timeout = TimeSpan.FromMilliseconds(random.Next(0,100));
                
                bool leftTaken = false;
                bool rightTaken = false;
                
                // TRY LOCK LEFT
                try
                {
                    Monitor.TryEnter(leftChopstick, timeout, ref leftTaken);
                    if (leftTaken)
                    {
                        Thread.Sleep(500);

                        // TRY LOCK RIGHT
                        try
                        {
                            Monitor.TryEnter(rightChopstick, timeout, ref rightTaken);
                            if (rightTaken)
                            {
                                // EAT
                                eatCount++;
                                Thread.Sleep(10);
                                Console.WriteLine($"{name} has eaten: {eatCount} times");
                            }
                        }
                        finally
                        {
                            // UNLOCK RIGHT
                            if (rightTaken)
                            {
                                Monitor.Exit(rightChopstick);
                            }
                        }
                    }
                }
                finally
                {
                    // UNLOCK LEFT
                    if (leftTaken)
                    {
                        Monitor.Exit(leftChopstick);
                    }
                }

                // SLEEP
                Thread.Sleep(10);
            }

        }

        /*public void Eat()
        {
            while (eatCount < 10) {
                lock (leftChopstick)
                {
                    Thread.Sleep(500);
                    lock (rightChopstick)
                    {
                        eatCount++;
                        Thread.Sleep(10);
                        Console.WriteLine($"{name} has eaten: {eatCount} times");
                    }
                }
                Thread.Sleep(10);
            }
        }*/
    }
}
