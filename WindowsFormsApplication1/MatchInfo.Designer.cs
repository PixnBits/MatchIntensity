namespace WindowsFormsApplication1
{
    partial class MatchInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchInfo));
            this.txt_playerB = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_playerA = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txt_location = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_enterer = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txt_event = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_playerB
            // 
            this.txt_playerB.Location = new System.Drawing.Point(69, 123);
            this.txt_playerB.Name = "txt_playerB";
            this.txt_playerB.Size = new System.Drawing.Size(320, 20);
            this.txt_playerB.TabIndex = 5;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(209, 107);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(21, 13);
            this.label26.TabIndex = 25;
            this.label26.Text = "vs.";
            // 
            // txt_playerA
            // 
            this.txt_playerA.Location = new System.Drawing.Point(69, 84);
            this.txt_playerA.Name = "txt_playerA";
            this.txt_playerA.Size = new System.Drawing.Size(320, 20);
            this.txt_playerA.TabIndex = 4;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 87);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(44, 13);
            this.label25.TabIndex = 23;
            this.label25.Text = "Players:";
            // 
            // txt_location
            // 
            this.txt_location.Location = new System.Drawing.Point(69, 58);
            this.txt_location.Name = "txt_location";
            this.txt_location.Size = new System.Drawing.Size(320, 20);
            this.txt_location.TabIndex = 3;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 61);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 13);
            this.label24.TabIndex = 21;
            this.label24.Text = "Location:";
            // 
            // txt_enterer
            // 
            this.txt_enterer.Location = new System.Drawing.Point(69, 6);
            this.txt_enterer.Name = "txt_enterer";
            this.txt_enterer.Size = new System.Drawing.Size(320, 20);
            this.txt_enterer.TabIndex = 0;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 9);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(44, 13);
            this.label23.TabIndex = 19;
            this.label23.Text = "Enterer:";
            // 
            // txt_event
            // 
            this.txt_event.Location = new System.Drawing.Point(69, 32);
            this.txt_event.Name = "txt_event";
            this.txt_event.Size = new System.Drawing.Size(320, 20);
            this.txt_event.TabIndex = 2;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 35);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 13);
            this.label22.TabIndex = 17;
            this.label22.Text = "Event:";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(212, 149);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(132, 24);
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(12, 152);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(66, 21);
            this.btn_cancel.TabIndex = 26;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // MatchInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(401, 180);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_playerB);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txt_playerA);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txt_location);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txt_enterer);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.txt_event);
            this.Controls.Add(this.label22);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MatchInfo";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MatchInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_playerB;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txt_playerA;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txt_location;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_enterer;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_event;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
    }
}