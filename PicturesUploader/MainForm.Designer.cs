namespace PicturesUploader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkLinkToCell = new System.Windows.Forms.CheckBox();
            this.txtPictureFolderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbSaveLocal = new System.Windows.Forms.RadioButton();
            this.rbSaveFTP = new System.Windows.Forms.RadioButton();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkbUseNames = new System.Windows.Forms.CheckBox();
            this.txtEndRow = new System.Windows.Forms.TextBox();
            this.txtBeginRow = new System.Windows.Forms.TextBox();
            this.cmbLinks = new System.Windows.Forms.ComboBox();
            this.cmbNames = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumberOfColumns = new System.Windows.Forms.Label();
            this.cmbSheets = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumberOfRows = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFileName = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnQuickLoad = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.mnuSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSettingsFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mnuAbout = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSendLetter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuShowAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.mnuSettings.SuspendLayout();
            this.mnuAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkLinkToCell);
            this.groupBox3.Controls.Add(this.txtPictureFolderName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.rbSaveLocal);
            this.groupBox3.Controls.Add(this.rbSaveFTP);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.groupBox3.Location = new System.Drawing.Point(5, 284);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(289, 104);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            // 
            // chkLinkToCell
            // 
            this.chkLinkToCell.AutoSize = true;
            this.chkLinkToCell.Location = new System.Drawing.Point(6, 77);
            this.chkLinkToCell.Name = "chkLinkToCell";
            this.chkLinkToCell.Size = new System.Drawing.Size(265, 21);
            this.chkLinkToCell.TabIndex = 38;
            this.chkLinkToCell.Text = "Значение ячейки - полный путь к файлу";
            this.chkLinkToCell.UseVisualStyleBackColor = true;
            // 
            // txtPictureFolderName
            // 
            this.txtPictureFolderName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPictureFolderName.Location = new System.Drawing.Point(123, 42);
            this.txtPictureFolderName.Name = "txtPictureFolderName";
            this.txtPictureFolderName.Size = new System.Drawing.Size(160, 25);
            this.txtPictureFolderName.TabIndex = 37;
            this.toolTip1.SetToolTip(this.txtPictureFolderName, "Если название папки не указано, то оно будет сгенерировано и новая папка будет со" +
        "здана.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label1.Location = new System.Drawing.Point(7, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 37;
            this.label1.Text = "Название папки:";
            // 
            // rbSaveLocal
            // 
            this.rbSaveLocal.AutoSize = true;
            this.rbSaveLocal.Checked = true;
            this.rbSaveLocal.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rbSaveLocal.Location = new System.Drawing.Point(148, 12);
            this.rbSaveLocal.Name = "rbSaveLocal";
            this.rbSaveLocal.Size = new System.Drawing.Size(135, 21);
            this.rbSaveLocal.TabIndex = 7;
            this.rbSaveLocal.TabStop = true;
            this.rbSaveLocal.Text = "сохранить на диск";
            this.rbSaveLocal.UseVisualStyleBackColor = true;
            // 
            // rbSaveFTP
            // 
            this.rbSaveFTP.AutoSize = true;
            this.rbSaveFTP.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rbSaveFTP.Location = new System.Drawing.Point(10, 12);
            this.rbSaveFTP.Name = "rbSaveFTP";
            this.rbSaveFTP.Size = new System.Drawing.Size(107, 21);
            this.rbSaveFTP.TabIndex = 6;
            this.rbSaveFTP.Text = "залить на FTP";
            this.rbSaveFTP.UseVisualStyleBackColor = true;
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProgress.Location = new System.Drawing.Point(3, 44);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(283, 16);
            this.pbProgress.TabIndex = 44;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkbUseNames);
            this.groupBox2.Controls.Add(this.txtEndRow);
            this.groupBox2.Controls.Add(this.txtBeginRow);
            this.groupBox2.Controls.Add(this.cmbLinks);
            this.groupBox2.Controls.Add(this.cmbNames);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.groupBox2.Location = new System.Drawing.Point(5, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 133);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            // 
            // checkbUseNames
            // 
            this.checkbUseNames.AutoSize = true;
            this.checkbUseNames.Location = new System.Drawing.Point(210, 47);
            this.checkbUseNames.Name = "checkbUseNames";
            this.checkbUseNames.Size = new System.Drawing.Size(15, 14);
            this.checkbUseNames.TabIndex = 37;
            this.toolTip1.SetToolTip(this.checkbUseNames, "Если активно - имена для файлов берутся из указанного столбца. Иначе генерируются" +
        " случайно");
            this.checkbUseNames.UseVisualStyleBackColor = true;
            this.checkbUseNames.CheckedChanged += new System.EventHandler(this.checkbUseNames_CheckedChanged);
            // 
            // txtEndRow
            // 
            this.txtEndRow.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtEndRow.Location = new System.Drawing.Point(231, 100);
            this.txtEndRow.Name = "txtEndRow";
            this.txtEndRow.Size = new System.Drawing.Size(52, 25);
            this.txtEndRow.TabIndex = 36;
            // 
            // txtBeginRow
            // 
            this.txtBeginRow.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtBeginRow.Location = new System.Drawing.Point(231, 71);
            this.txtBeginRow.Name = "txtBeginRow";
            this.txtBeginRow.Size = new System.Drawing.Size(52, 25);
            this.txtBeginRow.TabIndex = 9;
            // 
            // cmbLinks
            // 
            this.cmbLinks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLinks.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbLinks.FormattingEnabled = true;
            this.cmbLinks.Location = new System.Drawing.Point(231, 13);
            this.cmbLinks.Name = "cmbLinks";
            this.cmbLinks.Size = new System.Drawing.Size(52, 25);
            this.cmbLinks.TabIndex = 35;
            // 
            // cmbNames
            // 
            this.cmbNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNames.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbNames.FormattingEnabled = true;
            this.cmbNames.Location = new System.Drawing.Point(231, 42);
            this.cmbNames.Name = "cmbNames";
            this.cmbNames.Size = new System.Drawing.Size(52, 25);
            this.cmbNames.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label5.Location = new System.Drawing.Point(6, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 17);
            this.label5.TabIndex = 33;
            this.label5.Text = "Завершить чтение на строке:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label4.Location = new System.Drawing.Point(7, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Столбец с гиперссылками:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 17);
            this.label6.TabIndex = 32;
            this.label6.Text = "Начать чтение со строки:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Столбец с именами файлов:";
            // 
            // lblNumberOfColumns
            // 
            this.lblNumberOfColumns.AutoSize = true;
            this.lblNumberOfColumns.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNumberOfColumns.Location = new System.Drawing.Point(109, 48);
            this.lblNumberOfColumns.Name = "lblNumberOfColumns";
            this.lblNumberOfColumns.Size = new System.Drawing.Size(69, 17);
            this.lblNumberOfColumns.TabIndex = 8;
            this.lblNumberOfColumns.Text = "Столбцов:";
            // 
            // cmbSheets
            // 
            this.cmbSheets.DisplayMember = "Name";
            this.cmbSheets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSheets.Enabled = false;
            this.cmbSheets.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbSheets.FormattingEnabled = true;
            this.cmbSheets.Location = new System.Drawing.Point(112, 22);
            this.cmbSheets.Name = "cmbSheets";
            this.cmbSheets.Size = new System.Drawing.Size(171, 25);
            this.cmbSheets.TabIndex = 6;
            this.toolTip1.SetToolTip(this.cmbSheets, "Выберите лист");
            this.cmbSheets.ValueMember = "Index";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Активный лист:";
            // 
            // lblNumberOfRows
            // 
            this.lblNumberOfRows.AutoSize = true;
            this.lblNumberOfRows.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblNumberOfRows.Location = new System.Drawing.Point(109, 71);
            this.lblNumberOfRows.Name = "lblNumberOfRows";
            this.lblNumberOfRows.Size = new System.Drawing.Size(46, 17);
            this.lblNumberOfRows.TabIndex = 9;
            this.lblNumberOfRows.Text = "Строк:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNumberOfRows);
            this.groupBox1.Controls.Add(this.lblNumberOfColumns);
            this.groupBox1.Controls.Add(this.cmbSheets);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.groupBox1.Location = new System.Drawing.Point(5, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 92);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Рабочая область";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnStart.Location = new System.Drawing.Point(216, 457);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 26);
            this.btnStart.TabIndex = 40;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblFileName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 27);
            this.panel1.TabIndex = 46;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFileName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblFileName.Location = new System.Drawing.Point(3, 5);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(43, 17);
            this.lblFileName.TabIndex = 39;
            this.lblFileName.Text = "label7";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblStatus);
            this.groupBox4.Controls.Add(this.pbProgress);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(5, 388);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(289, 63);
            this.groupBox4.TabIndex = 49;
            this.groupBox4.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblStatus.Location = new System.Drawing.Point(11, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(107, 17);
            this.lblStatus.TabIndex = 45;
            this.lblStatus.Text = "Текущая задача:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSettings);
            this.panel2.Controls.Add(this.btnAbout);
            this.panel2.Controls.Add(this.btnQuickLoad);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnOpenFile);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(1);
            this.panel2.Size = new System.Drawing.Size(289, 32);
            this.panel2.TabIndex = 50;
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Image = global::PicturesUploader.Properties.Resources.preferences_icon_24;
            this.btnSettings.Location = new System.Drawing.Point(228, 1);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(30, 30);
            this.btnSettings.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnSettings, "Настройки");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSettings_MouseDown);
            // 
            // btnAbout
            // 
            this.btnAbout.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Image = global::PicturesUploader.Properties.Resources.information_icon_24;
            this.btnAbout.Location = new System.Drawing.Point(258, 1);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(30, 30);
            this.btnAbout.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnAbout, "Справка");
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAbout_MouseDown);
            // 
            // btnQuickLoad
            // 
            this.btnQuickLoad.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnQuickLoad.FlatAppearance.BorderSize = 0;
            this.btnQuickLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickLoad.Image = global::PicturesUploader.Properties.Resources.quick_load_icon_24;
            this.btnQuickLoad.Location = new System.Drawing.Point(61, 1);
            this.btnQuickLoad.Name = "btnQuickLoad";
            this.btnQuickLoad.Size = new System.Drawing.Size(30, 30);
            this.btnQuickLoad.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnQuickLoad, "Быстрая загрузка");
            this.btnQuickLoad.UseVisualStyleBackColor = true;
            this.btnQuickLoad.Click += new System.EventHandler(this.btnQuickLoad_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::PicturesUploader.Properties.Resources.refresh_icon_241;
            this.btnRefresh.Location = new System.Drawing.Point(31, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnRefresh.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnRefresh, "Перезагрузить файл");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOpenFile.FlatAppearance.BorderSize = 0;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Image = global::PicturesUploader.Properties.Resources.open_file_icon_24;
            this.btnOpenFile.Location = new System.Drawing.Point(1, 1);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(30, 30);
            this.btnOpenFile.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnOpenFile, "Открыть файл");
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettingsFTP,
            this.mnuSettingsImage});
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(213, 48);
            // 
            // mnuSettingsFTP
            // 
            this.mnuSettingsFTP.Name = "mnuSettingsFTP";
            this.mnuSettingsFTP.Size = new System.Drawing.Size(212, 22);
            this.mnuSettingsFTP.Text = "Настройки FTP";
            this.mnuSettingsFTP.Click += new System.EventHandler(this.btnSettingsFTP_Click);
            // 
            // mnuSettingsImage
            // 
            this.mnuSettingsImage.Name = "mnuSettingsImage";
            this.mnuSettingsImage.Size = new System.Drawing.Size(212, 22);
            this.mnuSettingsImage.Text = "Обработка изображений";
            this.mnuSettingsImage.Click += new System.EventHandler(this.mnuSettingsImage_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            // 
            // mnuAbout
            // 
            this.mnuAbout.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp,
            this.mnuSendLetter,
            this.toolStripSeparator1,
            this.mnuCheckUpdates,
            this.mnuShowAbout});
            this.mnuAbout.Name = "mnuSettings";
            this.mnuAbout.Size = new System.Drawing.Size(205, 120);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(204, 22);
            this.mnuHelp.Text = "Справка";
            this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
            // 
            // mnuSendLetter
            // 
            this.mnuSendLetter.Name = "mnuSendLetter";
            this.mnuSendLetter.Size = new System.Drawing.Size(204, 22);
            this.mnuSendLetter.Text = "Обратная связь";
            this.mnuSendLetter.Click += new System.EventHandler(this.mnuSendLetter_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
            // 
            // mnuShowAbout
            // 
            this.mnuShowAbout.Name = "mnuShowAbout";
            this.mnuShowAbout.Size = new System.Drawing.Size(204, 22);
            this.mnuShowAbout.Text = "О программе";
            this.mnuShowAbout.Click += new System.EventHandler(this.mnuShowAbout_Click);
            // 
            // mnuCheckUpdates
            // 
            this.mnuCheckUpdates.Name = "mnuCheckUpdates";
            this.mnuCheckUpdates.Size = new System.Drawing.Size(204, 22);
            this.mnuCheckUpdates.Text = "Проверить обновления";
            this.mnuCheckUpdates.Click += new System.EventHandler(this.mnuCheckUpdates_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(299, 491);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pictures Uploader";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.mnuSettings.ResumeLayout(false);
            this.mnuAbout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbSaveLocal;
        private System.Windows.Forms.RadioButton rbSaveFTP;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtEndRow;
        private System.Windows.Forms.TextBox txtBeginRow;
        private System.Windows.Forms.ComboBox cmbLinks;
        private System.Windows.Forms.ComboBox cmbNames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumberOfColumns;
        private System.Windows.Forms.ComboBox cmbSheets;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNumberOfRows;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtPictureFolderName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnQuickLoad;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.ContextMenuStrip mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsFTP;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsImage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkbUseNames;
        private System.Windows.Forms.ContextMenuStrip mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuSendLetter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowAbout;
        private System.Windows.Forms.CheckBox chkLinkToCell;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckUpdates;
    }
}

