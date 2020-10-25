using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alarm
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            clock.AlarmTime = new ClockTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second+10);
            clock.AlarmEvent += Clock_AlarmEvent;
            clock.TickEvent += Clock_TickEvent;
            new Thread(clock.Run).Start();
        }

        private static void Clock_TickEvent(Clock sender)
        {
            ClockTime currentTime = sender.CurrentTime;
            // DateTime dateTime = new DateTime(currentTime.Hour,currentTime.Minute,currentTime.Second);
            Console.WriteLine($"TickEvent:{currentTime.Hour}:{currentTime.Minute}:{currentTime.Second}");
        }

        private static void Clock_AlarmEvent(Clock sender)
        {
            ClockTime currentTime = sender.CurrentTime;
            Console.WriteLine($"AlarmEvent:{currentTime.Hour}:{currentTime.Minute}:{currentTime.Second}");
           // throw new NotImplementedException();
        }
    }
}
