namespace CartoGPS
{
    partial class FormCartoGPS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCartoGPS));
            this.sst = new System.Windows.Forms.StatusStrip();
            this.tsslIco = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLon = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLat = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslAlt = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslSat = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslHDOP = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCol = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLig = new System.Windows.Forms.ToolStripStatusLabel();
            this.port = new System.IO.Ports.SerialPort(this.components);
            this.openFileDialogBitmap = new System.Windows.Forms.OpenFileDialog();
            this.labelEch = new System.Windows.Forms.Label();
            this.sst.SuspendLayout();
            this.SuspendLayout();
            // 
            // sst
            // 
            this.sst.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslIco,
            this.tsslLon,
            this.tsslLat,
            this.tsslAlt,
            this.tsslSat,
            this.tsslHDOP,
            this.tsslCol,
            this.tsslLig});
            resources.ApplyResources(this.sst, "sst");
            this.sst.Name = "sst";
            this.sst.Click += new System.EventHandler(this.SstClick);
            // 
            // tsslIco
            // 
            this.tsslIco.Image = global::CartoGPS.Resource.ko;
            this.tsslIco.Name = "tsslIco";
            resources.ApplyResources(this.tsslIco, "tsslIco");
            // 
            // tsslLon
            // 
            this.tsslLon.Name = "tsslLon";
            resources.ApplyResources(this.tsslLon, "tsslLon");
            // 
            // tsslLat
            // 
            this.tsslLat.Name = "tsslLat";
            resources.ApplyResources(this.tsslLat, "tsslLat");
            // 
            // tsslAlt
            // 
            this.tsslAlt.Name = "tsslAlt";
            resources.ApplyResources(this.tsslAlt, "tsslAlt");
            // 
            // tsslSat
            // 
            this.tsslSat.Name = "tsslSat";
            resources.ApplyResources(this.tsslSat, "tsslSat");
            // 
            // tsslHDOP
            // 
            this.tsslHDOP.Name = "tsslHDOP";
            resources.ApplyResources(this.tsslHDOP, "tsslHDOP");
            // 
            // tsslCol
            // 
            this.tsslCol.Name = "tsslCol";
            resources.ApplyResources(this.tsslCol, "tsslCol");
            // 
            // tsslLig
            // 
            this.tsslLig.Name = "tsslLig";
            resources.ApplyResources(this.tsslLig, "tsslLig");
            // 
            // port
            // 
            this.port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.PortDataReceived);
            // 
            // openFileDialogBitmap
            // 
            resources.ApplyResources(this.openFileDialogBitmap, "openFileDialogBitmap");
            // 
            // labelEch
            // 
            resources.ApplyResources(this.labelEch, "labelEch");
            this.labelEch.BackColor = System.Drawing.SystemColors.Highlight;
            this.labelEch.Name = "labelEch";
            // 
            // FormCartoGPS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelEch);
            this.Controls.Add(this.sst);
            this.DoubleBuffered = true;
            this.Name = "FormCartoGPS";
            this.Tag = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCartoGPSFormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormCartoGPSPaint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCartoGPSMouseDown);
            this.MouseLeave += new System.EventHandler(this.FormCartoGPSMouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCartoGPSMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormCartoGPSMouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.FormCartoGPSMouseWheel);
            this.Resize += new System.EventHandler(this.FormCartoGPSResize);
            this.sst.ResumeLayout(false);
            this.sst.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.StatusStrip sst;
        private System.IO.Ports.SerialPort port;
        private System.Windows.Forms.ToolStripStatusLabel tsslIco;
        private System.Windows.Forms.ToolStripStatusLabel tsslLon;
        private System.Windows.Forms.ToolStripStatusLabel tsslLat;
        private System.Windows.Forms.ToolStripStatusLabel tsslAlt;
        private System.Windows.Forms.ToolStripStatusLabel tsslSat;
        private System.Windows.Forms.ToolStripStatusLabel tsslHDOP;
        private System.Windows.Forms.OpenFileDialog openFileDialogBitmap;
        private System.Windows.Forms.ToolStripStatusLabel tsslCol;
        private System.Windows.Forms.ToolStripStatusLabel tsslLig;
        private System.Windows.Forms.Label labelEch;

        #endregion

    }
}


