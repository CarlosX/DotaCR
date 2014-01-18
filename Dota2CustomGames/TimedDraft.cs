using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dota2CustomRealms
{
    abstract class TimedDraft
    {

        /// <summary>
        /// Needs to be synced between clients to ensure all random outcomes are the same without having to doublecheck (although we should of course doublecheck before the drfat ends)
        /// </summary>
        public static Random SharedRandom = new Random();

        public static Random NormalRandom = new Random();

        private long MillisecondsGiven = 30000;
        private Stopwatch timeRemaining = new Stopwatch();

        public long TimeRemaining
        {
            get { return MillisecondsGiven - timeRemaining.ElapsedMilliseconds; }
            set
            {
                MillisecondsGiven = value;
                timeRemaining.Reset();
                timeRemaining.Start();
            }
        }
    }
}
