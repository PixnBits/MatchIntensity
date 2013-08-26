using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Game
    {
        private Rally[] rallies;
        private int currentRally = -1;

        public void start()
        {
            this.rallies = new Rally[59];
            this.rallies[0] = new Rally();

            this.rallies[0].start();
            this.currentRally = 0;
        }

        public int startRally()
        {
            if (this.rallies == null)
            {
                this.start();
            }
            else
            {
                this.currentRally++;
                
                //TODO if currentRally > array, we need to send some feedback (new Match)
                if (this.currentRally >= this.rallies.Length)
                {
                    return --this.currentRally;
                }

                this.rallies[this.currentRally] = new Rally();
                this.rallies[this.currentRally].start();
            }

            Console.WriteLine("starting new rally, inside Game.cs");
            return this.currentRally;
        }

        public int addShot()
        {
            return this.rallies[this.currentRally].addShot();
        }

        public Rally getCurrentRally()
        {
            if (this.rallies == null)
                return null;

            return this.rallies[this.currentRally];
        }

        public void endRally()
        {
            this.rallies[this.currentRally].end();
        }

        public int getCurrentRallyNumber(){
            return this.currentRally + 1;
        }

        public string getStartTime()
        {
            return this.rallies[0].getStartTime();
        }

        internal string getFinishTime()
        {
            return this.rallies[this.currentRally].getFinishTime();
        }

        internal int getTotalShots()
        {
            if (this.rallies == null)
                return 0;
            int sum = 0;
            for (int i = 0; i <= this.currentRally; i++)
            {
                sum += this.rallies[i].getShots();
            }

            return sum;
        }

        internal int getTotalRallies()
        {
            return this.currentRally + 1;
        }

        internal int getLongestRally()
        {
            int millis = 0;
            for (int i = 0; i <= this.currentRally; i++)
            {
                int milliNew = this.rallies[i].getDuration();
                if (milliNew > millis)
                    millis = milliNew;
            }

            return millis;
        }

        internal int getPlayTime()
        {
            int milliSum = 0;
            for (int i = 0; i <= this.currentRally; i++)
            {
                milliSum += this.rallies[i].getDuration();
            }

            return milliSum;
        }

        internal long getBreakTime()
        {
            long milliSum = 0;
            long start = 0;
            long end = 0;


            for (int i = 0; i <= this.currentRally; i++)
            {
                start = this.rallies[i].getEnd();
                if(this.rallies[i+1] != null)
                    end = this.rallies[i+1].getStart();

                Console.WriteLine("The break between rally " + (i) + " and " + (i+1) + " started at " + start + "and ended at " + end);

                if (0 == end)
                    end = DateTime.Now.Ticks / 10000;
                // NOT WORKING
                if( start > 0)
                    milliSum += end - start;
            }

            //Console.WriteLine("getBreakTime called at " + DateTime.Now.TimeOfDay);

            return milliSum;
        }

        internal int getShotsPerRally(int lowerBound, int upperBound)
        {
            int sum = 0;
            for (int i = 0; i <= this.currentRally; i++)
            {
                if (this.rallies[i].getShots() >= lowerBound && this.rallies[i].getShots() <= upperBound)
                    sum++;
            }

            return sum;
        }

        internal float[] getLongestRallies()
        {
            float[] rallyList = new float[this.currentRally + 1];
            for (int i = 0; i <= this.currentRally; i++)
            {
                rallyList[i] = this.rallies[i].getDuration();
            }

            Array.Sort(rallyList);
            Array.Reverse(rallyList);
            Array.Resize(ref rallyList, 4);

            return rallyList;
        }

        internal void end()
        {
            //this.endRally();
            //TODO anything else for cleanup?
            //throw new NotImplementedException();
        }

        internal Rally getRally(int rallyIndex)
        {
            return this.rallies[rallyIndex];
        }

        internal long getRestTime(int index)
        {
            if (index >= this.rallies.Length)
                return 0;
            if (this.rallies[index + 1] == null)
                return 0;

            return this.rallies[index + 1].getStart() - this.rallies[index].getEnd();
        }

        public long getTotalRestTime()
        {
            long sum = 0;

            for (int i = 0; i < this.currentRally; i++)
            {
                sum += this.getRestTime(i);
            }

            return sum;
        }

        internal long getTotalPlayTime()
        {
            long sum = 0;

            for (int i = 0; i <= this.currentRally; i++)
            {
                long ere = this.getPlayTime2(i);
                //sum += this.getPlayTime2(i);
                sum += ere;
            }

            return sum;
        }

        private long getPlayTime2(int index)
        {
            if (index > this.rallies.Length)
                return 0;
            if (this.rallies[index] == null)
                return 0;

            long end = this.rallies[index].getEnd();
            long start = this.rallies[index].getStart();
            long diff = end - start;

            return this.rallies[index].getEnd() - this.rallies[index].getStart();
        }

        internal long getTotalTime()
        {
            return this.getTotalPlayTime() + this.getTotalRestTime();
        }

        internal void adjustLastRallysShots(int adjustment)
        {
            if (this.currentRally < 0)
                return;

            this.rallies[this.currentRally].adjustShots(adjustment);
        }

        internal void pause()
        {
            if (this.currentRally < 0)
                return;

            this.rallies[this.currentRally].pause();
        }

        internal void resume()
        {
            if (this.currentRally < 0)
                return;

            this.rallies[this.currentRally].resume();
        }
    }
}
