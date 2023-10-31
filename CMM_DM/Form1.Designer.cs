﻿namespace CMM_DM
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            getDirBtn = new Button();
            directoryTxt = new TextBox();
            label1 = new Label();
            automateBtn = new Button();
            clearBtn = new Button();
            dataDgv = new DataGridView();
            number = new DataGridViewTextBoxColumn();
            Cell2 = new DataGridViewTextBoxColumn();
            Cell1 = new DataGridViewTextBoxColumn();
            Cell3 = new DataGridViewTextBoxColumn();
            label2 = new Label();
            downloadbtn = new Button();
            label3 = new Label();
            iqaDir = new TextBox();
            SearchIQA = new Button();
            SaveDataBtn = new Button();
            cmmCountTxt = new TextBox();
            tempFile = new CheckBox();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataDgv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // getDirBtn
            // 
            getDirBtn.Location = new Point(234, 77);
            getDirBtn.Name = "getDirBtn";
            getDirBtn.Size = new Size(94, 29);
            getDirBtn.TabIndex = 0;
            getDirBtn.Text = "Browse";
            getDirBtn.UseVisualStyleBackColor = true;
            getDirBtn.Click += getDirBtn_Click;
            // 
            // directoryTxt
            // 
            directoryTxt.Location = new Point(461, 78);
            directoryTxt.Margin = new Padding(3, 4, 3, 4);
            directoryTxt.Name = "directoryTxt";
            directoryTxt.ReadOnly = true;
            directoryTxt.Size = new Size(155, 27);
            directoryTxt.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(350, 81);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 2;
            label1.Text = "CMM Data:";
            // 
            // automateBtn
            // 
            automateBtn.BackColor = SystemColors.ButtonHighlight;
            automateBtn.Enabled = false;
            automateBtn.Location = new Point(690, 76);
            automateBtn.Margin = new Padding(3, 4, 3, 4);
            automateBtn.Name = "automateBtn";
            automateBtn.Size = new Size(92, 31);
            automateBtn.TabIndex = 3;
            automateBtn.Text = "Collect";
            automateBtn.UseVisualStyleBackColor = false;
            automateBtn.Click += automateBtn_Click;
            // 
            // clearBtn
            // 
            clearBtn.Location = new Point(801, 21);
            clearBtn.Margin = new Padding(3, 4, 3, 4);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(95, 31);
            clearBtn.TabIndex = 7;
            clearBtn.Text = "Clear all";
            clearBtn.UseVisualStyleBackColor = true;
            clearBtn.Click += clearBtn_Click;
            // 
            // dataDgv
            // 
            dataDgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataDgv.Columns.AddRange(new DataGridViewColumn[] { number, Cell2, Cell1, Cell3 });
            dataDgv.ImeMode = ImeMode.NoControl;
            dataDgv.Location = new Point(47, 228);
            dataDgv.Margin = new Padding(3, 4, 3, 4);
            dataDgv.Name = "dataDgv";
            dataDgv.RowHeadersWidth = 51;
            dataDgv.RowTemplate.Height = 25;
            dataDgv.Size = new Size(849, 498);
            dataDgv.TabIndex = 10;
            // 
            // number
            // 
            number.Frozen = true;
            number.HeaderText = "Element No.";
            number.MinimumWidth = 6;
            number.Name = "number";
            number.Width = 125;
            // 
            // Cell2
            // 
            Cell2.Frozen = true;
            Cell2.HeaderText = "Min. Tolerance";
            Cell2.MinimumWidth = 6;
            Cell2.Name = "Cell2";
            Cell2.Width = 125;
            // 
            // Cell1
            // 
            Cell1.Frozen = true;
            Cell1.HeaderText = "Max. Tolerance";
            Cell1.MinimumWidth = 6;
            Cell1.Name = "Cell1";
            Cell1.Width = 125;
            // 
            // Cell3
            // 
            Cell3.Frozen = true;
            Cell3.HeaderText = "Actual";
            Cell3.MinimumWidth = 6;
            Cell3.Name = "Cell3";
            Cell3.Width = 125;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(234, 18);
            label2.Name = "label2";
            label2.Size = new Size(341, 40);
            label2.TabIndex = 11;
            label2.Text = "CMM Data Migrator";
            // 
            // downloadbtn
            // 
            downloadbtn.Enabled = false;
            downloadbtn.Location = new Point(784, 189);
            downloadbtn.Name = "downloadbtn";
            downloadbtn.Size = new Size(112, 31);
            downloadbtn.TabIndex = 12;
            downloadbtn.Text = "Download";
            downloadbtn.UseVisualStyleBackColor = true;
            downloadbtn.Click += downloadbtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(334, 129);
            label3.Name = "label3";
            label3.Size = new Size(121, 20);
            label3.TabIndex = 15;
            label3.Text = "I.Q.A Checksheet:";
            // 
            // iqaDir
            // 
            iqaDir.Location = new Point(461, 126);
            iqaDir.Margin = new Padding(3, 4, 3, 4);
            iqaDir.Name = "iqaDir";
            iqaDir.ReadOnly = true;
            iqaDir.Size = new Size(321, 27);
            iqaDir.TabIndex = 14;
            // 
            // SearchIQA
            // 
            SearchIQA.Location = new Point(234, 125);
            SearchIQA.Name = "SearchIQA";
            SearchIQA.Size = new Size(94, 29);
            SearchIQA.TabIndex = 16;
            SearchIQA.Text = "Browse";
            SearchIQA.UseVisualStyleBackColor = true;
            SearchIQA.Click += SearchIQA_Click;
            // 
            // SaveDataBtn
            // 
            SaveDataBtn.Enabled = false;
            SaveDataBtn.Location = new Point(47, 192);
            SaveDataBtn.Name = "SaveDataBtn";
            SaveDataBtn.Size = new Size(378, 29);
            SaveDataBtn.TabIndex = 17;
            SaveDataBtn.Text = "Save Data";
            SaveDataBtn.UseVisualStyleBackColor = true;
            SaveDataBtn.Click += SaveDataBtn_Click;
            // 
            // cmmCountTxt
            // 
            cmmCountTxt.Location = new Point(622, 78);
            cmmCountTxt.Name = "cmmCountTxt";
            cmmCountTxt.ReadOnly = true;
            cmmCountTxt.Size = new Size(62, 27);
            cmmCountTxt.TabIndex = 18;
            cmmCountTxt.TextAlign = HorizontalAlignment.Center;
            // 
            // tempFile
            // 
            tempFile.AutoSize = true;
            tempFile.Location = new Point(12, 737);
            tempFile.Name = "tempFile";
            tempFile.Size = new Size(91, 24);
            tempFile.TabIndex = 19;
            tempFile.Text = "TempFile";
            tempFile.UseVisualStyleBackColor = true;
            tempFile.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(788, 733);
            button1.Name = "button1";
            button1.Size = new Size(108, 31);
            button1.TabIndex = 20;
            button1.Text = "Developers";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(47, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(163, 146);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(942, 773);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(tempFile);
            Controls.Add(cmmCountTxt);
            Controls.Add(SaveDataBtn);
            Controls.Add(SearchIQA);
            Controls.Add(label3);
            Controls.Add(iqaDir);
            Controls.Add(downloadbtn);
            Controls.Add(label2);
            Controls.Add(dataDgv);
            Controls.Add(clearBtn);
            Controls.Add(automateBtn);
            Controls.Add(label1);
            Controls.Add(directoryTxt);
            Controls.Add(getDirBtn);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "CMM Data Migrator";
            ((System.ComponentModel.ISupportInitialize)dataDgv).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button getDirBtn;
        private TextBox directoryTxt;
        private Label label1;
        private Button automateBtn;
        private Button clearBtn;
        private Label label2;
        private DataGridView dataDgv;
        private Button downloadbtn;
        private Label label3;
        private TextBox iqaDir;
        private Button SearchIQA;
        private Button SaveDataBtn;
        private TextBox cmmCountTxt;
        private CheckBox tempFile;
        private Button button1;
        private DataGridViewTextBoxColumn number;
        private DataGridViewTextBoxColumn Cell2;
        private DataGridViewTextBoxColumn Cell1;
        private DataGridViewTextBoxColumn Cell3;
        private PictureBox pictureBox1;
    }
}