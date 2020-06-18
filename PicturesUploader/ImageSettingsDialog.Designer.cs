namespace PicturesUploader
{
    partial class ImageSettingsDialog
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
            this.lblMessageCaption = new System.Windows.Forms.Label();
            this.rb_SaveImageSize = new System.Windows.Forms.RadioButton();
            this.rb_ChangeImageSize = new System.Windows.Forms.RadioButton();
            this.cmbSizes = new System.Windows.Forms.ComboBox();
            this._primaryPanel = new System.Windows.Forms.Panel();
            this._secondaryPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this._primaryPanel.SuspendLayout();
            this._secondaryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessageCaption
            // 
            this.lblMessageCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMessageCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMessageCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.lblMessageCaption.Location = new System.Drawing.Point(5, 5);
            this.lblMessageCaption.Name = "lblMessageCaption";
            this.lblMessageCaption.Size = new System.Drawing.Size(245, 61);
            this.lblMessageCaption.TabIndex = 2;
            this.lblMessageCaption.Text = "Настройка обработки изображений";
            this.lblMessageCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rb_SaveImageSize
            // 
            this.rb_SaveImageSize.AutoSize = true;
            this.rb_SaveImageSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_SaveImageSize.Location = new System.Drawing.Point(12, 69);
            this.rb_SaveImageSize.Name = "rb_SaveImageSize";
            this.rb_SaveImageSize.Size = new System.Drawing.Size(186, 19);
            this.rb_SaveImageSize.TabIndex = 0;
            this.rb_SaveImageSize.TabStop = true;
            this.rb_SaveImageSize.Text = "Оставлять размер оригинала";
            this.rb_SaveImageSize.UseVisualStyleBackColor = true;
            // 
            // rb_ChangeImageSize
            // 
            this.rb_ChangeImageSize.AutoSize = true;
            this.rb_ChangeImageSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_ChangeImageSize.Location = new System.Drawing.Point(12, 94);
            this.rb_ChangeImageSize.Name = "rb_ChangeImageSize";
            this.rb_ChangeImageSize.Size = new System.Drawing.Size(156, 19);
            this.rb_ChangeImageSize.TabIndex = 0;
            this.rb_ChangeImageSize.TabStop = true;
            this.rb_ChangeImageSize.Text = "Уменьшать до размера:";
            this.rb_ChangeImageSize.UseVisualStyleBackColor = true;
            // 
            // cmbSizes
            // 
            this.cmbSizes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSizes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbSizes.FormattingEnabled = true;
            this.cmbSizes.Location = new System.Drawing.Point(32, 119);
            this.cmbSizes.Name = "cmbSizes";
            this.cmbSizes.Size = new System.Drawing.Size(133, 23);
            this.cmbSizes.TabIndex = 35;
            // 
            // _primaryPanel
            // 
            this._primaryPanel.BackColor = System.Drawing.Color.White;
            this._primaryPanel.Controls.Add(this.rb_SaveImageSize);
            this._primaryPanel.Controls.Add(this.rb_ChangeImageSize);
            this._primaryPanel.Controls.Add(this.lblMessageCaption);
            this._primaryPanel.Controls.Add(this.cmbSizes);
            this._primaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._primaryPanel.Location = new System.Drawing.Point(0, 0);
            this._primaryPanel.Name = "_primaryPanel";
            this._primaryPanel.Padding = new System.Windows.Forms.Padding(5);
            this._primaryPanel.Size = new System.Drawing.Size(255, 161);
            this._primaryPanel.TabIndex = 45;
            this._primaryPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._primaryPanel_Paint);
            // 
            // _secondaryPanel
            // 
            this._secondaryPanel.Controls.Add(this.btnCancel);
            this._secondaryPanel.Controls.Add(this.btnOK);
            this._secondaryPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._secondaryPanel.Location = new System.Drawing.Point(0, 161);
            this._secondaryPanel.Name = "_secondaryPanel";
            this._secondaryPanel.Size = new System.Drawing.Size(255, 52);
            this._secondaryPanel.TabIndex = 46;
            this._secondaryPanel.Paint += new System.Windows.Forms.PaintEventHandler(this._secondaryPanel_Paint);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(156, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOK.Location = new System.Drawing.Point(63, 16);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ImageSettingsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(255, 213);
            this.Controls.Add(this._primaryPanel);
            this.Controls.Add(this._secondaryPanel);
            this.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageSettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.ImageSettingsDialog_Load);
            this._primaryPanel.ResumeLayout(false);
            this._primaryPanel.PerformLayout();
            this._secondaryPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessageCaption;
        private System.Windows.Forms.RadioButton rb_SaveImageSize;
        private System.Windows.Forms.RadioButton rb_ChangeImageSize;
        private System.Windows.Forms.ComboBox cmbSizes;
        private System.Windows.Forms.Panel _primaryPanel;
        private System.Windows.Forms.Panel _secondaryPanel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}