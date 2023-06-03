using System;

namespace superagent
{
    public class TimerControl
    {
        public bool start;
        protected int milliSec;
        protected TimeSpan timer = new TimeSpan();
        
        public TimerControl(int sec)
        {
            start = false;
            milliSec = sec;
        }

        public void UpdateTimer()
        {
            timer += GeneralVariable.GameTime.ElapsedGameTime;
        }

        public bool Test()
        {
            if(timer.TotalMilliseconds >= milliSec || start) return true; 
            else return false; 
        }
    }
}
