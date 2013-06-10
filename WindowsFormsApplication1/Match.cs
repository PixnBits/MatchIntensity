using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Match
    {
        private Game[] games = new Game[3];
        private int currentGame = -1;

        public DateTime data_date;
        public string data_enterer;
        public string data_event;
        public string data_location;
        public string data_playerA;
        public string data_playerB;
        // winner, score?

        public void startGame()
        {
            //check for 4 or more
            if (this.currentGame >= 2)
                return;

            this.currentGame++;
            this.games[this.currentGame] = new Game();
        }

        public void startRally()
        {
            if (this.currentGame < 0)
                this.startGame();
            Console.WriteLine("starting new rally, inside Match.cs");
            this.games[this.currentGame].startRally();
        }

        public void updateRally(
            System.Windows.Forms.Control number,
            System.Windows.Forms.Control shots,
            System.Windows.Forms.Control seconds,
            System.Windows.Forms.Control shotsSec,
            System.Windows.Forms.Control secAfter
            )
        {
            Rally curRally = this.games[this.currentGame].getCurrentRally();
            if (curRally != null)
            {
                //number.Text += curRally.getDuration();
                float curDur = (float)curRally.getDuration();
                shots.Text = "" + curRally.getShots();
                seconds.Text = (curDur / (1000)).ToString("0.00");
                shotsSec.Text = (curRally.getShots() / (curDur / (1000))).ToString("0.00");
                secAfter.Text = ((float)curRally.getDurationSinceEnd() / (1000)).ToString("0.00");
            }
        }

        public int addShot()
        {
            if (this.games[this.currentGame] != null)
            {
                return this.games[this.currentGame].addShot();
            }

            return -1;
        }

        public void endRally()
        {
            this.games[this.currentGame].endRally();
        }

        public int getCurrentRallyNumber()
        {
            return this.games[this.currentGame].getCurrentRallyNumber();
        }

        public void updateMatchStats(
            System.Windows.Forms.Control ui_winner,
            System.Windows.Forms.Control ui_score,
            System.Windows.Forms.Control ui_startTime,
            System.Windows.Forms.Control ui_finishTime,
            System.Windows.Forms.Control ui_games,
            System.Windows.Forms.Control ui_shots,
            System.Windows.Forms.Control ui_rallies,
            System.Windows.Forms.Control ui_avgShotsRally,
            System.Windows.Forms.Control ui_longestRally,
            System.Windows.Forms.Control ui_timeShot,
            System.Windows.Forms.Control ui_totalMatchTime,
            System.Windows.Forms.Control ui_playTime,
            System.Windows.Forms.Control ui_rallyBreakTime,
            System.Windows.Forms.Control ui_avgRallyBreakTime,
            System.Windows.Forms.Control ui_gameBreakTime,
            System.Windows.Forms.Control ui_percentPlayTotalTime,
            System.Windows.Forms.Control ui_matchIntesityValue,
            System.Windows.Forms.Control ui_shotsPerRally_30Plus,
            System.Windows.Forms.Control ui_shotsPerRally_20To29,
            System.Windows.Forms.Control ui_shotsPerRally_10to19,
            System.Windows.Forms.Control ui_shotsPerRally_5To9,
            System.Windows.Forms.Control ui_shotsPerRally_1To4,
            System.Windows.Forms.Control ui_longestRally_2,
            System.Windows.Forms.Control ui_longestRally_3,
            System.Windows.Forms.Control ui_longestRally_4
            )
        {
            // update the output
            // yes, it's not good to rewrite the starting text every time, but it's faster for dev
            // can come back and fix that when all the req. features for the tourney are implemented
            // TODO fix accr. to comment above (VisStudio doesn't recognize TODO tags like Eclipse, does Redmine?)
            //ui_winner
            //ui_score
            //ui_startTime
            ui_startTime.Text = "Match Start Time: " + this.games[0].getStartTime();
            //ui_finishTime
            if (this.games[2] != null)
                ui_startTime.Text = "Finish Time: " + this.games[0].getFinishTime();
            //ui_games
            ui_games.Text = "# Games:" + (this.currentGame + 1);
            //ui_shots
            int totalShots = this.getTotalShots();
            ui_shots.Text = "# Shots: " + totalShots;
            //ui_rallies
            int totalRallies = this.getTotalRallies();
            ui_rallies.Text = "# Rallies: " + totalRallies;
            //ui_avgShotsRally
            ui_avgShotsRally.Text = "Avg. Shots/Rally: " + (totalShots / totalRallies).ToString("0.00");
            //ui_longestRally
            float[] longestRallies = this.getLongestRallies();
            ui_longestRally.Text = "Longest Rally: " + (longestRallies[0] / 1000).ToString("0.00");
            //ui_timeShot
            //ui_totalMatchTime
            ui_totalMatchTime.Text = "Total Match Time: " + (this.getTotalTime());// new DateTime
            //ui_playTime
            ui_playTime.Text = "Play Time: " + (this.getPlayTime());// new DateTime
            //ui_rallyBreakTime
            ui_rallyBreakTime.Text = "Rally Break Time: " + (this.getBreakTime());// new DateTime
            //ui_avgRallyBreakTime
            //ui_gameBreakTime
            //ui_percentPlayTotalTime
            //ui_matchIntesityValue
            //ui_shotsPerRally_30Plus
            ui_shotsPerRally_30Plus.Text = "30+: " + this.getShotsPerRally(30, Int32.MaxValue);
            //ui_shotsPerRally_20To29
            ui_shotsPerRally_20To29.Text = "20-29: " + this.getShotsPerRally(20, 29);
            //ui_shotsPerRally_10to19
            ui_shotsPerRally_10to19.Text = "10-19: " + this.getShotsPerRally(10, 19);
            //ui_shotsPerRally_5To9
            ui_shotsPerRally_5To9.Text = "5-9: " + this.getShotsPerRally(5, 9);
            //ui_shotsPerRally_1to4
            ui_shotsPerRally_1To4.Text = "1-4: " + this.getShotsPerRally(1, 4);
            //ui_longestRally_2
            ui_longestRally_2.Text = "2nd Longest: " + (longestRallies[1] / 1000).ToString("0.00");
            //ui_longestRally_3
            ui_longestRally_3.Text = "3rd Longest: " + (longestRallies[2] / 1000).ToString("0.00");
            //ui_longestRally_4
            ui_longestRally_4.Text = "4th Longest: " + (longestRallies[3] / 1000).ToString("0.00");


            //ui_winner
            //ui_score
            //ui_startTime
            //ui_finishTime
            //ui_games
            //ui_shots
            //ui_rallies
            //ui_avgShotsRally
            //ui_longestRally
            //ui_timeShot
            //ui_totalMatchTime
            //ui_playTime
            //ui_rallyBreakTime
            //ui_avgRallyBreakTime
            //ui_gameBreakTime
            //ui_percentPlayTotalTime
            //ui_matchIntesityValue
            //ui_shotsPerRally_30Plus
            //ui_shotsPerRally_20To29
            //ui_shotsPerRally_10to19
            //ui_shotsPerRally_5To9
            //ui_shotsPerRally_1to4
            //ui_longestRally_2
            //ui_longestRally_3
            //ui_longestRally_4
        }

        public float[] getLongestRallies()
        {
            float[] longestRallies = new float[4];

            for (int i = 0; i <= this.currentGame; i++)
            {
                float[] longestRallies_game = this.games[i].getLongestRallies();
                // http://stackoverflow.com/a/1547276
                float[] both = new float[longestRallies.Length + longestRallies_game.Length];
                longestRallies.CopyTo(both, 0);
                longestRallies_game.CopyTo(both, longestRallies.Length);
                Array.Sort(both);
                Array.Reverse(both);
                Array.Resize( ref both, 4);
                both.CopyTo(longestRallies, 0);
            }

            return longestRallies;
        }

        public int getShotsPerRally(int lowerBound, int upperBound)
        {
            int sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                sum += this.games[i].getShotsPerRally(lowerBound, upperBound);
            }
            return sum;
        }

        private long getTotalTime()
        {
            return this.getPlayTime() + this.getBreakTime();
        }

        private int getPlayTime()
        {
            int sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                //sum += this.games[i].getPlayTime();
                sum += (int)this.games[i].getTotalPlayTime();
            }
            return sum;
        }

        private long getBreakTime()
        {
            long sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                //sum += this.games[i].getBreakTime();
                sum += this.games[i].getTotalRestTime();
            }
            return sum;
        }

        private int getLongestRally()
        {
            int millis = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                int milliNew = this.games[i].getLongestRally();
                if (milliNew > millis)
                    millis = milliNew;
            }

            return millis;
        }

        private int getTotalRallies()
        {
            int sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                sum += this.games[i].getTotalRallies();
            }
            return sum;
        }

        private int getTotalShots()
        {
            int sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                sum += this.games[i].getTotalShots();
            }
            return sum;
        }

        internal void endGame()
        {
            this.games[this.currentGame].end();
            this.startGame();
        }

        internal int getCurrentGameNumber()
        {
            return this.currentGame + 1;
        }

        internal void endMatch()
        {
            if (this.games[this.currentGame].getCurrentRallyNumber() < 1)
            {
                this.games[this.currentGame] = null;
                this.currentGame--;
            }

            this.games[this.currentGame].getCurrentRally().endFinality();
        }

        internal Game getGame(int gameIndex)
        {
            return this.games[gameIndex];
        }

        internal string getStartTime()
        {
            if (this.games[0] == null)
                return "";

            return this.games[0].getStartTime();
        }

        internal string getFinishTime()
        {
            //Rally lastRally = this.games[this.currentGame].getCurrentRally();
            //long endTicks = lastRally.getEnd() * 10000;
            //DateTime dateTimeEnd = new DateTime(endTicks);
            //return 
            //throw new NotImplementedException();
            return this.games[this.currentGame].getFinishTime();
        }

        internal string getTotalMatchTime()
        {
            return (this.getTotalTime() / (long)1000).ToString("0.0");
        }

        internal int getTotalPlayTime()
        {
            int sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                sum += (int)this.games[i].getTotalPlayTime();
            }
            return sum;
        }

        internal long getTotalBreakTime()
        {
            //throw new NotImplementedException();
            return this.getBreakTime();
        }

        internal int getTotalRallyCount()
        {
            int sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                sum += this.games[i].getTotalRallies();
            }
            return sum;
        }

        internal int getTotalShotCount()
        {
            int sum = 0;
            for (int i = 0; i <= this.currentGame; i++)
            {
                sum += this.games[i].getTotalShots();
            }
            return sum;
        }

        internal void adjustLastRallysShots(int adjustment)
        {
            if (this.currentGame < 0)
                return;

            this.games[this.currentGame].adjustLastRallysShots(adjustment);
        }

        internal void pause()
        {
            if (this.currentGame < 0)
                return;

            this.games[this.currentGame].pause();
        }

        internal void resume()
        {
            if (this.currentGame < 0)
                return;

            this.games[this.currentGame].resume();
        }
    }
}
