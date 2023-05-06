
namespace Barcode_Tiger
{
    partial class scan
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_Train_Line_1 = new Guna.UI2.WinForms.Guna2Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.status1 = new Guna.UI2.WinForms.Guna2Button();
            this.lblTrain1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRead1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.btn_Train_Line_1);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.status1);
            this.panel1.Controls.Add(this.lblTrain1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblRead1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(497, 156);
            this.panel1.TabIndex = 32;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(330, 96);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 53);
            this.comboBox1.TabIndex = 34;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // btn_Train_Line_1
            // 
            this.btn_Train_Line_1.BorderColor = System.Drawing.Color.Gray;
            this.btn_Train_Line_1.BorderThickness = 1;
            this.btn_Train_Line_1.CheckedState.Parent = this.btn_Train_Line_1;
            this.btn_Train_Line_1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Train_Line_1.CustomImages.Parent = this.btn_Train_Line_1;
            this.btn_Train_Line_1.FillColor = System.Drawing.Color.Silver;
            this.btn_Train_Line_1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_Train_Line_1.ForeColor = System.Drawing.Color.White;
            this.btn_Train_Line_1.HoverState.Parent = this.btn_Train_Line_1;
            this.btn_Train_Line_1.Location = new System.Drawing.Point(418, 31);
            this.btn_Train_Line_1.Name = "btn_Train_Line_1";
            this.btn_Train_Line_1.ShadowDecoration.Parent = this.btn_Train_Line_1;
            this.btn_Train_Line_1.Size = new System.Drawing.Size(72, 60);
            this.btn_Train_Line_1.TabIndex = 33;
            this.btn_Train_Line_1.Text = "Change Barcode";
            this.btn_Train_Line_1.Click += new System.EventHandler(this.btn_Train_Line_1_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Blue;
            this.panel7.Controls.Add(this.lblStatus1);
            this.panel7.Controls.Add(this.label26);
            this.panel7.Controls.Add(this.label21);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(495, 29);
            this.panel7.TabIndex = 13;
            // 
            // lblStatus1
            // 
            this.lblStatus1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus1.ForeColor = System.Drawing.Color.Lime;
            this.lblStatus1.Location = new System.Drawing.Point(294, 2);
            this.lblStatus1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(135, 27);
            this.lblStatus1.TabIndex = 2;
            this.lblStatus1.Text = "...";
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(64, 1);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(106, 27);
            this.label26.TabIndex = 1;
            this.label26.Text = "Scanner 1";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(5, 1);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 27);
            this.label21.TabIndex = 0;
            this.label21.Text = "LINE 1: ";
            // 
            // status1
            // 
            this.status1.BorderRadius = 15;
            this.status1.CheckedState.Parent = this.status1;
            this.status1.CustomImages.Parent = this.status1;
            this.status1.FillColor = System.Drawing.Color.Silver;
            this.status1.Font = new System.Drawing.Font("Segoe UI", 35F, System.Drawing.FontStyle.Bold);
            this.status1.ForeColor = System.Drawing.Color.White;
            this.status1.HoverState.Parent = this.status1;
            this.status1.Location = new System.Drawing.Point(2, 95);
            this.status1.Name = "status1";
            this.status1.ShadowDecoration.Parent = this.status1;
            this.status1.Size = new System.Drawing.Size(322, 56);
            this.status1.TabIndex = 12;
            this.status1.Text = "OK/NG";
            // 
            // lblTrain1
            // 
            this.lblTrain1.AutoSize = true;
            this.lblTrain1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrain1.ForeColor = System.Drawing.Color.Blue;
            this.lblTrain1.Location = new System.Drawing.Point(137, 29);
            this.lblTrain1.Name = "lblTrain1";
            this.lblTrain1.Size = new System.Drawing.Size(24, 28);
            this.lblTrain1.TabIndex = 10;
            this.lblTrain1.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 28);
            this.label4.TabIndex = 9;
            this.label4.Text = "Barcode Train: ";
            // 
            // lblRead1
            // 
            this.lblRead1.AutoSize = true;
            this.lblRead1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRead1.Location = new System.Drawing.Point(137, 63);
            this.lblRead1.Name = "lblRead1";
            this.lblRead1.Size = new System.Drawing.Size(24, 28);
            this.lblRead1.TabIndex = 8;
            this.lblRead1.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 28);
            this.label1.TabIndex = 7;
            this.label1.Text = "Barcode Read: ";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // scan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "scan";
            this.Size = new System.Drawing.Size(500, 160);
            this.Load += new System.EventHandler(this.scan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label21;
        private Guna.UI2.WinForms.Guna2Button status1;
        private System.Windows.Forms.Label lblTrain1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRead1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btn_Train_Line_1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
