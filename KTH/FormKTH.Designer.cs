namespace KTH {
    partial class FormKTH {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKTH));
            this.panelMonitoring = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxDisplayUpdate = new System.Windows.Forms.ComboBox();
            this.labelDisplayupdate = new System.Windows.Forms.Label();
            this.labelAof = new System.Windows.Forms.Label();
            this.comboBoxAof = new System.Windows.Forms.ComboBox();
            this.labelUnit = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.comboBoxUnit = new System.Windows.Forms.ComboBox();
            this.comboBoxSpeed = new System.Windows.Forms.ComboBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonBootloader = new System.Windows.Forms.Button();
            this.buttonCalOffset = new System.Windows.Forms.Button();
            this.buttonCalGain = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonStorecfg = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelCalProfile = new System.Windows.Forms.Label();
            this.comboBoxCalProfile = new System.Windows.Forms.ComboBox();
            this.textBoxCalTc1ThOffset = new System.Windows.Forms.TextBox();
            this.textBoxCalTc1Gain = new System.Windows.Forms.TextBox();
            this.labelCalTc1Tc0 = new System.Windows.Forms.Label();
            this.labelCalTc1TcGain = new System.Windows.Forms.Label();
            this.labelCalTc1ThOffset = new System.Windows.Forms.Label();
            this.textBoxCalTc1Zero = new System.Windows.Forms.TextBox();
            this.textBoxCalTc2Zero = new System.Windows.Forms.TextBox();
            this.textBoxCalTc2Gain = new System.Windows.Forms.TextBox();
            this.textBoxCalTc2ThOffset = new System.Windows.Forms.TextBox();
            this.labelTc1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonChangeCalProfile = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.labelFwVer = new System.Windows.Forms.Label();
            this.labelUid = new System.Windows.Forms.Label();
            this.labelFwVerValue = new System.Windows.Forms.Label();
            this.labelUidValue = new System.Windows.Forms.Label();
            this.buttonApplyCal = new System.Windows.Forms.Button();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.buttonOpenPort = new System.Windows.Forms.Button();
            this.buttonRefreshPorts = new System.Windows.Forms.Button();
            this.buttonLog = new System.Windows.Forms.Button();
            this.buttonWriteToFile = new System.Windows.Forms.Button();
            this.checkBoxTc1 = new System.Windows.Forms.CheckBox();
            this.checkBoxTc2 = new System.Windows.Forms.CheckBox();
            this.buttonUpdateLut = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMonitoring
            // 
            this.panelMonitoring.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMonitoring.AutoScroll = true;
            this.panelMonitoring.Location = new System.Drawing.Point(9, 138);
            this.panelMonitoring.Margin = new System.Windows.Forms.Padding(2);
            this.panelMonitoring.Name = "panelMonitoring";
            this.panelMonitoring.Size = new System.Drawing.Size(795, 328);
            this.panelMonitoring.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDisplayUpdate, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelDisplayupdate, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelAof, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxAof, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelUnit, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelSpeed, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxUnit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxSpeed, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(157, 103);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // comboBoxDisplayUpdate
            // 
            this.comboBoxDisplayUpdate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboBoxDisplayUpdate.FormattingEnabled = true;
            this.comboBoxDisplayUpdate.Location = new System.Drawing.Point(78, 78);
            this.comboBoxDisplayUpdate.Name = "comboBoxDisplayUpdate";
            this.comboBoxDisplayUpdate.Size = new System.Drawing.Size(76, 21);
            this.comboBoxDisplayUpdate.TabIndex = 8;
            // 
            // labelDisplayupdate
            // 
            this.labelDisplayupdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelDisplayupdate.AutoSize = true;
            this.labelDisplayupdate.Location = new System.Drawing.Point(3, 82);
            this.labelDisplayupdate.Name = "labelDisplayupdate";
            this.labelDisplayupdate.Size = new System.Drawing.Size(67, 13);
            this.labelDisplayupdate.TabIndex = 7;
            this.labelDisplayupdate.Text = "Disp. update";
            // 
            // labelAof
            // 
            this.labelAof.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAof.AutoSize = true;
            this.labelAof.Location = new System.Drawing.Point(3, 6);
            this.labelAof.Name = "labelAof";
            this.labelAof.Size = new System.Drawing.Size(44, 13);
            this.labelAof.TabIndex = 0;
            this.labelAof.Text = "Auto-off";
            // 
            // comboBoxAof
            // 
            this.comboBoxAof.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboBoxAof.FormattingEnabled = true;
            this.comboBoxAof.Location = new System.Drawing.Point(78, 3);
            this.comboBoxAof.Name = "comboBoxAof";
            this.comboBoxAof.Size = new System.Drawing.Size(76, 21);
            this.comboBoxAof.TabIndex = 1;
            // 
            // labelUnit
            // 
            this.labelUnit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUnit.AutoSize = true;
            this.labelUnit.Location = new System.Drawing.Point(3, 31);
            this.labelUnit.Name = "labelUnit";
            this.labelUnit.Size = new System.Drawing.Size(26, 13);
            this.labelUnit.TabIndex = 2;
            this.labelUnit.Text = "Unit";
            // 
            // labelSpeed
            // 
            this.labelSpeed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(3, 56);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(38, 13);
            this.labelSpeed.TabIndex = 3;
            this.labelSpeed.Text = "Speed";
            // 
            // comboBoxUnit
            // 
            this.comboBoxUnit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboBoxUnit.FormattingEnabled = true;
            this.comboBoxUnit.Location = new System.Drawing.Point(78, 28);
            this.comboBoxUnit.Name = "comboBoxUnit";
            this.comboBoxUnit.Size = new System.Drawing.Size(76, 21);
            this.comboBoxUnit.TabIndex = 5;
            // 
            // comboBoxSpeed
            // 
            this.comboBoxSpeed.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboBoxSpeed.FormattingEnabled = true;
            this.comboBoxSpeed.Location = new System.Drawing.Point(78, 53);
            this.comboBoxSpeed.Name = "comboBoxSpeed";
            this.comboBoxSpeed.Size = new System.Drawing.Size(76, 21);
            this.comboBoxSpeed.TabIndex = 6;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(658, 75);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(70, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonBootloader
            // 
            this.buttonBootloader.Location = new System.Drawing.Point(734, 75);
            this.buttonBootloader.Name = "buttonBootloader";
            this.buttonBootloader.Size = new System.Drawing.Size(70, 23);
            this.buttonBootloader.TabIndex = 3;
            this.buttonBootloader.Text = "Bootloader";
            this.buttonBootloader.UseVisualStyleBackColor = true;
            this.buttonBootloader.Click += new System.EventHandler(this.buttonBootloader_Click);
            // 
            // buttonCalOffset
            // 
            this.buttonCalOffset.Location = new System.Drawing.Point(404, 5);
            this.buttonCalOffset.Name = "buttonCalOffset";
            this.buttonCalOffset.Size = new System.Drawing.Size(105, 23);
            this.buttonCalOffset.TabIndex = 4;
            this.buttonCalOffset.Text = "Calibrate Offset";
            this.buttonCalOffset.UseVisualStyleBackColor = true;
            this.buttonCalOffset.Click += new System.EventHandler(this.buttonCalOffset_Click);
            // 
            // buttonCalGain
            // 
            this.buttonCalGain.Enabled = false;
            this.buttonCalGain.Location = new System.Drawing.Point(404, 31);
            this.buttonCalGain.Name = "buttonCalGain";
            this.buttonCalGain.Size = new System.Drawing.Size(105, 23);
            this.buttonCalGain.TabIndex = 5;
            this.buttonCalGain.Text = "Cal. Gain";
            this.buttonCalGain.UseVisualStyleBackColor = true;
            this.buttonCalGain.Click += new System.EventHandler(this.buttonCalGain_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(9, 106);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(157, 23);
            this.buttonApply.TabIndex = 6;
            this.buttonApply.Text = "Apply settings";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonStorecfg
            // 
            this.buttonStorecfg.Location = new System.Drawing.Point(584, 75);
            this.buttonStorecfg.Name = "buttonStorecfg";
            this.buttonStorecfg.Size = new System.Drawing.Size(70, 23);
            this.buttonStorecfg.TabIndex = 7;
            this.buttonStorecfg.Text = "Store cfg";
            this.buttonStorecfg.UseVisualStyleBackColor = true;
            this.buttonStorecfg.Click += new System.EventHandler(this.buttonStorecfg_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.labelCalProfile, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxCalProfile, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCalTc1ThOffset, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCalTc1Gain, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelCalTc1Tc0, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelCalTc1TcGain, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelCalTc1ThOffset, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCalTc1Zero, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCalTc2Zero, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCalTc2Gain, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCalTc2ThOffset, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelTc1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonChangeCalProfile, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(172, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(226, 125);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // labelCalProfile
            // 
            this.labelCalProfile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCalProfile.AutoSize = true;
            this.labelCalProfile.Location = new System.Drawing.Point(3, 6);
            this.labelCalProfile.Name = "labelCalProfile";
            this.labelCalProfile.Size = new System.Drawing.Size(54, 13);
            this.labelCalProfile.TabIndex = 13;
            this.labelCalProfile.Text = "Cal.Profile";
            // 
            // comboBoxCalProfile
            // 
            this.comboBoxCalProfile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.comboBoxCalProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCalProfile.FormattingEnabled = true;
            this.comboBoxCalProfile.Location = new System.Drawing.Point(64, 3);
            this.comboBoxCalProfile.Name = "comboBoxCalProfile";
            this.comboBoxCalProfile.Size = new System.Drawing.Size(76, 21);
            this.comboBoxCalProfile.TabIndex = 11;
            // 
            // textBoxCalTc1ThOffset
            // 
            this.textBoxCalTc1ThOffset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxCalTc1ThOffset.Location = new System.Drawing.Point(63, 103);
            this.textBoxCalTc1ThOffset.Name = "textBoxCalTc1ThOffset";
            this.textBoxCalTc1ThOffset.Size = new System.Drawing.Size(77, 20);
            this.textBoxCalTc1ThOffset.TabIndex = 6;
            // 
            // textBoxCalTc1Gain
            // 
            this.textBoxCalTc1Gain.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxCalTc1Gain.Location = new System.Drawing.Point(63, 78);
            this.textBoxCalTc1Gain.Name = "textBoxCalTc1Gain";
            this.textBoxCalTc1Gain.Size = new System.Drawing.Size(77, 20);
            this.textBoxCalTc1Gain.TabIndex = 5;
            // 
            // labelCalTc1Tc0
            // 
            this.labelCalTc1Tc0.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCalTc1Tc0.AutoSize = true;
            this.labelCalTc1Tc0.Location = new System.Drawing.Point(3, 56);
            this.labelCalTc1Tc0.Name = "labelCalTc1Tc0";
            this.labelCalTc1Tc0.Size = new System.Drawing.Size(51, 13);
            this.labelCalTc1Tc0.TabIndex = 0;
            this.labelCalTc1Tc0.Text = "Zero-bias";
            // 
            // labelCalTc1TcGain
            // 
            this.labelCalTc1TcGain.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCalTc1TcGain.AutoSize = true;
            this.labelCalTc1TcGain.Location = new System.Drawing.Point(3, 81);
            this.labelCalTc1TcGain.Name = "labelCalTc1TcGain";
            this.labelCalTc1TcGain.Size = new System.Drawing.Size(29, 13);
            this.labelCalTc1TcGain.TabIndex = 2;
            this.labelCalTc1TcGain.Text = "Gain";
            // 
            // labelCalTc1ThOffset
            // 
            this.labelCalTc1ThOffset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCalTc1ThOffset.AutoSize = true;
            this.labelCalTc1ThOffset.Location = new System.Drawing.Point(3, 106);
            this.labelCalTc1ThOffset.Name = "labelCalTc1ThOffset";
            this.labelCalTc1ThOffset.Size = new System.Drawing.Size(48, 13);
            this.labelCalTc1ThOffset.TabIndex = 3;
            this.labelCalTc1ThOffset.Text = "ThOffset";
            // 
            // textBoxCalTc1Zero
            // 
            this.textBoxCalTc1Zero.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxCalTc1Zero.Location = new System.Drawing.Point(63, 53);
            this.textBoxCalTc1Zero.Name = "textBoxCalTc1Zero";
            this.textBoxCalTc1Zero.Size = new System.Drawing.Size(77, 20);
            this.textBoxCalTc1Zero.TabIndex = 4;
            // 
            // textBoxCalTc2Zero
            // 
            this.textBoxCalTc2Zero.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxCalTc2Zero.Location = new System.Drawing.Point(146, 53);
            this.textBoxCalTc2Zero.Name = "textBoxCalTc2Zero";
            this.textBoxCalTc2Zero.Size = new System.Drawing.Size(77, 20);
            this.textBoxCalTc2Zero.TabIndex = 7;
            // 
            // textBoxCalTc2Gain
            // 
            this.textBoxCalTc2Gain.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxCalTc2Gain.Location = new System.Drawing.Point(146, 78);
            this.textBoxCalTc2Gain.Name = "textBoxCalTc2Gain";
            this.textBoxCalTc2Gain.Size = new System.Drawing.Size(77, 20);
            this.textBoxCalTc2Gain.TabIndex = 8;
            // 
            // textBoxCalTc2ThOffset
            // 
            this.textBoxCalTc2ThOffset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxCalTc2ThOffset.Location = new System.Drawing.Point(146, 103);
            this.textBoxCalTc2ThOffset.Name = "textBoxCalTc2ThOffset";
            this.textBoxCalTc2ThOffset.Size = new System.Drawing.Size(77, 20);
            this.textBoxCalTc2ThOffset.TabIndex = 9;
            // 
            // labelTc1
            // 
            this.labelTc1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTc1.AutoSize = true;
            this.labelTc1.Location = new System.Drawing.Point(88, 31);
            this.labelTc1.Name = "labelTc1";
            this.labelTc1.Size = new System.Drawing.Size(26, 13);
            this.labelTc1.TabIndex = 10;
            this.labelTc1.Text = "Tc1";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Tc2";
            // 
            // buttonChangeCalProfile
            // 
            this.buttonChangeCalProfile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonChangeCalProfile.Location = new System.Drawing.Point(144, 1);
            this.buttonChangeCalProfile.Margin = new System.Windows.Forms.Padding(1);
            this.buttonChangeCalProfile.Name = "buttonChangeCalProfile";
            this.buttonChangeCalProfile.Size = new System.Drawing.Size(81, 23);
            this.buttonChangeCalProfile.TabIndex = 12;
            this.buttonChangeCalProfile.Text = "Change";
            this.buttonChangeCalProfile.UseVisualStyleBackColor = true;
            this.buttonChangeCalProfile.Click += new System.EventHandler(this.buttonChangeCalProfile_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.labelFwVer, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelUid, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelFwVerValue, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelUidValue, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(578, 104);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(226, 29);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // labelFwVer
            // 
            this.labelFwVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFwVer.AutoSize = true;
            this.labelFwVer.Location = new System.Drawing.Point(3, 15);
            this.labelFwVer.Name = "labelFwVer";
            this.labelFwVer.Size = new System.Drawing.Size(46, 13);
            this.labelFwVer.TabIndex = 4;
            this.labelFwVer.Text = "FW Ver.";
            // 
            // labelUid
            // 
            this.labelUid.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelUid.AutoSize = true;
            this.labelUid.Location = new System.Drawing.Point(3, 0);
            this.labelUid.Name = "labelUid";
            this.labelUid.Size = new System.Drawing.Size(26, 13);
            this.labelUid.TabIndex = 0;
            this.labelUid.Text = "UID";
            // 
            // labelFwVerValue
            // 
            this.labelFwVerValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelFwVerValue.AutoSize = true;
            this.labelFwVerValue.Location = new System.Drawing.Point(223, 15);
            this.labelFwVerValue.Name = "labelFwVerValue";
            this.labelFwVerValue.Size = new System.Drawing.Size(0, 13);
            this.labelFwVerValue.TabIndex = 2;
            // 
            // labelUidValue
            // 
            this.labelUidValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelUidValue.AutoSize = true;
            this.labelUidValue.Location = new System.Drawing.Point(223, 0);
            this.labelUidValue.Name = "labelUidValue";
            this.labelUidValue.Size = new System.Drawing.Size(0, 13);
            this.labelUidValue.TabIndex = 3;
            // 
            // buttonApplyCal
            // 
            this.buttonApplyCal.Location = new System.Drawing.Point(404, 106);
            this.buttonApplyCal.Name = "buttonApplyCal";
            this.buttonApplyCal.Size = new System.Drawing.Size(105, 23);
            this.buttonApplyCal.TabIndex = 11;
            this.buttonApplyCal.Text = "Apply Calibration";
            this.buttonApplyCal.UseVisualStyleBackColor = true;
            this.buttonApplyCal.Click += new System.EventHandler(this.buttonApplyCal_Click);
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(515, 6);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(137, 21);
            this.comboBoxPorts.TabIndex = 12;
            // 
            // buttonOpenPort
            // 
            this.buttonOpenPort.Location = new System.Drawing.Point(734, 5);
            this.buttonOpenPort.Name = "buttonOpenPort";
            this.buttonOpenPort.Size = new System.Drawing.Size(70, 23);
            this.buttonOpenPort.TabIndex = 13;
            this.buttonOpenPort.Text = "Open";
            this.buttonOpenPort.UseVisualStyleBackColor = true;
            this.buttonOpenPort.Click += new System.EventHandler(this.buttonOpenPort_Click);
            // 
            // buttonRefreshPorts
            // 
            this.buttonRefreshPorts.Location = new System.Drawing.Point(658, 5);
            this.buttonRefreshPorts.Name = "buttonRefreshPorts";
            this.buttonRefreshPorts.Size = new System.Drawing.Size(70, 23);
            this.buttonRefreshPorts.TabIndex = 14;
            this.buttonRefreshPorts.Text = "Refresh";
            this.buttonRefreshPorts.UseVisualStyleBackColor = true;
            this.buttonRefreshPorts.Click += new System.EventHandler(this.buttonRefreshPorts_Click);
            // 
            // buttonLog
            // 
            this.buttonLog.Location = new System.Drawing.Point(734, 40);
            this.buttonLog.Name = "buttonLog";
            this.buttonLog.Size = new System.Drawing.Size(70, 23);
            this.buttonLog.TabIndex = 15;
            this.buttonLog.Text = "Log to CSV";
            this.buttonLog.UseVisualStyleBackColor = true;
            this.buttonLog.Click += new System.EventHandler(this.buttonLog_Click);
            // 
            // buttonWriteToFile
            // 
            this.buttonWriteToFile.Location = new System.Drawing.Point(658, 40);
            this.buttonWriteToFile.Name = "buttonWriteToFile";
            this.buttonWriteToFile.Size = new System.Drawing.Size(70, 23);
            this.buttonWriteToFile.TabIndex = 16;
            this.buttonWriteToFile.Text = "Write to file";
            this.buttonWriteToFile.UseVisualStyleBackColor = true;
            this.buttonWriteToFile.Click += new System.EventHandler(this.buttonWriteToFile_Click);
            // 
            // checkBoxTc1
            // 
            this.checkBoxTc1.AutoSize = true;
            this.checkBoxTc1.Checked = true;
            this.checkBoxTc1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTc1.Location = new System.Drawing.Point(561, 44);
            this.checkBoxTc1.Name = "checkBoxTc1";
            this.checkBoxTc1.Size = new System.Drawing.Size(46, 17);
            this.checkBoxTc1.TabIndex = 17;
            this.checkBoxTc1.Text = "TC1";
            this.checkBoxTc1.UseVisualStyleBackColor = true;
            this.checkBoxTc1.CheckedChanged += new System.EventHandler(this.checkBoxTc1_CheckedChanged);
            // 
            // checkBoxTc2
            // 
            this.checkBoxTc2.AutoSize = true;
            this.checkBoxTc2.Checked = true;
            this.checkBoxTc2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTc2.Location = new System.Drawing.Point(608, 44);
            this.checkBoxTc2.Name = "checkBoxTc2";
            this.checkBoxTc2.Size = new System.Drawing.Size(46, 17);
            this.checkBoxTc2.TabIndex = 18;
            this.checkBoxTc2.Text = "TC2";
            this.checkBoxTc2.UseVisualStyleBackColor = true;
            this.checkBoxTc2.CheckedChanged += new System.EventHandler(this.checkBoxTc2_CheckedChanged);
            // 
            // buttonUpdateLut
            // 
            this.buttonUpdateLut.Location = new System.Drawing.Point(404, 75);
            this.buttonUpdateLut.Name = "buttonUpdateLut";
            this.buttonUpdateLut.Size = new System.Drawing.Size(174, 23);
            this.buttonUpdateLut.TabIndex = 19;
            this.buttonUpdateLut.Text = "Update LUT";
            this.buttonUpdateLut.UseVisualStyleBackColor = true;
            this.buttonUpdateLut.Click += new System.EventHandler(this.buttonUpdateLut_Click);
            // 
            // FormKTH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 477);
            this.Controls.Add(this.buttonUpdateLut);
            this.Controls.Add(this.checkBoxTc2);
            this.Controls.Add(this.checkBoxTc1);
            this.Controls.Add(this.buttonWriteToFile);
            this.Controls.Add(this.buttonLog);
            this.Controls.Add(this.buttonRefreshPorts);
            this.Controls.Add(this.buttonOpenPort);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.buttonApplyCal);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.buttonStorecfg);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonCalGain);
            this.Controls.Add(this.buttonCalOffset);
            this.Controls.Add(this.buttonBootloader);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelMonitoring);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormKTH";
            this.Text = "KTH Thermometer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormKTH_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMonitoring;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelAof;
        private System.Windows.Forms.ComboBox comboBoxAof;
        private System.Windows.Forms.Label labelUnit;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.ComboBox comboBoxUnit;
        private System.Windows.Forms.ComboBox comboBoxSpeed;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonBootloader;
        private System.Windows.Forms.Button buttonCalOffset;
        private System.Windows.Forms.Button buttonCalGain;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.ComboBox comboBoxDisplayUpdate;
        private System.Windows.Forms.Label labelDisplayupdate;
        private System.Windows.Forms.Button buttonStorecfg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxCalTc1ThOffset;
        private System.Windows.Forms.TextBox textBoxCalTc1Gain;
        private System.Windows.Forms.Label labelCalTc1Tc0;
        private System.Windows.Forms.Label labelCalTc1TcGain;
        private System.Windows.Forms.Label labelCalTc1ThOffset;
        private System.Windows.Forms.TextBox textBoxCalTc1Zero;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelUid;
        private System.Windows.Forms.Label labelFwVerValue;
        private System.Windows.Forms.Label labelUidValue;
        private System.Windows.Forms.Label labelFwVer;
        private System.Windows.Forms.Label labelCalProfile;
        private System.Windows.Forms.ComboBox comboBoxCalProfile;
        private System.Windows.Forms.TextBox textBoxCalTc2Zero;
        private System.Windows.Forms.TextBox textBoxCalTc2Gain;
        private System.Windows.Forms.TextBox textBoxCalTc2ThOffset;
        private System.Windows.Forms.Label labelTc1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonChangeCalProfile;
        private System.Windows.Forms.Button buttonApplyCal;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Button buttonOpenPort;
        private System.Windows.Forms.Button buttonRefreshPorts;
        private System.Windows.Forms.Button buttonLog;
        private System.Windows.Forms.Button buttonWriteToFile;
        private System.Windows.Forms.CheckBox checkBoxTc1;
        private System.Windows.Forms.CheckBox checkBoxTc2;
        private System.Windows.Forms.Button buttonUpdateLut;
    }
}