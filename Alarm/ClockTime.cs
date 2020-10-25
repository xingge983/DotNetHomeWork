using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm
{
    class ClockTime
    {
        private int hour, minute, second;
       // readonly DateTime now = DateTime.Now;
        public ClockTime(int hour=0,int minute=0,int second = 0)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public int Hour
        {
            get
            {
                return hour;
            }
            set
            {
                if (value < 0 || value > 24) throw new ArgumentOutOfRangeException("invalid hour");
                hour = value;
            }
        }

        public int Minute
        {
            get
            {
                return minute;
            }
            set
            {
                if (value < 0 || value > 60) throw new ArgumentOutOfRangeException("invalid minute");
                minute = value;
            }
        }

        public int Second
        {
            get
            {
                return second;
            }
            set
            {
                if (value < 0 || value > 60) throw new ArgumentOutOfRangeException("invalid second");
                second = value;
            }
        }

        public override bool Equals(object obj)
        {
            var time = obj as ClockTime; //obj不能转换成ClockTime类型时，time=null 
            return time != null && Hour == time.hour && Minute == time.minute && Second == time.second;
        }
    }
}
