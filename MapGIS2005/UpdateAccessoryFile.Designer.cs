namespace MapGIS2005
{
    partial class UpdateAccessoryFile
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
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnAccessorySave = new DevComponents.DotNetBar.ButtonX();
            this.txtbSavePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.sbSaveOrNo = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.btnLoadNewAccessoryFile = new DevComponents.DotNetBar.ButtonX();
            this.txtNewAccessoryFilePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(271, 163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(107, 163);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // btnAccessorySave
            // 
            this.btnAccessorySave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAccessorySave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAccessorySave.Location = new System.Drawing.Point(352, 114);
            this.btnAccessorySave.Name = "btnAccessorySave";
            this.btnAccessorySave.Size = new System.Drawing.Size(38, 23);
            this.btnAccessorySave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAccessorySave.TabIndex = 17;
            this.btnAccessorySave.Text = "...";
            this.btnAccessorySave.Click += new System.EventHandler(this.btnAccessorySave_Click);
            // 
            // txtbSavePath
            // 
            // 
            // 
            // 
            this.txtbSavePath.Border.Class = "TextBoxBorder";
            this.txtbSavePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtbSavePath.Location = new System.Drawing.Point(107, 114);
            this.txtbSavePath.Name = "txtbSavePath";
            this.txtbSavePath.Size = new System.Drawing.Size(239, 21);
            this.txtbSavePath.TabIndex = 16;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 114);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(96, 23);
            this.labelX3.TabIndex = 15;
            this.labelX3.Text = "保存附件文件到";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 67);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(96, 23);
            this.labelX2.TabIndex = 14;
            this.labelX2.Text = "保存原始文件";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // sbSaveOrNo
            // 
            // 
            // 
            // 
            this.sbSaveOrNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sbSaveOrNo.Location = new System.Drawing.Point(107, 66);
            this.sbSaveOrNo.Name = "sbSaveOrNo";
            this.sbSaveOrNo.Size = new System.Drawing.Size(66, 22);
            this.sbSaveOrNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sbSaveOrNo.TabIndex = 13;
            this.sbSaveOrNo.ValueChanged += new System.EventHandler(this.sbSaveOrNo_ValueChanged);
            // 
            // btnLoadNewAccessoryFile
            // 
            this.btnLoadNewAccessoryFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLoadNewAccessoryFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLoadNewAccessoryFile.Location = new System.Drawing.Point(352, 11);
            this.btnLoadNewAccessoryFile.Name = "btnLoadNewAccessoryFile";
            this.btnLoadNewAccessoryFile.Size = new System.Drawing.Size(38, 23);
            this.btnLoadNewAccessoryFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLoadNewAccessoryFile.TabIndex = 12;
            this.btnLoadNewAccessoryFile.Text = "...";
            this.btnLoadNewAccessoryFile.Click += new System.EventHandler(this.btnLoadNewAccessoryFile_Click);
            // 
            // txtNewAccessoryFilePath
            // 
            // 
            // 
            // 
            this.txtNewAccessoryFilePath.Border.Class = "TextBoxBorder";
            this.txtNewAccessoryFilePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewAccessoryFilePath.Location = new System.Drawing.Point(107, 12);
            this.txtNewAccessoryFilePath.Name = "txtNewAccessoryFilePath";
            this.txtNewAccessoryFilePath.Size = new System.Drawing.Size(239, 21);
            this.txtNewAccessoryFilePath.TabIndex = 11;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(96, 23);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "选择附件文件";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // UpdateAccessoryFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 199);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnAccessorySave);
            this.Controls.Add(this.txtbSavePath);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.sbSaveOrNo);
            this.Controls.Add(this.btnLoadNewAccessoryFile);
            this.Controls.Add(this.txtNewAccessoryFilePath);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "UpdateAccessoryFile";
            this.Text = "更新附件文件";
            this.Load += new System.EventHandler(this.UpdateAccessoryFile_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnAccessorySave;
        private DevComponents.DotNetBar.Controls.TextBoxX txtbSavePath;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.SwitchButton sbSaveOrNo;
        private DevComponents.DotNetBar.ButtonX btnLoadNewAccessoryFile;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewAccessoryFilePath;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}