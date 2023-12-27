using System;


namespace Assets.src.state2
{
    public sealed class Time
    {
        public long TimeMs { get; private set; } = 0;

        public void ChangeTime_Instantaneous(long t)
        {
            TimeMs = t;
            //TODO: Generate all timelines and snapshots
            // ... or  not? Who is in charge?
        }

        public static DateTime ToDate(long t)
        {
            return new DateTime(1970,1,1,0,0,0)
                .AddYears(-20) //Our game starts in 1950
                .AddMilliseconds(t);
        }
    }
}
