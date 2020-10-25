using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alarm
{
    class Clock
    {
        public delegate void AlarmEventHandler(Clock sender);
        public delegate void TickEventHandler(Clock sender);
        public event AlarmEventHandler AlarmEvent;
        public event TickEventHandler TickEvent;

        public Clock()
        {
            CurrentTime = new ClockTime();
        }

        public ClockTime CurrentTime { get; set; }
        public ClockTime AlarmTime { get; set; }

        public void Run()
        {
            while (true)
            {
                //ClockTime CurrentTime = new ClockTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                CurrentTime = new ClockTime(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TickEvent(this);
                if (AlarmTime.Equals(CurrentTime))
                {
                    AlarmEvent(this);
                }
                Thread.Sleep(1000);
            }
        }
    }
}
