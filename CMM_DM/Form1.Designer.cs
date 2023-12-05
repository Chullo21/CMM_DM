namespace CMM_DM
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
            nominal = new DataGridViewTextBoxColumn();
            max = new DataGridViewTextBoxColumn();
            min = new DataGridViewTextBoxColumn();
            actual = new DataGridViewTextBoxColumn();
            label2 = new Label();
            downloadbtn = new Button();
            label3 = new Label();
            iqaDir = new TextBox();
            SearchIQA = new Button();
            SaveDataBtn = new Button();
            cmmCountTxt = new TextBox();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            IQATemplateBtn = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataDgv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // getDirBtn
            // 
            getDirBtn.Location = new Point(5, 24);
            getDirBtn.Name = "getDirBtn";
            getDirBtn.Size = new Size(94, 29);
            getDirBtn.TabIndex = 0;
            getDirBtn.Text = "Browse";
            getDirBtn.UseVisualStyleBackColor = true;
            getDirBtn.Click += getDirBtn_Click;
            // 
            // directoryTxt
            // 
            directoryTxt.Location = new Point(194, 27);
            directoryTxt.Margin = new Padding(3, 4, 3, 4);
            directoryTxt.Name = "directoryTxt";
            directoryTxt.ReadOnly = true;
            directoryTxt.Size = new Size(174, 27);
            directoryTxt.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(105, 28);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 2;
            label1.Text = "CMM Data:";
            // 
            // automateBtn
            // 
            automateBtn.BackColor = SystemColors.ButtonHighlight;
            automateBtn.Enabled = false;
            automateBtn.Location = new Point(433, 23);
            automateBtn.Margin = new Padding(3, 4, 3, 4);
            automateBtn.Name = "automateBtn";
            automateBtn.Size = new Size(82, 31);
            automateBtn.TabIndex = 3;
            automateBtn.Text = "Collect";
            automateBtn.UseVisualStyleBackColor = false;
            automateBtn.Click += automateBtn_Click;
            // 
            // clearBtn
            // 
            clearBtn.Location = new Point(624, 23);
            clearBtn.Margin = new Padding(3, 4, 3, 4);
            clearBtn.Name = "clearBtn";
            clearBtn.Size = new Size(82, 31);
            clearBtn.TabIndex = 7;
            clearBtn.Text = "Clear all";
            clearBtn.UseVisualStyleBackColor = true;
            clearBtn.Click += clearBtn_Click;
            // 
            // dataDgv
            // 
            dataDgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataDgv.Columns.AddRange(new DataGridViewColumn[] { number, nominal, max, min, actual });
            dataDgv.ImeMode = ImeMode.NoControl;
            dataDgv.Location = new Point(29, 223);
            dataDgv.Margin = new Padding(3, 4, 3, 4);
            dataDgv.Name = "dataDgv";
            dataDgv.RowHeadersWidth = 51;
            dataDgv.RowTemplate.Height = 25;
            dataDgv.Size = new Size(677, 364);
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
            // nominal
            // 
            nominal.HeaderText = "Nominal";
            nominal.MinimumWidth = 6;
            nominal.Name = "nominal";
            nominal.Width = 125;
            // 
            // max
            // 
            max.HeaderText = "Max. Tolerance";
            max.MinimumWidth = 6;
            max.Name = "max";
            max.Width = 125;
            // 
            // min
            // 
            min.HeaderText = "Min. Tolerance";
            min.MinimumWidth = 6;
            min.Name = "min";
            min.Width = 125;
            // 
            // actual
            // 
            actual.HeaderText = "Actual";
            actual.MinimumWidth = 6;
            actual.Name = "actual";
            actual.Width = 125;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(195, 23);
            label2.Name = "label2";
            label2.Size = new Size(341, 40);
            label2.TabIndex = 11;
            label2.Text = "CMM Data Migrator";
            // 
            // downloadbtn
            // 
            downloadbtn.Enabled = false;
            downloadbtn.Location = new Point(29, 185);
            downloadbtn.Name = "downloadbtn";
            downloadbtn.Size = new Size(153, 31);
            downloadbtn.TabIndex = 12;
            downloadbtn.Text = "Download";
            downloadbtn.UseVisualStyleBackColor = true;
            downloadbtn.Click += downloadbtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(174, 36);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 15;
            label3.Text = "Checksheet:";
            // 
            // iqaDir
            // 
            iqaDir.Location = new Point(266, 32);
            iqaDir.Margin = new Padding(3, 4, 3, 4);
            iqaDir.Name = "iqaDir";
            iqaDir.ReadOnly = true;
            iqaDir.Size = new Size(163, 27);
            iqaDir.TabIndex = 14;
            // 
            // SearchIQA
            // 
            SearchIQA.Location = new Point(7, 31);
            SearchIQA.Name = "SearchIQA";
            SearchIQA.Size = new Size(70, 29);
            SearchIQA.TabIndex = 16;
            SearchIQA.Text = "Browse";
            SearchIQA.UseVisualStyleBackColor = true;
            SearchIQA.Click += SearchIQA_Click;
            // 
            // SaveDataBtn
            // 
            SaveDataBtn.Enabled = false;
            SaveDataBtn.Location = new Point(433, 32);
            SaveDataBtn.Name = "SaveDataBtn";
            SaveDataBtn.Size = new Size(82, 29);
            SaveDataBtn.TabIndex = 17;
            SaveDataBtn.Text = "Transfer Data";
            SaveDataBtn.UseVisualStyleBackColor = true;
            SaveDataBtn.Click += SaveDataBtn_Click;
            // 
            // cmmCountTxt
            // 
            cmmCountTxt.Location = new Point(374, 25);
            cmmCountTxt.Name = "cmmCountTxt";
            cmmCountTxt.ReadOnly = true;
            cmmCountTxt.Size = new Size(53, 27);
            cmmCountTxt.TabIndex = 18;
            cmmCountTxt.TextAlign = HorizontalAlignment.Center;
            // 
            // button1
            // 
            button1.Location = new Point(598, 594);
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
            pictureBox1.Location = new Point(29, 38);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(153, 141);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // IQATemplateBtn
            // 
            IQATemplateBtn.Location = new Point(83, 30);
            IQATemplateBtn.Name = "IQATemplateBtn";
            IQATemplateBtn.Size = new Size(85, 29);
            IQATemplateBtn.TabIndex = 22;
            IQATemplateBtn.Text = "Template";
            IQATemplateBtn.UseVisualStyleBackColor = true;
            IQATemplateBtn.Click += IQATemplateBtn_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(directoryTxt);
            groupBox1.Controls.Add(getDirBtn);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(automateBtn);
            groupBox1.Controls.Add(cmmCountTxt);
            groupBox1.Location = new Point(188, 66);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(518, 71);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "CMM FILES";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(iqaDir);
            groupBox2.Controls.Add(IQATemplateBtn);
            groupBox2.Controls.Add(SearchIQA);
            groupBox2.Controls.Add(SaveDataBtn);
            groupBox2.Location = new Point(188, 143);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(518, 73);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "IQA CHECKSHEET";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(735, 634);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(downloadbtn);
            Controls.Add(label2);
            Controls.Add(dataDgv);
            Controls.Add(clearBtn);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "CMM Data Migrator";
            ((System.ComponentModel.ISupportInitialize)dataDgv).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
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
        private Button button1;
        private PictureBox pictureBox1;
        private DataGridViewTextBoxColumn number;
        private DataGridViewTextBoxColumn nominal;
        private DataGridViewTextBoxColumn max;
        private DataGridViewTextBoxColumn min;
        private DataGridViewTextBoxColumn actual;
        private Button IQATemplateBtn;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}