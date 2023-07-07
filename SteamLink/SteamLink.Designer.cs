namespace SteamLink
{
    partial class SteamLink
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            modesListView = new ListView();
            idxColumnHeader = new ColumnHeader();
            widthColumnHeader = new ColumnHeader();
            heightColumnHeader = new ColumnHeader();
            orientColumnHeader = new ColumnHeader();
            bitsColumnHeader = new ColumnHeader();
            freqColumnHeader = new ColumnHeader();
            ckActive = new CheckBox();
            tbProcess = new TextBox();
            tbTitleName = new TextBox();
            btnSave = new Button();
            SuspendLayout();
            // 
            // modesListView
            // 
            modesListView.Columns.AddRange(new ColumnHeader[] { idxColumnHeader, widthColumnHeader, heightColumnHeader, orientColumnHeader, bitsColumnHeader, freqColumnHeader });
            modesListView.ForeColor = SystemColors.HotTrack;
            modesListView.FullRowSelect = true;
            modesListView.GridLines = true;
            modesListView.Location = new Point(12, 12);
            modesListView.Name = "modesListView";
            modesListView.ShowItemToolTips = true;
            modesListView.Size = new Size(381, 528);
            modesListView.TabIndex = 0;
            modesListView.UseCompatibleStateImageBehavior = false;
            modesListView.View = View.Details;
            modesListView.SelectedIndexChanged += modesListView_SelectedIndexChanged;
            modesListView.DoubleClick += modesListView_DoubleClick;
            // 
            // idxColumnHeader
            // 
            idxColumnHeader.Text = "Idx";
            idxColumnHeader.Width = 30;
            // 
            // widthColumnHeader
            // 
            widthColumnHeader.Text = "Width";
            // 
            // heightColumnHeader
            // 
            heightColumnHeader.Text = "Height";
            // 
            // orientColumnHeader
            // 
            orientColumnHeader.Text = "Orientation";
            orientColumnHeader.Width = 70;
            // 
            // bitsColumnHeader
            // 
            bitsColumnHeader.Text = "Bits Count";
            bitsColumnHeader.Width = 70;
            // 
            // freqColumnHeader
            // 
            freqColumnHeader.Text = "Frequency";
            freqColumnHeader.Width = 70;
            // 
            // ckActive
            // 
            ckActive.AutoSize = true;
            ckActive.BackColor = Color.Transparent;
            ckActive.ForeColor = SystemColors.Window;
            ckActive.Location = new Point(399, 12);
            ckActive.Name = "ckActive";
            ckActive.Size = new Size(59, 19);
            ckActive.TabIndex = 1;
            ckActive.Text = "Active";
            ckActive.UseVisualStyleBackColor = false;
            ckActive.CheckedChanged += ckActive_CheckedChanged;
            // 
            // tbProcess
            // 
            tbProcess.ForeColor = SystemColors.HotTrack;
            tbProcess.Location = new Point(399, 37);
            tbProcess.Name = "tbProcess";
            tbProcess.Size = new Size(173, 23);
            tbProcess.TabIndex = 2;
            tbProcess.TextChanged += tbProcess_TextChanged;
            // 
            // tbTitleName
            // 
            tbTitleName.ForeColor = SystemColors.HotTrack;
            tbTitleName.Location = new Point(399, 66);
            tbTitleName.Name = "tbTitleName";
            tbTitleName.Size = new Size(173, 23);
            tbTitleName.TabIndex = 3;
            tbTitleName.TextChanged += tbTitleName_TextChanged;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(497, 95);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // SteamLink
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.steam_link_resize1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(584, 561);
            Controls.Add(btnSave);
            Controls.Add(tbTitleName);
            Controls.Add(tbProcess);
            Controls.Add(ckActive);
            Controls.Add(modesListView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "SteamLink";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Steam Link Resize";
            FormClosing += SteamLink_FormClosing;
            Load += SteamLink_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView modesListView;
        private ColumnHeader idxColumnHeader;
        private ColumnHeader widthColumnHeader;
        private ColumnHeader heightColumnHeader;
        private ColumnHeader orientColumnHeader;
        private ColumnHeader bitsColumnHeader;
        private ColumnHeader freqColumnHeader;
        private CheckBox ckActive;
        private TextBox tbProcess;
        private TextBox tbTitleName;
        private Button btnSave;
    }
}