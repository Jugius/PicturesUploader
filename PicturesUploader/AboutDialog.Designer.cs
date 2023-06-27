namespace PicturesUploader
{
    partial class AboutDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkWWW = new System.Windows.Forms.LinkLabel();
            this.btCancel = new System.Windows.Forms.Button();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProgramName = new System.Windows.Forms.Label();
            this.pictureAppImage = new System.Windows.Forms.PictureBox();
            this._secondaryPanel = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppImage)).BeginInit();
            this._secondaryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.linkWWW);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.lblCopyright);
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.lblProgramName);
            this.panel1.Controls.Add(this.pictureAppImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(323, 107);
            this.panel1.TabIndex = 0;
            // 
            // linkWWW
            // 
            this.linkWWW.AutoSize = true;
            this.linkWWW.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.linkWWW.ForeColor = System.Drawing.Color.Coral;
            this.linkWWW.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(187)))));
            this.linkWWW.Location = new System.Drawing.Point(110, 87);
            this.linkWWW.Name = "linkWWW";
            this.linkWWW.Size = new System.Drawing.Size(112, 15);
            this.linkWWW.TabIndex = 7;
            this.linkWWW.TabStop = true;
            this.linkWWW.Text = "Сайт разработчика";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(363, 24);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.lblCopyright.Location = new System.Drawing.Point(110, 51);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(60, 15);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "Copyright";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.lblVersion.Location = new System.Drawing.Point(110, 30);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(94, 15);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Program Version";
            // 
            // lblProgramName
            // 
            this.lblProgramName.AutoSize = true;
            this.lblProgramName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblProgramName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.lblProgramName.Location = new System.Drawing.Point(110, 9);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(101, 17);
            this.lblProgramName.TabIndex = 1;
            this.lblProgramName.Text = "Program Name";
            // 
            // pictureAppImage
            // 
            this.pictureAppImage.Image = global::PicturesUploader.Properties.Resources.Hard_Disk_Server_icon;
            this.pictureAppImage.Location = new System.Drawing.Point(3, 3);
            this.pictureAppImage.Name = "pictureAppImage";
            this.pictureAppImage.Size = new System.Drawing.Size(101, 101);
            this.pictureAppImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureAppImage.TabIndex = 0;
            this.pictureAppImage.TabStop = false;
            // 
            // _secondaryPanel
            // 
            this._secondaryPanel.Controls.Add(this.btnOK);
            this._secondaryPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._secondaryPanel.Location = new System.Drawing.Point(0, 107);
            this._secondaryPanel.Name = "_secondaryPanel";
            this._secondaryPanel.Size = new System.Drawing.Size(323, 52);
            this._secondaryPanel.TabIndex = 17;
            this._secondaryPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._secondaryPanel_Paint);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(224, 14);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(323, 159);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._secondaryPanel);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О приложении";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureAppImage)).EndInit();
            this._secondaryPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureAppImage;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblProgramName;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Panel _secondaryPanel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.LinkLabel linkWWW;
    }
}