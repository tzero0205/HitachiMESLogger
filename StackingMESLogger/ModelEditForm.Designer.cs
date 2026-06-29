namespace HitachiMESLogger
{
    partial class ModelEditForm
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

        private void InitializeComponent()
        {
            this.cmbModelList = new System.Windows.Forms.ComboBox();
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.nudBarcodeQty = new System.Windows.Forms.NumericUpDown();
            this.txtBarcodeFormat = new System.Windows.Forms.TextBox();
            this.nudBarcodeLength = new System.Windows.Forms.NumericUpDown();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnDeleteModel = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labelSelectModel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeLength)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbModelList
            // 
            this.cmbModelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModelList.Location = new System.Drawing.Point(140, 20);
            this.cmbModelList.Name = "cmbModelList";
            this.cmbModelList.Size = new System.Drawing.Size(200, 20);
            this.cmbModelList.TabIndex = 0;
            this.cmbModelList.SelectedIndexChanged += new System.EventHandler(this.cmbModelList_SelectedIndexChanged);
            // 
            // txtModelName
            // 
            this.txtModelName.Location = new System.Drawing.Point(140, 60);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(200, 21);
            this.txtModelName.TabIndex = 1;
            // 
            // nudBarcodeQty
            // 
            this.nudBarcodeQty.Location = new System.Drawing.Point(140, 100);
            this.nudBarcodeQty.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudBarcodeQty.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBarcodeQty.Name = "nudBarcodeQty";
            this.nudBarcodeQty.Size = new System.Drawing.Size(120, 21);
            this.nudBarcodeQty.TabIndex = 2;
            this.nudBarcodeQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtBarcodeFormat
            // 
            this.txtBarcodeFormat.Location = new System.Drawing.Point(140, 140);
            this.txtBarcodeFormat.Name = "txtBarcodeFormat";
            this.txtBarcodeFormat.Size = new System.Drawing.Size(200, 21);
            this.txtBarcodeFormat.TabIndex = 3;
            // 
            // nudBarcodeLength
            // 
            this.nudBarcodeLength.Location = new System.Drawing.Point(140, 180);
            this.nudBarcodeLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBarcodeLength.Name = "nudBarcodeLength";
            this.nudBarcodeLength.Size = new System.Drawing.Size(120, 21);
            this.nudBarcodeLength.TabIndex = 4;
            this.nudBarcodeLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(20, 220);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(100, 30);
            this.btnSaveChanges.TabIndex = 5;
            this.btnSaveChanges.Text = "Save";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnDeleteModel
            // 
            this.btnDeleteModel.Location = new System.Drawing.Point(140, 220);
            this.btnDeleteModel.Name = "btnDeleteModel";
            this.btnDeleteModel.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteModel.TabIndex = 6;
            this.btnDeleteModel.Text = "Delete";
            this.btnDeleteModel.UseVisualStyleBackColor = true;
            this.btnDeleteModel.Click += new System.EventHandler(this.btnDeleteModel_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelSelectModel
            // 
            this.labelSelectModel.AutoSize = true;
            this.labelSelectModel.Location = new System.Drawing.Point(20, 23);
            this.labelSelectModel.Name = "labelSelectModel";
            this.labelSelectModel.Size = new System.Drawing.Size(83, 12);
            this.labelSelectModel.TabIndex = 8;
            this.labelSelectModel.Text = "Select Model:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Model Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Barcode Qty:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Barcode Format:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Barcode Length:";
            // 
            // ModelEditForm
            // 
            this.ClientSize = new System.Drawing.Size(380, 270);
            this.Controls.Add(this.cmbModelList);
            this.Controls.Add(this.txtModelName);
            this.Controls.Add(this.nudBarcodeQty);
            this.Controls.Add(this.txtBarcodeFormat);
            this.Controls.Add(this.nudBarcodeLength);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnDeleteModel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelSelectModel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "ModelEditForm";
            this.Text = "Edit Model";
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cmbModelList;
        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.NumericUpDown nudBarcodeQty;
        private System.Windows.Forms.TextBox txtBarcodeFormat;
        private System.Windows.Forms.NumericUpDown nudBarcodeLength;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnDeleteModel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelSelectModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
