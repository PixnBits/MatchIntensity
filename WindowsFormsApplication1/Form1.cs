using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MatchIntensityApplication;

namespace WindowsFormsApplication1
{
    public partial class frm_matchIntensity : Form
    {

        private const int RALLYS_PER_ROW = 22;

        // TODO this should probably be in the Match class, move it
        private const int DEBUG_canEndGameAfterRallyNumber = 2;//21;
        //private DateTime formStarted;

        public frm_matchIntensity()
        {
            InitializeComponent();
            this.matchData = new Match();

            matchData.data_date = DateTime.Now;
            lbl_date.Text = "Date: " + matchData.data_date.Date.ToShortDateString();

            this.editInfoToolStripMenuItem_Click(this, null);
        }

        private void btn_startRally_Click(object sender, EventArgs e)
        {

            this.matchData.startRally();
            this.timer_updateRally.Enabled = true;

            Console.WriteLine("columns: " + this.tbl_game1_a.ColumnCount);
            Console.WriteLine("column: " + this.tbl_game1_a.GetControlFromPosition(1,1));

            btn_startRally_disable();
            this.btn_addShot.Enabled = true;
            this.btn_EndRally.Enabled = true;
            this.btn_EndGame.Enabled = false;
            this.btn_pauseRally.Enabled = true;
            this.btn_AdjustMinus.Enabled = true;
            this.btn_AdjustPlus.Enabled = true;
            
            
        }

