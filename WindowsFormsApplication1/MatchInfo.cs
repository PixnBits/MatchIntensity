using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MatchInfo : Form
    {

        private String info_enterer;
        private String info_event;
        private String info_location;
        private String info_playerA;
        private String info_playerB;

        public MatchInfo()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.info_enterer = this.txt_enterer.Text;
            this.info_event = this.txt_event.Text;
            this.info_location = this.txt_location.Text;
            this.info_playerA = this.txt_playerA.Text;
            this.info_playerB = this.txt_playerB.Text;

            this.Close();
        }


        // http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/ec9651da-c254-4423-89cc-633036b436ae
        // Adavesh
        public void ShowForm(
            out String Enterer, out String Event, out String Location, out String PlayerA, out String PlayerB,
            String origEnterer = null, String origEvent = null, String origLocation = null, String origPlayerA = null, String origPlayerB = null
        )
        {

            if (origEnterer != null)
                this.txt_enterer.Text = origEnterer;
            if (origEvent != null)
                this.txt_event.Text = origEvent;
            if (origLocation != null)
                this.txt_location.Text = origLocation;
            if (origPlayerA != null)
                this.txt_playerA.Text = origPlayerA;
            if (origPlayerB != null)
                this.txt_playerB.Text = origPlayerB;

            this.ShowDialog();

            Enterer = this.info_enterer;
            Event = this.info_event;
            Location = this.info_location;
            PlayerA = this.info_playerA;
            PlayerB = this.info_playerB;
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
