namespace DBDictGen
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDBPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDBPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDBType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDB = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnListTable = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lstTable = new System.Windows.Forms.ListBox();
            this.lstTableSelected = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDictPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnGen = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 275);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = " ";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host";
            // 
            // txtDBHost
            // 
            this.txtDBHost.Location = new System.Drawing.Point(48, 10);
            this.txtDBHost.Name = "txtDBHost";
            this.txtDBHost.Size = new System.Drawing.Size(100, 21);
            this.txtDBHost.TabIndex = 2;
            this.txtDBHost.Text = "127.000.000.001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // txtDBPort
            // 
            this.txtDBPort.Location = new System.Drawing.Point(207, 9);
            this.txtDBPort.Name = "txtDBPort";
            this.txtDBPort.Size = new System.Drawing.Size(100, 21);
            this.txtDBPort.TabIndex = 4;
            this.txtDBPort.Text = "3306";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "User";
            // 
            // txtDBUser
            // 
            this.txtDBUser.Location = new System.Drawing.Point(365, 10);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(100, 21);
            this.txtDBUser.TabIndex = 6;
            this.txtDBUser.Text = "root";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(487, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pwd";
            // 
            // txtDBPwd
            // 
            this.txtDBPwd.Location = new System.Drawing.Point(517, 9);
            this.txtDBPwd.Name = "txtDBPwd";
            this.txtDBPwd.Size = new System.Drawing.Size(100, 21);
            this.txtDBPwd.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(633, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "DBType";
            // 
            // cmbDBType
            // 
            this.cmbDBType.FormattingEnabled = true;
            this.cmbDBType.Items.AddRange(new object[] {
            "MySQL",
            "SQLServer",
            "Sqlite"});
            this.cmbDBType.Location = new System.Drawing.Point(691, 11);
            this.cmbDBType.Name = "cmbDBType";
            this.cmbDBType.Size = new System.Drawing.Size(121, 20);
            this.cmbDBType.TabIndex = 10;
            this.cmbDBType.Text = "MySQL";
            this.cmbDBType.SelectedIndexChanged += new System.EventHandler(this.cmbDBType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "DB";
            // 
            // cmbDB
            // 
            this.cmbDB.FormattingEnabled = true;
            this.cmbDB.Location = new System.Drawing.Point(48, 44);
            this.cmbDB.Name = "cmbDB";
            this.cmbDB.Size = new System.Drawing.Size(521, 20);
            this.cmbDB.TabIndex = 12;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(575, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnListTable
            // 
            this.btnListTable.Location = new System.Drawing.Point(656, 42);
            this.btnListTable.Name = "btnListTable";
            this.btnListTable.Size = new System.Drawing.Size(75, 23);
            this.btnListTable.TabIndex = 14;
            this.btnListTable.Text = "&ListTable";
            this.btnListTable.UseVisualStyleBackColor = true;
            this.btnListTable.Click += new System.EventHandler(this.btnListTable_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(737, 42);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 15;
            this.btnDisconnect.Text = "&Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lstTable
            // 
            this.lstTable.FormattingEnabled = true;
            this.lstTable.ItemHeight = 12;
            this.lstTable.Location = new System.Drawing.Point(15, 79);
            this.lstTable.Name = "lstTable";
            this.lstTable.Size = new System.Drawing.Size(360, 160);
            this.lstTable.TabIndex = 16;
            // 
            // lstTableSelected
            // 
            this.lstTableSelected.FormattingEnabled = true;
            this.lstTableSelected.ItemHeight = 12;
            this.lstTableSelected.Location = new System.Drawing.Point(452, 79);
            this.lstTableSelected.Name = "lstTableSelected";
            this.lstTableSelected.Size = new System.Drawing.Size(360, 160);
            this.lstTableSelected.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(383, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = ">>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(384, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = ">";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(384, 158);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(62, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "<";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(384, 188);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(62, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "<<";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "DictPath";
            // 
            // txtDictPath
            // 
            this.txtDictPath.Location = new System.Drawing.Point(72, 249);
            this.txtDictPath.Name = "txtDictPath";
            this.txtDictPath.Size = new System.Drawing.Size(374, 21);
            this.txtDictPath.TabIndex = 23;
            this.txtDictPath.Text = "./dict/";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(453, 246);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 24;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(737, 246);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(75, 23);
            this.btnGen.TabIndex = 25;
            this.btnGen.Text = "&Gen";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 297);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDictPath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstTableSelected);
            this.Controls.Add(this.lstTable);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnListTable);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cmbDB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbDBType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDBPwd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDBUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDBPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDBHost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDBHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDBPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDBPwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbDBType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDB;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnListTable;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ListBox lstTable;
        private System.Windows.Forms.ListBox lstTableSelected;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDictPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnGen;
    }
}

