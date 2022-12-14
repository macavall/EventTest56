using System.Diagnostics.Metrics;

namespace EventTest56
{
    class ProgramThree
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            Counter c = new Counter();
            c.ThresholdReached += c_ThresholdReached;

            Thread.Sleep(3000);

            c.ThrowEvent();
        }

        static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The Event has fired with Data: " + e.myData);
            Environment.Exit(0);
        }
    }

    class Counter
    {
        public void ThrowEvent()
        {
            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
            args.myData = "My_Data";
            OnThresholdReached(args);
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
    }

    public class ThresholdReachedEventArgs : EventArgs
    {
        public string myData { get; set; }
    }
}