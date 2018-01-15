namespace MapGIS2005
{
    partial class UpdateMPJFile
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtNewMPJFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnLoadNewMPJFile = new DevComponents.DotNetBar.ButtonX();
            this.sbSaveOrNo = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnMPJSave = new DevComponents.DotNetBar.ButtonX();
            this.txtbSavePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 32);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(96, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "MPJ文件";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // txtNewMPJFilePath
            // 
            // 
            // 
            // 
            this.txtNewMPJFilePath.Border.Class = "TextBoxBorder";
            this.txtNewMPJFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewMPJFilePath.Location = new System.Drawing.Point(107, 32);
            this.txtNewMPJFilePath.Name = "txtNewMPJFilePath";
            this.txtNewMPJFilePath.Size = new System.Drawing.Size(239, 21);
            this.txtNewMPJFilePath.TabIndex = 1;
            // 
            // btnLoadNewMPJFile
            // 
            this.btnLoadNewMPJFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLoadNewMPJFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLoadNewMPJFile.Location = new System.Drawing.Point(352, 31);
            this.btnLoadNewMPJFile.Name = "btnLoadNewMPJFile";
            this.btnLoadNewMPJFile.Size = new System.Drawing.Size(38, 23);
            this.btnLoadNewMPJFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLoadNewMPJFile.TabIndex = 2;
            this.btnLoadNewMPJFile.Text = "...";
            this.btnLoadNewMPJFile.Click += new System.EventHandler(this.btnLoadNewMPJFile_Click);
            // 
            // sbSaveOrNo
            // 
            // 
            // 
            // 
            this.sbSaveOrNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sbSaveOrNo.Location = new System.Drawing.Point(107, 86);
            this.sbSaveOrNo.Name = "sbSaveOrNo";
            this.sbSaveOrNo.Size = new System.Drawing.Size(66, 22);
            this.sbSaveOrNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sbSaveOrNo.TabIndex = 3;
            this.sbSaveOrNo.ValueChanged += new System.EventHandler(this.sbSaveOrNo_ValueChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 87);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(96, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "保存原始文件";
            // 
            // btnMPJSave
            // 
            this.btnMPJSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMPJSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMPJSave.Location = new System.Drawing.Point(352, 133);
            this.btnMPJSave.Name = "btnMPJSave";
            this.btnMPJSave.Size = new System.Drawing.Size(38, 23);
            this.btnMPJSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMPJSave.TabIndex = 7;
            this.btnMPJSave.Text = "...";
            this.btnMPJSave.Click += new System.EventHandler(this.btnMPJSave_Click);
            // 
            // txtbSavePath
            // 
            // 
            // 
            // 
            this.txtbSavePath.Border.Class = "TextBoxBorder";
            this.txtbSavePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtbSavePath.Location = new System.Drawing.Point(107, 134);
            this.txtbSavePath.Name = "txtbSavePath";
            this.txtbSavePath.Size = new System.Drawing.Size(239, 21);
            this.txtbSavePath.TabIndex = 6;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 134);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(96, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "保存MPJ文件到";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(107, 183);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(271, 183);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UpdateMPJFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 228);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnMPJSave);
            this.Controls.Add(this.txtbSavePath);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.sbSaveOrNo);
            this.Controls.Add(this.btnLoadNewMPJFile);
            this.Controls.Add(this.txtNewMPJFilePath);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "UpdateMPJFile";
            this.Text = "更新矿区工程文件";
            this.Load += new System.EventHandler(this.UpdateMPJFile_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewMPJFilePath;
        private DevComponents.DotNetBar.ButtonX btnLoadNewMPJFile;
        private DevComponents.DotNetBar.Controls.SwitchButton sbSaveOrNo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtbSavePath;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnMPJSave;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}