        private void timer_updateRally_Tick(object sender, EventArgs e)
        {
            if (this.matchData != null)
            {

                /*
                lbl_number
                lbl_shots
                lbl_seconds
                lbl_shotsSec
                lbl_secAfter
                 */
                int curGame = this.matchData.getCurrentGameNumber();
                int curRall = this.matchData.getCurrentRallyNumber();
                // curRall is 1 indexed, columns are 0 indexed, but col 0 has labels
                // so we need to decrement curRall to get 0 indexed, but increment result
                // so we don't overwrite the row labels
                int curRallIndex = (curRall - 1) % RALLYS_PER_ROW + 1;

                TableLayoutPanel currentTable;
                switch (curGame)
                {
                    case 1:
                        this.gBox_game1.Visible = true;
                        if (curRall <= RALLYS_PER_ROW)
                        {
                            currentTable = this.tbl_game1_a;
                        }
                        else if (curRall <= (2 * RALLYS_PER_ROW))
                        {
                            currentTable = this.tbl_game1_b;
                        }
                        else
                        {
                            currentTable = this.tbl_game1_c;
                        }
                        break;
                    case 2:
                        this.gBox_game2.Visible = true;
                        //pnl_games.ScrollControlIntoView(this.gBox_game2);
                        if (curRall <= RALLYS_PER_ROW)
                        {
                            currentTable = this.tbl_game2_a;
                        }
                        else if (curRall <= (2 * RALLYS_PER_ROW))
                        {
                            currentTable = this.tbl_game2_b;
                        }
                        else
                        {
                            currentTable = this.tbl_game2_c;
                        }
                        break;
                    case 3:
                        this.gBox_game3.Visible = true;
                        //pnl_games.ScrollControlIntoView(this.gBox_game3);
                        if (curRall <= RALLYS_PER_ROW)
                        {
                            currentTable = this.tbl_game3_a;
                        }
                        else if (curRall <= (2 * RALLYS_PER_ROW))
                        {
                            currentTable = this.tbl_game3_b;
                        }
                        else
                        {
                            currentTable = this.tbl_game3_c;
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }

                currentTable.Visible = true;
                currentTable.Enabled = true;

                System.Windows.Forms.Control lbl_number;
                System.Windows.Forms.Control lbl_shots;
                System.Windows.Forms.Control lbl_seconds;
                System.Windows.Forms.Control lbl_shotsSec;
                System.Windows.Forms.Control lbl_secAfter;

                

                Console.Write("current game ");
                Console.WriteLine(curGame);
                Console.Write("current rally index (in row)");
                Console.WriteLine(curRallIndex);

                lbl_number = currentTable.GetControlFromPosition(curRallIndex, 0);
                lbl_shots = currentTable.GetControlFromPosition(curRallIndex, 1);
                lbl_seconds = currentTable.GetControlFromPosition(curRallIndex, 2);
                lbl_shotsSec = currentTable.GetControlFromPosition(curRallIndex, 3);
                lbl_secAfter = currentTable.GetControlFromPosition(curRallIndex, 4);
                

                lbl_number.Visible = true;
                lbl_shots.Visible = true;
                lbl_seconds.Visible = true;
                lbl_shotsSec.Visible = true;
                lbl_secAfter.Visible = true;

                lbl_number.Text = "" + curRall;

                this.matchData.updateRally(lbl_number, lbl_shots, lbl_seconds, lbl_shotsSec, lbl_secAfter);
                lbl_number.Text = curRall.ToString();

                this.matchData.updateMatchStats(
                        this.lbl_winner,
                        this.lbl_score,
                        this.lbl_startTime,
                        this.lbl_finishTime,
                        this.lbl_games,
                        this.lbl_shots,
                        this.lbl_rallies,
                        this.lbl_avgShotsRally,
                        this.lbl_longestRally,
                        this.lbl_timeShot,
                        this.lbl_totalMatchTime,
                        this.lbl_playTime,
                        this.lbl_rallyBreakTime,
                        this.lbl_avgRallyBreakTime,
                        this.lbl_gameBreakTime,
                        this.lbl_percentPlayTotalTime,
                        this.lbl_matchIntensityValue,
                        this.lbl_shotsPerRally_30Plus,
                        this.lbl_shotsPerRally_20To29,
                        this.lbl_shotsPerRally_10To19,
                        this.lbl_shotsPerRally_5To9,
                        this.lbl_shotsPerRally_1To4,
                        this.lbl_longestRally_2,
                        this.lbl_longestRally_3,
                        this.lbl_longestRally_4
                    );
            }

            // update immediately :)
            //this.timer_updateRally_Tick(this, null);
        }

        private void btn_addShot_Click(object sender, EventArgs e)
        {
            this.matchData.addShot();
            // update immediately :)
            this.timer_updateRally_Tick(this, null);
        }

        private void btn_EndRally_Click(object sender, EventArgs e)
        {
            this.matchData.endRally();

            btn_startRally_enable();
            this.btn_addShot.Enabled = false;
            this.btn_EndRally.Enabled = false;
            this.btn_pauseRally.Enabled = false;
            this.btn_AdjustMinus.Enabled = true;
            this.btn_AdjustPlus.Enabled = true;

            if (this.matchData.getCurrentRallyNumber() >= DEBUG_canEndGameAfterRallyNumber)
                this.btn_EndGame.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            //Use to determine the Keys.* name of a Key
            //Console.WriteLine(e.KeyCode.ToString());
            //Console.WriteLine(e.KeyCode);
            switch (e.KeyCode)
            {
                case Keys.Z:
                    // start rally
                    if (this.btn_startRally.Enabled != true)
                        return;
                    Console.WriteLine("starting rally");
                    this.btn_startRally_Click(this, e);
                    break;
                case Keys.X:
                    // add shot
                    if (this.btn_addShot.Enabled != true)
                        return;
                    Console.WriteLine("adding shot");
                    btn_addShot_Click(this, null);
                    break;
                case Keys.P:
                    // pause/resume rally
                    if (this.btn_pauseRally.Enabled != true)
                        return;
                    Console.WriteLine("ending rally");
                    this.btn_pauseRally_Click(this, null);
                    break;
                case Keys.Space:
                    // end rally
                    if (this.btn_EndRally.Enabled != true)
                        return;
                    Console.WriteLine("ending rally");
                    this.btn_EndRally_Click(this, null);
                    break;
                case Keys.K:
                    // end game
                    if (this.btn_EndGame.Enabled != true)
                        return;
                    Console.WriteLine("ending game");
                    this.btn_EndGame_Click(this, null);
                    break;
                case Keys.L:
                    // end match
                    if (this.btn_EndMatch.Enabled != true)
                        return;
                    Console.WriteLine("ending match");
                    this.btn_EndMatch_Click(this, null);
                    break;
                case Keys.OemPeriod: // >
                    // adjust shots +1
                    if (this.btn_AdjustPlus.Enabled != true)
                        return;
                    Console.WriteLine("adjust shots +1");
                    this.btn_AdjustPlus_Click(this, null);
                    break;
                case Keys.Oemcomma:  // <
                    // adjust shots -1
                    if (this.btn_AdjustMinus.Enabled != true)
                        return;
                    Console.WriteLine("adjust shots -1");
                    this.btn_AdjustMinus_Click(this, null);
                    break;
                default:
                    Console.Write(e.KeyCode.ToString());
                    Console.WriteLine(" key not used");
                    //throw new NotImplementedException();
                    break;
            }
        }

        private void btn_EndGame_Click(object sender, EventArgs e)
        {
            this.matchData.endGame();
            this.btn_EndGame.Enabled = false;
            if (this.matchData.getCurrentGameNumber() > 2)
                this.btn_EndMatch.Enabled = true;
            if (this.matchData.getCurrentGameNumber() == 3 && this.matchData.getCurrentRallyNumber() > 0)
                this.btn_EndMatch_Click(this, null);

            // scrolling isn't effective if there's nothing (visible) inside the group box
            // so update the display (make contents visible or not) and then scroll to the next one
            if(this.matchData.getCurrentGameNumber() == 2){
                timer_updateRally_Tick(this, e);
                pnl_games.ScrollControlIntoView(this.gBox_game2);
            }
            if (this.matchData.getCurrentGameNumber() == 3)
            {
                timer_updateRally_Tick(this, e);
                pnl_games.ScrollControlIntoView(this.gBox_game3);
            }
        }

        private void editInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String Enterer, Event, Location, PlayerA, PlayerB;

            MatchInfo matchInfoDialog = new MatchInfo();
            matchInfoDialog.ShowForm(
                out Enterer,
                out Event,
                out Location,
                out PlayerA,
                out PlayerB,
                matchData.data_enterer,
                matchData.data_event,
                matchData.data_location,
                matchData.data_playerA,
                matchData.data_playerB
            );

            if (Enterer != null && Enterer.Length > 0)
                matchData.data_enterer = Enterer;
            if (Event != null && Event.Length > 0)
                matchData.data_event = Event;
            if (Location != null && Location.Length > 0)
                matchData.data_location = Location;
            if (PlayerA != null && PlayerA.Length > 0)
                matchData.data_playerA = PlayerA;
            if (PlayerB != null && PlayerB.Length > 0)
                matchData.data_playerB= PlayerB;

            if (matchData.data_enterer  == null || matchData.data_enterer.Length  < 1) { this.lbl_enterer.Text = "-";  } else { this.lbl_enterer.Text = matchData.data_enterer;   }
            if (matchData.data_event    == null || matchData.data_event.Length    < 1) { this.lbl_event.Text = "-";    } else { this.lbl_event.Text = matchData.data_event;       }
            if (matchData.data_location == null || matchData.data_location.Length < 1) { this.lbl_location.Text = "-"; } else { this.lbl_location.Text = matchData.data_location; }
            if (matchData.data_playerA  == null || matchData.data_playerA.Length  < 1) { this.lbl_playerA.Text = "-";  } else { this.lbl_playerA.Text = matchData.data_playerA;   }
            if (matchData.data_playerB  == null || matchData.data_playerB.Length  < 1) { this.lbl_playerB.Text = "-";  } else { this.lbl_playerB.Text = matchData.data_playerB;   }
        }

