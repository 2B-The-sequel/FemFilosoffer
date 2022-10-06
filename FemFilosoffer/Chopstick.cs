namespace FemFilosoffer
{
    public class Chopstick
    {
        private bool available = true;
        private readonly object _lock = new();

        public bool Take()
        {
            bool taken = false;
            if (available)
            {
                lock (_lock)
                {
                    if (available)
                    {
                        available = false;
                        taken = true;
                    }
                }
            }
            return taken;
        }

        public void HandIn()
        {
            if (!available)
            {
                lock (_lock)
                {
                    if (!available)
                    {
                        available = true;
                    }
                }
            }
        }
    }
}
