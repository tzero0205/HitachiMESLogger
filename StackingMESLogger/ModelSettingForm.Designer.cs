namespace HitachiMESLogger
{
    partial class ModelSettingForm
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
            this.txtModelName = new System.Windows.Forms.TextBox();
            this.nudBarcodeQty = new System.Windows.Forms.NumericUpDown();
            this.txtBarcodeFormat = new System.Windows.Forms.TextBox();
            this.nudBarcodeLength = new System.Windows.Forms.NumericUpDown();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeLength)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModelName
            // 
            this.txtModelName.Location = new System.Drawing.Point(140, 20);
            this.txtModelName.Name = "txtModelName";
            this.txtModelName.Size = new System.Drawing.Size(200, 21);
            this.txtModelName.TabIndex = 0;
            // 
            // nudBarcodeQty
            // 
            this.nudBarcodeQty.Location = new System.Drawing.Point(140, 60);
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
            this.nudBarcodeQty.TabIndex = 1;
            this.nudBarcodeQty.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtBarcodeFormat
            // 
            this.txtBarcodeFormat.Location = new System.Drawing.Point(140, 100);
            this.txtBarcodeFormat.Name = "txtBarcodeFormat";
            this.txtBarcodeFormat.Size = new System.Drawing.Size(200, 21);
            this.txtBarcodeFormat.TabIndex = 2;
            // 
            // nudBarcodeLength
            // 
            this.nudBarcodeLength.Location = new System.Drawing.Point(140, 140);
            this.nudBarcodeLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBarcodeLength.Name = "nudBarcodeLength";
            this.nudBarcodeLength.Size = new System.Drawing.Size(120, 21);
            this.nudBarcodeLength.TabIndex = 3;
            this.nudBarcodeLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Location = new System.Drawing.Point(20, 180);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(100, 30);
            this.btnSaveModel.TabIndex = 4;
            this.btnSaveModel.Text = "Save";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Model Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Barcode Qty:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Barcode Format:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Barcode Length:";
            // 
            // ModelSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(380, 230);
            this.Controls.Add(this.txtModelName);
            this.Controls.Add(this.nudBarcodeQty);
            this.Controls.Add(this.txtBarcodeFormat);
            this.Controls.Add(this.nudBarcodeLength);
            this.Controls.Add(this.btnSaveModel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "ModelSettingForm";
            this.Text = "Model Setting";
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBarcodeLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtModelName;
        private System.Windows.Forms.NumericUpDown nudBarcodeQty;
        private System.Windows.Forms.TextBox txtBarcodeFormat;
        private System.Windows.Forms.NumericUpDown nudBarcodeLength;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
