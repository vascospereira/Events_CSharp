using System;

namespace ConsoleApplication2
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Counter c = new Counter();
            Random rnd = new Random();
            int threshold = rnd.Next(1, 10);
            c.Threshold = threshold;

            c.ThresholdReached += c_Thresholdreached;

            Console.WriteLine("Press 'a' to add one to total.");
            while (Console.ReadKey(true).KeyChar == 'a' && c.Threshold > c.Total)
            {
                Console.WriteLine("Adding one...");
                c.Add(1);
            }
        }

        private static void c_Thresholdreached(object sender, ThresholdEventArgs e)
        {
            Console.WriteLine($"Threshold value {e.Value} at time {e.TimeReached}.");
        }

        private class Counter
        {
            public int Threshold { get; set; }
            public int Total { get; private set; }

            public event EventHandler<ThresholdEventArgs> ThresholdReached;

            public void Add(int value)
            {
                Total += value;

                if (Total < Threshold) return;
                ThresholdEventArgs t =
                    new ThresholdEventArgs() { Value = Threshold, TimeReached = DateTime.Now };
                OnThresholdreached(t);
            }

            private void OnThresholdreached(ThresholdEventArgs threshold)
            {
                ThresholdReached?.Invoke(this, threshold);
            }
        }

        private class ThresholdEventArgs : EventArgs
        {
            public int Value { get; set; }
            public DateTime TimeReached { get; set; }
        }
    }
}
