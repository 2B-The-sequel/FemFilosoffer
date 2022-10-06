using System;
using System.Threading;

namespace FemFilosoffer
{
    internal class Philosopher
    {
        private readonly Chopstick leftChopstick;
        private readonly Chopstick rightChopstick;

        private readonly string name;
        private int eatCount = 0;

        private readonly Thread thread;

        public Philosopher(string name, Chopstick left, Chopstick right)
        {
            this.name = name;
            leftChopstick = left;
            rightChopstick = right;

            thread = new(Live);
            thread.Start();
        }

        public void Live()
        {
            while (eatCount < 10)
            {
                bool left = leftChopstick.Take();
                Thread.Sleep(10);
                if (left)
                {
                    bool right = rightChopstick.Take();
                    
                    if (right)
                    {
                        Eat();
                        leftChopstick.HandIn();
                        rightChopstick.HandIn();
                    }
                }
                else
                    leftChopstick.HandIn();

                Rest();
            }

        }

        public void Eat()
        {
            eatCount++;
            Thread.Sleep(10);
            Console.WriteLine($"{name} has eaten: {eatCount} times");
        }

        public void Rest()
        {
            Random rnd = new();
            Thread.Sleep(rnd.Next(0,100));
        }

        public void WaitUntilDone()
        {
            thread.Join();
        }
    }
}
