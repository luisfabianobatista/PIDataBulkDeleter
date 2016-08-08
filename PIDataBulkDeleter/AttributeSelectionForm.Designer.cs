namespace PIDataBulkDeleter
{
    partial class AttributeSelectionForm
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
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkedListBoxAttributeSelection = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(243, 413);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(96, 23);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(345, 413);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(96, 23);
            this.btnClearSelection.TabIndex = 3;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.Location = new System.Drawing.Point(544, 413);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(96, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(646, 413);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkedListBoxAttributeSelection
            // 
            this.checkedListBoxAttributeSelection.CheckOnClick = true;
            this.checkedListBoxAttributeSelection.FormattingEnabled = true;
            this.checkedListBoxAttributeSelection.HorizontalScrollbar = true;
            this.checkedListBoxAttributeSelection.Location = new System.Drawing.Point(12, 12);
            this.checkedListBoxAttributeSelection.Name = "checkedListBoxAttributeSelection";
            this.checkedListBoxAttributeSelection.Size = new System.Drawing.Size(969, 379);
            this.checkedListBoxAttributeSelection.TabIndex = 7;
            // 
            // AttributeSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(997, 448);
            this.Controls.Add(this.checkedListBoxAttributeSelection);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnClearSelection);
            this.Controls.Add(this.btnSelectAll);
            this.Name = "AttributeSelectionForm";
            this.Text = "Select Attributes";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AttributeSelectionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox checkedListBoxAttributeSelection;
    }
}