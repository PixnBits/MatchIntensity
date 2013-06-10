using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Rally
    {
        protected int  shots           = 0;
        protected long millisEnd_match = 0;
        /*
        protected long millisStart     = 0;
        protected long millisEnd       = 0;

        public long start( long req = 0){
            if (0 == req)
            {
                req = DateTime.Now.Ticks / 10000;
            }

            this.shots = 1; // starting shot

            Console.WriteLine( "starting new rally, req" + req);
            return this.millisStart = req;
        }

        public int end(long req = 0)
        {
            if (0 == req)
            {
                req = DateTime.Now.Ticks / 10000;
            }

            this.millisEnd = req;
            return this.getDuration();
        }

        public int getDuration()
        {
            long end = this.millisEnd;
            if (0 == end)
            {
                end = DateTime.Now.Ticks / 10000;
            }
            return (int)(end - this.millisStart);
        }


        public int addShot(){
            if( (this.millisStart > 0) && (this.millisEnd == 0)){
                return this.shots += 1;
            }else{
                return 0;
            }
        }

        public int getDurationSinceEnd()
        {
            if (this.millisEnd == 0)
            {
                return 0;
            }

            long cur = (DateTime.Now.Ticks / 10000);
            if(this.millisEnd_match > 0)
                cur = this.millisEnd_match;

            return (int)(cur - this.millisEnd);
        }
        
        public string getStartTime()
        {
            return new DateTime(this.millisStart * 10000).ToShortTimeString();
        }

        internal long getStart()
        {
            return this.millisStart;
        }

        internal long getEnd()
        {
            return this.millisEnd;
        }

        internal string getFinishTime()
        {
            return new DateTime(this.millisEnd * 10000).ToShortTimeString();
        }/*/
        protected long[] millisStart;//     =  [0,0,0,0,0,0,0,0];
        protected long[] millisEnd;//       =  0;
        protected int    millisPointer   = -1; // C# is higher-level than C </sarcasm>
                                              // no, Lists<> don't count

        public long start( long req = 0){
            if (0 == req)
            {
                req = DateTime.Now.Ticks / 10000;
            }

            this.shots = 1; // starting shot


            if (this.millisPointer++ < 0)
            {
                this.millisStart = new long[1];
                this.millisEnd = new long[1];
            }else{
                long[] millisTemp = new long[this.millisPointer+1];
                this.millisStart.CopyTo(millisTemp, 0);
                this.millisStart = new long[this.millisPointer+1];
                millisTemp.CopyTo(this.millisStart, 0);

                this.millisEnd.CopyTo(millisTemp, 0);
                this.millisEnd = new long[this.millisPointer+1];
                millisTemp.CopyTo(this.millisEnd, 0);
            }

            Console.WriteLine( "starting new rally, req" + req);
            return this.millisStart[this.millisPointer] = req;
        }

        public int end(long req = 0)
        {
            if (0 == req)
            {
                req = DateTime.Now.Ticks / 10000;
            }
            if (this.millisPointer < 0)
                this.start(req);

            this.millisEnd[this.millisPointer] = req;
            return this.getDuration();
        }

        public int getDuration()
        {
            if (this.millisPointer < 0)
                return 0;

            long sum = 0;
            for (int i = 0; i < this.millisPointer; i++)
            {
                sum += this.millisEnd[i] - this.millisStart[i];
            }

            long end = this.millisEnd[this.millisPointer];
            if (0 == end)
            {
                end = DateTime.Now.Ticks / 10000;
            }
            sum += end - this.millisStart[this.millisPointer];

            return (int)(sum);
        }


        public int addShot(){
            if( (this.millisPointer >= 0) && (this.millisEnd[this.millisPointer] == 0)){
                return this.shots += 1;
            }else{
                return this.shots;
            }
        }

        public int getDurationSinceEnd()
        {
            if (this.millisPointer < 0)
                this.start();

            if (this.millisEnd[this.millisPointer] == 0)
            {
                return 0;
            }

            long cur = (DateTime.Now.Ticks / 10000);
            if(this.millisEnd_match > 0)
                cur = this.millisEnd_match;

            return (int)(cur - this.millisEnd[this.millisPointer]);
        }

        public int pause()
        {
            if( this.millisPointer < 0)
                this.start();

            //throw new NotImplementedException();
            return (int)(this.millisEnd[this.millisPointer] = (DateTime.Now.Ticks / 10000));
        }

        public long resume()
        {
            return this.start();
        }

        public string getStartTime()
        {
            if(this.millisPointer < 0)
                return "not started";

            return new DateTime(this.millisStart[0] * 10000).ToShortTimeString();
        }

        internal long getStart()
        {
            if (this.millisPointer < 0)
                return 0;

            return this.millisStart[0];
        }

        internal long getEnd()
        {
            if (this.millisPointer < 0)
                return 0;

            return this.millisEnd[this.millisPointer];
        }

        internal string getFinishTime()
        {
            if (this.millisPointer < 0)
                return "not started";

            return new DateTime(this.millisEnd[this.millisPointer] * 10000).ToShortTimeString();
        }
        //*/

        internal void endFinality()
        {
            // called at the end of the match
            this.millisEnd_match = DateTime.Now.Ticks / 10000;
        }

        public int getShots()
        {
            return this.shots;
        }

        internal void adjustShots(int adjustment)
        {
            this.shots += adjustment;
            if (this.shots < 1)
                this.shots = 1;
        }
    }
}
