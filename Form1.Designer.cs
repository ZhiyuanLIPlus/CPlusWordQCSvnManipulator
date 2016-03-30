namespace TiampMan
{
    partial class TiampMan
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TiampMan));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonFire = new System.Windows.Forms.Button();
            this.ButtonQuit = new System.Windows.Forms.Button();
            this.tbRepoURI = new System.Windows.Forms.TextBox();
            this.tbLocalPath = new System.Windows.Forms.TextBox();
            this.tbTAG = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbQCNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rBFond = new System.Windows.Forms.RadioButton();
            this.rBData = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cBBranch = new System.Windows.Forms.ComboBox();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ButtonBrower = new System.Windows.Forms.Button();
            this.ofdFileSelector = new System.Windows.Forms.OpenFileDialog();
            this.labelMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cBFicheSuivi = new System.Windows.Forms.CheckBox();
            this.cBTouchQC = new System.Windows.Forms.CheckBox();
            this.tBQCComments = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lbMessageQC = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Repo URL:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 374);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Local Path:";
            // 
            // ButtonFire
            // 
            this.ButtonFire.Location = new System.Drawing.Point(180, 590);
            this.ButtonFire.Name = "ButtonFire";
            this.ButtonFire.Size = new System.Drawing.Size(65, 22);
            this.ButtonFire.TabIndex = 9;
            this.ButtonFire.Text = "Fire!";
            this.ButtonFire.UseVisualStyleBackColor = true;
            this.ButtonFire.Click += new System.EventHandler(this.ButtonFire_Click);
            // 
            // ButtonQuit
            // 
            this.ButtonQuit.Location = new System.Drawing.Point(381, 590);
            this.ButtonQuit.Name = "ButtonQuit";
            this.ButtonQuit.Size = new System.Drawing.Size(65, 22);
            this.ButtonQuit.TabIndex = 10;
            this.ButtonQuit.Text = "Quit";
            this.ButtonQuit.UseVisualStyleBackColor = true;
            this.ButtonQuit.Click += new System.EventHandler(this.ButtonQuit_Click);
            // 
            // tbRepoURI
            // 
            this.tbRepoURI.Location = new System.Drawing.Point(180, 348);
            this.tbRepoURI.Name = "tbRepoURI";
            this.tbRepoURI.Size = new System.Drawing.Size(400, 20);
            this.tbRepoURI.TabIndex = 7;
            this.tbRepoURI.Text = "http://slxd2004.app.eiffage.loc/operis/svn/tags/";
            // 
            // tbLocalPath
            // 
            this.tbLocalPath.Location = new System.Drawing.Point(180, 374);
            this.tbLocalPath.Name = "tbLocalPath";
            this.tbLocalPath.Size = new System.Drawing.Size(400, 20);
            this.tbLocalPath.TabIndex = 8;
            this.tbLocalPath.Text = "D:\\ZLI\\Bureau\\Livrason\\TAGS\\";
            // 
            // tbTAG
            // 
            this.tbTAG.Location = new System.Drawing.Point(180, 322);
            this.tbTAG.Name = "tbTAG";
            this.tbTAG.Size = new System.Drawing.Size(122, 20);
            this.tbTAG.TabIndex = 6;
            this.tbTAG.TextChanged += new System.EventHandler(this.tbTAG_TextChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "TAG:";
            // 
            // tbQCNum
            // 
            this.tbQCNum.Location = new System.Drawing.Point(180, 296);
            this.tbQCNum.Name = "tbQCNum";
            this.tbQCNum.Size = new System.Drawing.Size(122, 20);
            this.tbQCNum.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "QC Number:";
            // 
            // rBFond
            // 
            this.rBFond.AutoSize = true;
            this.rBFond.Location = new System.Drawing.Point(202, 215);
            this.rBFond.Name = "rBFond";
            this.rBFond.Size = new System.Drawing.Size(49, 17);
            this.rBFond.TabIndex = 1;
            this.rBFond.Text = "Fond";
            this.rBFond.UseVisualStyleBackColor = true;
            // 
            // rBData
            // 
            this.rBData.AutoSize = true;
            this.rBData.Checked = true;
            this.rBData.Location = new System.Drawing.Point(294, 215);
            this.rBData.Name = "rBData";
            this.rBData.Size = new System.Drawing.Size(68, 17);
            this.rBData.TabIndex = 2;
            this.rBData.TabStop = true;
            this.rBData.Text = "Données";
            this.rBData.UseVisualStyleBackColor = true;
            this.rBData.CheckedChanged += new System.EventHandler(this.rBData_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Correction Type:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(95, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Target Branch:";
            // 
            // cBBranch
            // 
            this.cBBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBBranch.FormattingEnabled = true;
            this.cBBranch.Items.AddRange(new object[] {
            "EIFFAGE",
            "FORCLUM",
            "TP",
            "BATIMENT",
            "EIFFEL"});
            this.cBBranch.Location = new System.Drawing.Point(180, 267);
            this.cBBranch.Name = "cBBranch";
            this.cBBranch.Size = new System.Drawing.Size(122, 21);
            this.cBBranch.TabIndex = 4;
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(180, 241);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(312, 20);
            this.tbFileName.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(96, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Files:";
            // 
            // ButtonBrower
            // 
            this.ButtonBrower.Location = new System.Drawing.Point(515, 239);
            this.ButtonBrower.Name = "ButtonBrower";
            this.ButtonBrower.Size = new System.Drawing.Size(65, 22);
            this.ButtonBrower.TabIndex = 17;
            this.ButtonBrower.Text = "Brower";
            this.ButtonBrower.UseVisualStyleBackColor = true;
            this.ButtonBrower.Click += new System.EventHandler(this.ButtonBrower_Click);
            // 
            // ofdFileSelector
            // 
            this.ofdFileSelector.Filter = "Tiamp File |*.xtiamp|All files|*.*";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(95, 546);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(78, 13);
            this.labelMessage.TabIndex = 18;
            this.labelMessage.Text = "Message SVN:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TiampMan.Properties.Resources.TiampMan;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(51, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(558, 180);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // cBFicheSuivi
            // 
            this.cBFicheSuivi.AutoSize = true;
            this.cBFicheSuivi.Location = new System.Drawing.Point(98, 400);
            this.cBFicheSuivi.Name = "cBFicheSuivi";
            this.cBFicheSuivi.Size = new System.Drawing.Size(207, 17);
            this.cBFicheSuivi.TabIndex = 9;
            this.cBFicheSuivi.Text = "Initialise and Upload FicheSuive to QC";
            this.cBFicheSuivi.UseVisualStyleBackColor = true;
            // 
            // cBTouchQC
            // 
            this.cBTouchQC.AutoSize = true;
            this.cBTouchQC.Location = new System.Drawing.Point(98, 423);
            this.cBTouchQC.Name = "cBTouchQC";
            this.cBTouchQC.Size = new System.Drawing.Size(144, 17);
            this.cBTouchQC.TabIndex = 10;
            this.cBTouchQC.Text = "Update information in QC";
            this.cBTouchQC.UseVisualStyleBackColor = true;
            this.cBTouchQC.CheckedChanged += new System.EventHandler(this.cBTouchQC_CheckedChanged);
            // 
            // tBQCComments
            // 
            this.tBQCComments.Enabled = false;
            this.tBQCComments.Location = new System.Drawing.Point(180, 447);
            this.tBQCComments.Multiline = true;
            this.tBQCComments.Name = "tBQCComments";
            this.tBQCComments.Size = new System.Drawing.Size(400, 83);
            this.tBQCComments.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(95, 450);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "QC comments:";
            // 
            // lbMessageQC
            // 
            this.lbMessageQC.AutoSize = true;
            this.lbMessageQC.Location = new System.Drawing.Point(95, 568);
            this.lbMessageQC.Name = "lbMessageQC";
            this.lbMessageQC.Size = new System.Drawing.Size(71, 13);
            this.lbMessageQC.TabIndex = 21;
            this.lbMessageQC.Text = "Message QC:";
            // 
            // TiampMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 637);
            this.Controls.Add(this.lbMessageQC);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tBQCComments);
            this.Controls.Add(this.cBTouchQC);
            this.Controls.Add(this.cBFicheSuivi);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.ButtonBrower);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cBBranch);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rBData);
            this.Controls.Add(this.rBFond);
            this.Controls.Add(this.tbQCNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbTAG);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbLocalPath);
            this.Controls.Add(this.tbRepoURI);
            this.Controls.Add(this.ButtonQuit);
            this.Controls.Add(this.ButtonFire);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TiampMan";
            this.Text = "TiampMan";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonFire;
        private System.Windows.Forms.Button ButtonQuit;
        private System.Windows.Forms.TextBox tbRepoURI;
        private System.Windows.Forms.TextBox tbLocalPath;
        private System.Windows.Forms.TextBox tbTAG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbQCNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rBFond;
        private System.Windows.Forms.RadioButton rBData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cBBranch;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ButtonBrower;
        private System.Windows.Forms.OpenFileDialog ofdFileSelector;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cBFicheSuivi;
        private System.Windows.Forms.CheckBox cBTouchQC;
        private System.Windows.Forms.TextBox tBQCComments;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbMessageQC;
    }
}

