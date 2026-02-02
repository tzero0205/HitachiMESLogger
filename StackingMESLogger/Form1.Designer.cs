namespace StackingMESLogger
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmbCOM = new System.Windows.Forms.ComboBox();
            this.btnRefreshCOM = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblStatusCOM = new System.Windows.Forms.Label();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.btnBrowsePath = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtStackName = new System.Windows.Forms.TextBox();
            this.txtManager = new System.Windows.Forms.TextBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.cmbModelList = new System.Windows.Forms.ComboBox();
            this.btnLoadModel = new System.Windows.Forms.Button();
            this.btnOpenModelSetting = new System.Windows.Forms.Button();
            this.lblProgramStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEditModel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLogCopyPath = new System.Windows.Forms.TextBox();
            this.btnBrowseLogCopy = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblStatusDI = new System.Windows.Forms.Label();
            this.lblStatusDO = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCOM
            // 
            this.cmbCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOM.FormattingEnabled = true;
            this.cmbCOM.Location = new System.Drawing.Point(88, 23);
            this.cmbCOM.Name = "cmbCOM";
            this.cmbCOM.Size = new System.Drawing.Size(120, 20);
            this.cmbCOM.TabIndex = 0;
            // 
            // btnRefreshCOM
            // 
            this.btnRefreshCOM.Location = new System.Drawing.Point(228, 23);
            this.btnRefreshCOM.Name = "btnRefreshCOM";
            this.btnRefreshCOM.Size = new System.Drawing.Size(100, 50);
            this.btnRefreshCOM.TabIndex = 1;
            this.btnRefreshCOM.Text = "REFRESH";
            this.btnRefreshCOM.UseVisualStyleBackColor = true;
            this.btnRefreshCOM.Click += new System.EventHandler(this.btnRefreshCOM_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(334, 23);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 50);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(438, 23);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(100, 50);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "DISCONNECT";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblStatusCOM
            // 
            this.lblStatusCOM.AutoSize = true;
            this.lblStatusCOM.Location = new System.Drawing.Point(86, 61);
            this.lblStatusCOM.Name = "lblStatusCOM";
            this.lblStatusCOM.Size = new System.Drawing.Size(134, 12);
            this.lblStatusCOM.TabIndex = 4;
            this.lblStatusCOM.Text = "Comport Disconnected";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(88, 159);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(400, 21);
            this.txtLogPath.TabIndex = 5;
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Location = new System.Drawing.Point(508, 157);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(80, 23);
            this.btnBrowsePath.TabIndex = 6;
            this.btnBrowsePath.Text = "BROWSE";
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            this.btnBrowsePath.Click += new System.EventHandler(this.btnBrowsePath_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(88, 550);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 50);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(192, 550);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 50);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtStackName
            // 
            this.txtStackName.ForeColor = System.Drawing.Color.Gray;
            this.txtStackName.Location = new System.Drawing.Point(88, 251);
            this.txtStackName.Name = "txtStackName";
            this.txtStackName.Size = new System.Drawing.Size(188, 21);
            this.txtStackName.TabIndex = 7;
            this.txtStackName.Text = "Machine Name";
            // 
            // txtManager
            // 
            this.txtManager.ForeColor = System.Drawing.Color.Gray;
            this.txtManager.Location = new System.Drawing.Point(288, 251);
            this.txtManager.Name = "txtManager";
            this.txtManager.Size = new System.Drawing.Size(200, 21);
            this.txtManager.TabIndex = 8;
            this.txtManager.Text = "User";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(508, 251);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(80, 23);
            this.btnSaveSettings.TabIndex = 9;
            this.btnSaveSettings.Text = "SAVE";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // cmbModelList
            // 
            this.cmbModelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModelList.FormattingEnabled = true;
            this.cmbModelList.Location = new System.Drawing.Point(88, 304);
            this.cmbModelList.Name = "cmbModelList";
            this.cmbModelList.Size = new System.Drawing.Size(400, 20);
            this.cmbModelList.TabIndex = 10;
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Location = new System.Drawing.Point(508, 301);
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.Size = new System.Drawing.Size(80, 23);
            this.btnLoadModel.TabIndex = 11;
            this.btnLoadModel.Text = "LOAD";
            this.btnLoadModel.UseVisualStyleBackColor = true;
            this.btnLoadModel.Click += new System.EventHandler(this.btnLoadModel_Click);
            // 
            // btnOpenModelSetting
            // 
            this.btnOpenModelSetting.Location = new System.Drawing.Point(88, 346);
            this.btnOpenModelSetting.Name = "btnOpenModelSetting";
            this.btnOpenModelSetting.Size = new System.Drawing.Size(120, 23);
            this.btnOpenModelSetting.TabIndex = 12;
            this.btnOpenModelSetting.Text = "MODEL SETUP";
            this.btnOpenModelSetting.UseVisualStyleBackColor = true;
            this.btnOpenModelSetting.Click += new System.EventHandler(this.btnOpenModelSetting_Click);
            // 
            // lblProgramStatus
            // 
            this.lblProgramStatus.AutoSize = true;
            this.lblProgramStatus.Font = new System.Drawing.Font("굴림", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblProgramStatus.Location = new System.Drawing.Point(94, 616);
            this.lblProgramStatus.Name = "lblProgramStatus";
            this.lblProgramStatus.Size = new System.Drawing.Size(0, 48);
            this.lblProgramStatus.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "COM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "LOG";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "ID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "MODEL";
            // 
            // btnEditModel
            // 
            this.btnEditModel.Location = new System.Drawing.Point(228, 346);
            this.btnEditModel.Name = "btnEditModel";
            this.btnEditModel.Size = new System.Drawing.Size(120, 23);
            this.btnEditModel.TabIndex = 21;
            this.btnEditModel.Text = "MODEL EDIT";
            this.btnEditModel.UseVisualStyleBackColor = true;
            this.btnEditModel.Click += new System.EventHandler(this.btnEditModel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "LOG COPY";
            // 
            // txtLogCopyPath
            // 
            this.txtLogCopyPath.Location = new System.Drawing.Point(88, 203);
            this.txtLogCopyPath.Name = "txtLogCopyPath";
            this.txtLogCopyPath.Size = new System.Drawing.Size(400, 21);
            this.txtLogCopyPath.TabIndex = 23;
            // 
            // btnBrowseLogCopy
            // 
            this.btnBrowseLogCopy.Location = new System.Drawing.Point(508, 203);
            this.btnBrowseLogCopy.Name = "btnBrowseLogCopy";
            this.btnBrowseLogCopy.Size = new System.Drawing.Size(80, 23);
            this.btnBrowseLogCopy.TabIndex = 24;
            this.btnBrowseLogCopy.Text = "BROWSE";
            this.btnBrowseLogCopy.UseVisualStyleBackColor = true;
            this.btnBrowseLogCopy.Click += new System.EventHandler(this.btnBrowseLogCopy_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(429, 487);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 405);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "BARCODE";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 550);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 50);
            this.button1.TabIndex = 27;
            this.button1.Text = "RESET";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblStatusDI
            // 
            this.lblStatusDI.AutoSize = true;
            this.lblStatusDI.Location = new System.Drawing.Point(86, 87);
            this.lblStatusDI.Name = "lblStatusDI";
            this.lblStatusDI.Size = new System.Drawing.Size(24, 12);
            this.lblStatusDI.TabIndex = 28;
            this.lblStatusDI.Text = "DI :";
            // 
            // lblStatusDO
            // 
            this.lblStatusDO.AutoSize = true;
            this.lblStatusDO.Location = new System.Drawing.Point(86, 110);
            this.lblStatusDO.Name = "lblStatusDO";
            this.lblStatusDO.Size = new System.Drawing.Size(30, 12);
            this.lblStatusDO.TabIndex = 29;
            this.lblStatusDO.Text = "DO :";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(634, 1041);
            this.Controls.Add(this.lblStatusDO);
            this.Controls.Add(this.lblStatusDI);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnBrowseLogCopy);
            this.Controls.Add(this.txtLogCopyPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnEditModel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProgramStatus);
            this.Controls.Add(this.cmbCOM);
            this.Controls.Add(this.btnRefreshCOM);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.lblStatusCOM);
            this.Controls.Add(this.txtLogPath);
            this.Controls.Add(this.btnBrowsePath);
            this.Controls.Add(this.txtStackName);
            this.Controls.Add(this.txtManager);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.cmbModelList);
            this.Controls.Add(this.btnLoadModel);
            this.Controls.Add(this.btnOpenModelSetting);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Stacking MES Logger";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCOM;
        private System.Windows.Forms.Button btnRefreshCOM;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lblStatusCOM;
        private System.Windows.Forms.TextBox txtLogPath;
        private System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtStackName;
        private System.Windows.Forms.TextBox txtManager;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.ComboBox cmbModelList;
        private System.Windows.Forms.Button btnLoadModel;
        private System.Windows.Forms.Button btnOpenModelSetting;
        private System.Windows.Forms.Label lblProgramStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEditModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLogCopyPath;
        private System.Windows.Forms.Button btnBrowseLogCopy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblStatusDI;
        private System.Windows.Forms.Label lblStatusDO;
    }
}