        private void btn_EndMatch_Click(object sender, EventArgs e)
        {
            // TODO: confirmation popup
            // disable all buttons
            btn_startRally_disable();
            this.btn_addShot.Enabled = false;
            this.btn_pauseRally.Enabled = false;
            this.btn_EndRally.Enabled = false;
            this.btn_AdjustMinus.Enabled = false;
            this.btn_AdjustPlus.Enabled = false;
            this.btn_EndGame.Enabled = false;
            this.btn_EndMatch.Enabled = false;
            // end all timers (unended rallies)
            this.matchData.endMatch();
            if (this.matchData.getCurrentGameNumber() == 2)
            {
                this.tbl_game3_a.Visible = false; // hide it if set to null
                this.gBox_game3.Visible = false;
            }
            // redraw stats
            this.timer_updateRally_Tick(this, null);
            // redraw rallies?

        }

        private void btn_startRally_enable()
        {
            this.btn_startRally.Enabled = true;
            this.btn_startRally.BackColor = Color.FromArgb(0, 192, 0);
        }

        private void btn_startRally_disable()
        {
            this.btn_startRally.Enabled = false;
            this.btn_startRally.BackColor = Color.FromArgb(0, 64, 0);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportToPDF worker = new ExportToPDF();
            bool didItWork = worker.export(this.matchData, matchData.data_date, this.pic_logo.Image);
            if (didItWork)
            {
                Console.WriteLine("exported to " + worker.getExportPath());
            }
            else
            {
                Console.WriteLine("ERROR!");
                MessageBox.Show(
                    "An Error was encountered while attempting to save the PDF. Sorry :-(\nPerhaps use the 'PrtScn' button to copy a snapshot of the screen to the clipboard and save in mspaint or a text editor???",
                    "Error Exporting",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("fileToolStripMenuItem_Click!");
        }

        private void btn_AdjustPlus_Click(object sender, EventArgs e)
        {
            this.matchData.adjustLastRallysShots(+1);
            //TODO update display?
        }

        private void btn_AdjustMinus_Click(object sender, EventArgs e)
        {
            this.matchData.adjustLastRallysShots(-1);
            //TODO update display
        }

        private void btn_pauseRally_Click(object sender, EventArgs e)
        {

            String resume = "Resume Rally (P)";
            if (this.btn_pauseRally.Text.Equals(resume))
            {
                this.matchData.resume();
                this.btn_pauseRally.Text = "Pause Rally (P)";
                this.btn_EndRally.Enabled = true;
            }else{
                this.matchData.pause();
                this.btn_pauseRally.Text = resume;
                this.btn_EndRally.Enabled = false; // TODO unhandled situation, need to write code to handle (pause, then end rally)
            }
        }

    }
}
