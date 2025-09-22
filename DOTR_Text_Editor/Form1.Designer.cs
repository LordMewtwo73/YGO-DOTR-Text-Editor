namespace DOTR_Text_Editor
{
	partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openISOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToISOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.numIndex = new System.Windows.Forms.NumericUpDown();
            this.comboCardName = new System.Windows.Forms.ComboBox();
            this.txtString = new System.Windows.Forms.TextBox();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.openISOFD = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboCardAbility = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboSection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.numTotalBytes = new System.Windows.Forms.NumericUpDown();
            this.lblTotalBytes = new System.Windows.Forms.Label();
            this.lblByteLimit = new System.Windows.Forms.Label();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.btnClearTutorial = new System.Windows.Forms.Button();
            this.openTSVFD = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalBytes)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openISOToolStripMenuItem,
            this.saveToISOToolStripMenuItem,
            this.exportToCSVToolStripMenuItem,
            this.importFromCSVToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(667, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openISOToolStripMenuItem
            // 
            this.openISOToolStripMenuItem.Name = "openISOToolStripMenuItem";
            this.openISOToolStripMenuItem.Size = new System.Drawing.Size(78, 21);
            this.openISOToolStripMenuItem.Text = "Open ISO";
            this.openISOToolStripMenuItem.Click += new System.EventHandler(this.openISOToolStripMenuItem_Click_1);
            // 
            // saveToISOToolStripMenuItem
            // 
            this.saveToISOToolStripMenuItem.Name = "saveToISOToolStripMenuItem";
            this.saveToISOToolStripMenuItem.Size = new System.Drawing.Size(90, 21);
            this.saveToISOToolStripMenuItem.Text = "Save to ISO";
            this.saveToISOToolStripMenuItem.Click += new System.EventHandler(this.saveToISOToolStripMenuItem_Click);
            // 
            // exportToCSVToolStripMenuItem
            // 
            this.exportToCSVToolStripMenuItem.Name = "exportToCSVToolStripMenuItem";
            this.exportToCSVToolStripMenuItem.Size = new System.Drawing.Size(105, 21);
            this.exportToCSVToolStripMenuItem.Text = "Export to TSV";
            this.exportToCSVToolStripMenuItem.Click += new System.EventHandler(this.exportToCSVToolStripMenuItem_Click);
            // 
            // importFromCSVToolStripMenuItem
            // 
            this.importFromCSVToolStripMenuItem.Name = "importFromCSVToolStripMenuItem";
            this.importFromCSVToolStripMenuItem.Size = new System.Drawing.Size(124, 21);
            this.importFromCSVToolStripMenuItem.Text = "Import from TSV";
            this.importFromCSVToolStripMenuItem.Click += new System.EventHandler(this.importFromCSVToolStripMenuItem_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(12, 33);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(70, 16);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "lblFilePath";
            // 
            // numIndex
            // 
            this.numIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numIndex.Location = new System.Drawing.Point(15, 83);
            this.numIndex.Maximum = new decimal(new int[] {
            3073,
            0,
            0,
            0});
            this.numIndex.Name = "numIndex";
            this.numIndex.Size = new System.Drawing.Size(80, 22);
            this.numIndex.TabIndex = 2;
            this.numIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numIndex.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // comboCardName
            // 
            this.comboCardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCardName.FormattingEnabled = true;
            this.comboCardName.Location = new System.Drawing.Point(15, 144);
            this.comboCardName.Name = "comboCardName";
            this.comboCardName.Size = new System.Drawing.Size(177, 24);
            this.comboCardName.TabIndex = 3;
            this.comboCardName.SelectedIndexChanged += new System.EventHandler(this.comboCardName_SelectedIndexChanged);
            // 
            // txtString
            // 
            this.txtString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtString.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtString.Location = new System.Drawing.Point(215, 118);
            this.txtString.Multiline = true;
            this.txtString.Name = "txtString";
            this.txtString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtString.Size = new System.Drawing.Size(417, 273);
            this.txtString.TabIndex = 4;
            this.txtString.WordWrap = false;
            this.txtString.TextChanged += new System.EventHandler(this.txtString_TextChanged);
            // 
            // btnResetAll
            // 
            this.btnResetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetAll.Location = new System.Drawing.Point(557, 30);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(75, 38);
            this.btnResetAll.TabIndex = 6;
            this.btnResetAll.Text = "Reset All";
            this.btnResetAll.UseVisualStyleBackColor = true;
            this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(557, 74);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 38);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // openISOFD
            // 
            this.openISOFD.FileName = "openFileDialog1";
            this.openISOFD.Filter = "ISO File|*.iso";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Index";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Jump to Card Name";
            // 
            // comboCardAbility
            // 
            this.comboCardAbility.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCardAbility.FormattingEnabled = true;
            this.comboCardAbility.Location = new System.Drawing.Point(15, 205);
            this.comboCardAbility.Name = "comboCardAbility";
            this.comboCardAbility.Size = new System.Drawing.Size(177, 24);
            this.comboCardAbility.TabIndex = 3;
            this.comboCardAbility.SelectedIndexChanged += new System.EventHandler(this.comboCardAbility_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Jump to Card Ability";
            // 
            // comboSection
            // 
            this.comboSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSection.FormattingEnabled = true;
            this.comboSection.Location = new System.Drawing.Point(15, 262);
            this.comboSection.Name = "comboSection";
            this.comboSection.Size = new System.Drawing.Size(177, 24);
            this.comboSection.TabIndex = 3;
            this.comboSection.SelectedIndexChanged += new System.EventHandler(this.comboSection_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Jump to Section";
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.Location = new System.Drawing.Point(215, 84);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(322, 22);
            this.txtTitle.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(212, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "String Title";
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(12, 405);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(643, 23);
            this.progBar.TabIndex = 9;
            this.progBar.Visible = false;
            // 
            // numTotalBytes
            // 
            this.numTotalBytes.Location = new System.Drawing.Point(97, 342);
            this.numTotalBytes.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numTotalBytes.Name = "numTotalBytes";
            this.numTotalBytes.ReadOnly = true;
            this.numTotalBytes.Size = new System.Drawing.Size(95, 20);
            this.numTotalBytes.TabIndex = 10;
            this.numTotalBytes.Visible = false;
            // 
            // lblTotalBytes
            // 
            this.lblTotalBytes.AutoSize = true;
            this.lblTotalBytes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBytes.Location = new System.Drawing.Point(94, 323);
            this.lblTotalBytes.Name = "lblTotalBytes";
            this.lblTotalBytes.Size = new System.Drawing.Size(75, 16);
            this.lblTotalBytes.TabIndex = 7;
            this.lblTotalBytes.Text = "Total Bytes";
            this.lblTotalBytes.Visible = false;
            // 
            // lblByteLimit
            // 
            this.lblByteLimit.AutoSize = true;
            this.lblByteLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblByteLimit.Location = new System.Drawing.Point(94, 365);
            this.lblByteLimit.Name = "lblByteLimit";
            this.lblByteLimit.Size = new System.Drawing.Size(20, 16);
            this.lblByteLimit.TabIndex = 7;
            this.lblByteLimit.Text = "/ x";
            this.lblByteLimit.Visible = false;
            // 
            // saveFD
            // 
            this.saveFD.Filter = "TSV File|*.tsv";
            // 
            // btnClearTutorial
            // 
            this.btnClearTutorial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearTutorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearTutorial.Location = new System.Drawing.Point(446, 30);
            this.btnClearTutorial.Name = "btnClearTutorial";
            this.btnClearTutorial.Size = new System.Drawing.Size(91, 38);
            this.btnClearTutorial.TabIndex = 6;
            this.btnClearTutorial.Text = "Clear Tutorial Strings";
            this.btnClearTutorial.UseVisualStyleBackColor = true;
            this.btnClearTutorial.Click += new System.EventHandler(this.btnClearTutorial_Click);
            // 
            // openTSVFD
            // 
            this.openTSVFD.FileName = "openFileDialog1";
            this.openTSVFD.Filter = "TSV File|*.tsv";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 440);
            this.Controls.Add(this.numTotalBytes);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblByteLimit);
            this.Controls.Add(this.lblTotalBytes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClearTutorial);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnResetAll);
            this.Controls.Add(this.comboSection);
            this.Controls.Add(this.comboCardAbility);
            this.Controls.Add(this.txtString);
            this.Controls.Add(this.comboCardName);
            this.Controls.Add(this.numIndex);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "YGO DOTR Text Editor by LordMewtwo73";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalBytes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.Label lblFilePath;
		private System.Windows.Forms.NumericUpDown numIndex;
		private System.Windows.Forms.ComboBox comboCardName;
		private System.Windows.Forms.TextBox txtString;
		private System.Windows.Forms.Button btnResetAll;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.OpenFileDialog openISOFD;
        private System.Windows.Forms.ToolStripMenuItem openISOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToISOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToCSVToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboCardAbility;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboSection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ProgressBar progBar;
		private System.Windows.Forms.NumericUpDown numTotalBytes;
		private System.Windows.Forms.Label lblTotalBytes;
		private System.Windows.Forms.Label lblByteLimit;
		private System.Windows.Forms.ToolStripMenuItem importFromCSVToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFD;
		private System.Windows.Forms.Button btnClearTutorial;
        private System.Windows.Forms.OpenFileDialog openTSVFD;
    }
}

