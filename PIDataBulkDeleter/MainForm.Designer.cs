namespace PIDataBulkDeleter
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtbStartTime = new System.Windows.Forms.TextBox();
            this.txtbEndTime = new System.Windows.Forms.TextBox();
            this.lblStatusMsg = new System.Windows.Forms.Label();
            this.lblRootPath = new System.Windows.Forms.Label();
            this.grpbDeletionType = new System.Windows.Forms.GroupBox();
            this.rbtnEFandTagValues = new System.Windows.Forms.RadioButton();
            this.rbtnEFOnly = new System.Windows.Forms.RadioButton();
            this.rbtnTagValuesOnly = new System.Windows.Forms.RadioButton();
            this.afElementFindCtrl1 = new OSIsoft.AF.UI.AFElementFindCtrl();
            this.btnDatabase = new System.Windows.Forms.Button();
            this.rtxtbMsgLog = new System.Windows.Forms.RichTextBox();
            this.numUpDownMaxSearchResults = new System.Windows.Forms.NumericUpDown();
            this.lblMaxSearchResults = new System.Windows.Forms.Label();
            this.numUpDownThreadCount = new System.Windows.Forms.NumericUpDown();
            this.lblThreads = new System.Windows.Forms.Label();
            this.ckbIncludeChildElements = new System.Windows.Forms.CheckBox();
            this.ckbSelectedAttributesOnly = new System.Windows.Forms.CheckBox();
            this.btnPIServers = new System.Windows.Forms.Button();
            this.txtAttrFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpbDeletionType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownMaxSearchResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownThreadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(9, 93);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(55, 13);
            this.lblStartTime.TabIndex = 9;
            this.lblStartTime.Text = "Start Time";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(9, 117);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(52, 13);
            this.lblEndTime.TabIndex = 10;
            this.lblEndTime.Text = "End Time";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 505);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(712, 17);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(191, 187);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(167, 30);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete Data";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Location = new System.Drawing.Point(398, 187);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(167, 30);
            this.btnAbort.TabIndex = 17;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(34, 342);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 19;
            // 
            // txtbStartTime
            // 
            this.txtbStartTime.Location = new System.Drawing.Point(120, 90);
            this.txtbStartTime.Name = "txtbStartTime";
            this.txtbStartTime.Size = new System.Drawing.Size(230, 20);
            this.txtbStartTime.TabIndex = 21;
            // 
            // txtbEndTime
            // 
            this.txtbEndTime.Location = new System.Drawing.Point(120, 117);
            this.txtbEndTime.Name = "txtbEndTime";
            this.txtbEndTime.Size = new System.Drawing.Size(230, 20);
            this.txtbEndTime.TabIndex = 22;
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.AutoSize = true;
            this.lblStatusMsg.Location = new System.Drawing.Point(34, 369);
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(0, 13);
            this.lblStatusMsg.TabIndex = 23;
            // 
            // lblRootPath
            // 
            this.lblRootPath.AutoSize = true;
            this.lblRootPath.Location = new System.Drawing.Point(9, 16);
            this.lblRootPath.Name = "lblRootPath";
            this.lblRootPath.Size = new System.Drawing.Size(79, 13);
            this.lblRootPath.TabIndex = 14;
            this.lblRootPath.Text = "Target Element";
            // 
            // grpbDeletionType
            // 
            this.grpbDeletionType.Controls.Add(this.rbtnEFandTagValues);
            this.grpbDeletionType.Controls.Add(this.rbtnEFOnly);
            this.grpbDeletionType.Controls.Add(this.rbtnTagValuesOnly);
            this.grpbDeletionType.Location = new System.Drawing.Point(480, 77);
            this.grpbDeletionType.Name = "grpbDeletionType";
            this.grpbDeletionType.Size = new System.Drawing.Size(247, 87);
            this.grpbDeletionType.TabIndex = 29;
            this.grpbDeletionType.TabStop = false;
            this.grpbDeletionType.Text = "Deletion Type";
            // 
            // rbtnEFandTagValues
            // 
            this.rbtnEFandTagValues.AutoSize = true;
            this.rbtnEFandTagValues.Location = new System.Drawing.Point(17, 66);
            this.rbtnEFandTagValues.Name = "rbtnEFandTagValues";
            this.rbtnEFandTagValues.Size = new System.Drawing.Size(197, 17);
            this.rbtnEFandTagValues.TabIndex = 31;
            this.rbtnEFandTagValues.Text = "Delete tag values and Event Frames";
            this.rbtnEFandTagValues.UseVisualStyleBackColor = true;
            // 
            // rbtnEFOnly
            // 
            this.rbtnEFOnly.AutoSize = true;
            this.rbtnEFOnly.Location = new System.Drawing.Point(17, 43);
            this.rbtnEFOnly.Name = "rbtnEFOnly";
            this.rbtnEFOnly.Size = new System.Drawing.Size(146, 17);
            this.rbtnEFOnly.TabIndex = 30;
            this.rbtnEFOnly.Text = "Delete Event Frames only";
            this.rbtnEFOnly.UseVisualStyleBackColor = true;
            // 
            // rbtnTagValuesOnly
            // 
            this.rbtnTagValuesOnly.AutoSize = true;
            this.rbtnTagValuesOnly.Checked = true;
            this.rbtnTagValuesOnly.Location = new System.Drawing.Point(17, 20);
            this.rbtnTagValuesOnly.Name = "rbtnTagValuesOnly";
            this.rbtnTagValuesOnly.Size = new System.Drawing.Size(130, 17);
            this.rbtnTagValuesOnly.TabIndex = 0;
            this.rbtnTagValuesOnly.TabStop = true;
            this.rbtnTagValuesOnly.Text = "Delete tag values only";
            this.rbtnTagValuesOnly.UseVisualStyleBackColor = true;
            // 
            // afElementFindCtrl1
            // 
            this.afElementFindCtrl1.AllowEmptyText = true;
            this.afElementFindCtrl1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.afElementFindCtrl1.Location = new System.Drawing.Point(120, 9);
            this.afElementFindCtrl1.Margin = new System.Windows.Forms.Padding(4);
            this.afElementFindCtrl1.MinimumSize = new System.Drawing.Size(0, 22);
            this.afElementFindCtrl1.Name = "afElementFindCtrl1";
            this.afElementFindCtrl1.ShowFindButton = true;
            this.afElementFindCtrl1.Size = new System.Drawing.Size(405, 24);
            this.afElementFindCtrl1.TabIndex = 32;
            this.afElementFindCtrl1.AFElementUpdated += new OSIsoft.AF.UI.AFElementFindCtrl.AFElementUpdatedEventHandler(this.afElementFindCtrl1_AFElementUpdated);
            // 
            // btnDatabase
            // 
            this.btnDatabase.Location = new System.Drawing.Point(532, 9);
            this.btnDatabase.Name = "btnDatabase";
            this.btnDatabase.Size = new System.Drawing.Size(99, 23);
            this.btnDatabase.TabIndex = 33;
            this.btnDatabase.Text = "Select Database";
            this.btnDatabase.UseVisualStyleBackColor = true;
            this.btnDatabase.Click += new System.EventHandler(this.btnDatabase_Click);
            // 
            // rtxtbMsgLog
            // 
            this.rtxtbMsgLog.DetectUrls = false;
            this.rtxtbMsgLog.Location = new System.Drawing.Point(12, 223);
            this.rtxtbMsgLog.Name = "rtxtbMsgLog";
            this.rtxtbMsgLog.ReadOnly = true;
            this.rtxtbMsgLog.Size = new System.Drawing.Size(715, 276);
            this.rtxtbMsgLog.TabIndex = 34;
            this.rtxtbMsgLog.Text = "";
            // 
            // numUpDownMaxSearchResults
            // 
            this.numUpDownMaxSearchResults.Location = new System.Drawing.Point(120, 144);
            this.numUpDownMaxSearchResults.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numUpDownMaxSearchResults.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownMaxSearchResults.Name = "numUpDownMaxSearchResults";
            this.numUpDownMaxSearchResults.Size = new System.Drawing.Size(102, 20);
            this.numUpDownMaxSearchResults.TabIndex = 35;
            this.numUpDownMaxSearchResults.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownMaxSearchResults.ValueChanged += new System.EventHandler(this.numUpDownMaxSearchResults_ValueChanged);
            // 
            // lblMaxSearchResults
            // 
            this.lblMaxSearchResults.AutoSize = true;
            this.lblMaxSearchResults.Location = new System.Drawing.Point(9, 146);
            this.lblMaxSearchResults.Name = "lblMaxSearchResults";
            this.lblMaxSearchResults.Size = new System.Drawing.Size(105, 13);
            this.lblMaxSearchResults.TabIndex = 36;
            this.lblMaxSearchResults.Text = "Max. Search Results";
            // 
            // numUpDownThreadCount
            // 
            this.numUpDownThreadCount.Location = new System.Drawing.Point(306, 144);
            this.numUpDownThreadCount.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numUpDownThreadCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownThreadCount.Name = "numUpDownThreadCount";
            this.numUpDownThreadCount.Size = new System.Drawing.Size(44, 20);
            this.numUpDownThreadCount.TabIndex = 37;
            this.numUpDownThreadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownThreadCount.ValueChanged += new System.EventHandler(this.numUpDownThreadCount_ValueChanged);
            // 
            // lblThreads
            // 
            this.lblThreads.AutoSize = true;
            this.lblThreads.Location = new System.Drawing.Point(228, 146);
            this.lblThreads.Name = "lblThreads";
            this.lblThreads.Size = new System.Drawing.Size(72, 13);
            this.lblThreads.TabIndex = 38;
            this.lblThreads.Text = "Max. Threads";
            // 
            // ckbIncludeChildElements
            // 
            this.ckbIncludeChildElements.AutoSize = true;
            this.ckbIncludeChildElements.Location = new System.Drawing.Point(120, 40);
            this.ckbIncludeChildElements.Name = "ckbIncludeChildElements";
            this.ckbIncludeChildElements.Size = new System.Drawing.Size(133, 17);
            this.ckbIncludeChildElements.TabIndex = 40;
            this.ckbIncludeChildElements.Text = "Include Child Elements";
            this.ckbIncludeChildElements.UseVisualStyleBackColor = true;
            this.ckbIncludeChildElements.CheckedChanged += new System.EventHandler(this.ckbIncludeChildElements_CheckedChanged);
            // 
            // ckbSelectedAttributesOnly
            // 
            this.ckbSelectedAttributesOnly.AutoSize = true;
            this.ckbSelectedAttributesOnly.Location = new System.Drawing.Point(120, 66);
            this.ckbSelectedAttributesOnly.Name = "ckbSelectedAttributesOnly";
            this.ckbSelectedAttributesOnly.Size = new System.Drawing.Size(139, 17);
            this.ckbSelectedAttributesOnly.TabIndex = 41;
            this.ckbSelectedAttributesOnly.Text = "Selected Attributes Only";
            this.ckbSelectedAttributesOnly.UseVisualStyleBackColor = true;
            this.ckbSelectedAttributesOnly.CheckedChanged += new System.EventHandler(this.ckbSelectedAttributesOnly_CheckedChanged);
            // 
            // btnPIServers
            // 
            this.btnPIServers.Location = new System.Drawing.Point(637, 9);
            this.btnPIServers.Name = "btnPIServers";
            this.btnPIServers.Size = new System.Drawing.Size(90, 23);
            this.btnPIServers.TabIndex = 42;
            this.btnPIServers.Text = "PI Servers";
            this.btnPIServers.UseVisualStyleBackColor = true;
            this.btnPIServers.Click += new System.EventHandler(this.btnPIServers_Click);
            // 
            // txtAttrFilter
            // 
            this.txtAttrFilter.Location = new System.Drawing.Point(378, 40);
            this.txtAttrFilter.Name = "txtAttrFilter";
            this.txtAttrFilter.Size = new System.Drawing.Size(349, 20);
            this.txtAttrFilter.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Attribute Path Filter";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 524);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAttrFilter);
            this.Controls.Add(this.btnPIServers);
            this.Controls.Add(this.ckbSelectedAttributesOnly);
            this.Controls.Add(this.ckbIncludeChildElements);
            this.Controls.Add(this.lblThreads);
            this.Controls.Add(this.numUpDownThreadCount);
            this.Controls.Add(this.lblMaxSearchResults);
            this.Controls.Add(this.numUpDownMaxSearchResults);
            this.Controls.Add(this.rtxtbMsgLog);
            this.Controls.Add(this.btnDatabase);
            this.Controls.Add(this.afElementFindCtrl1);
            this.Controls.Add(this.grpbDeletionType);
            this.Controls.Add(this.lblStatusMsg);
            this.Controls.Add(this.txtbEndTime);
            this.Controls.Add(this.txtbStartTime);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblRootPath);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.lblStartTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "PI Analysis Output Data Bulk Deleter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.grpbDeletionType.ResumeLayout(false);
            this.grpbDeletionType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownMaxSearchResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownThreadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtbStartTime;
        private System.Windows.Forms.TextBox txtbEndTime;
        private System.Windows.Forms.Label lblStatusMsg;
        private System.Windows.Forms.Label lblRootPath;
        private System.Windows.Forms.GroupBox grpbDeletionType;
        private System.Windows.Forms.RadioButton rbtnEFOnly;
        private System.Windows.Forms.RadioButton rbtnTagValuesOnly;
        private System.Windows.Forms.RadioButton rbtnEFandTagValues;
        private OSIsoft.AF.UI.AFElementFindCtrl afElementFindCtrl1;
        private System.Windows.Forms.Button btnDatabase;
        private System.Windows.Forms.RichTextBox rtxtbMsgLog;
        private System.Windows.Forms.NumericUpDown numUpDownMaxSearchResults;
        private System.Windows.Forms.Label lblMaxSearchResults;
        private System.Windows.Forms.NumericUpDown numUpDownThreadCount;
        private System.Windows.Forms.Label lblThreads;
        private System.Windows.Forms.CheckBox ckbIncludeChildElements;
        private System.Windows.Forms.CheckBox ckbSelectedAttributesOnly;
        private System.Windows.Forms.Button btnPIServers;
        private System.Windows.Forms.TextBox txtAttrFilter;
        private System.Windows.Forms.Label label1;
    }
}

