namespace MapGIS2005
{
    partial class PathSet
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
            this.btnHisPathOK = new DevComponents.DotNetBar.ButtonX();
            this.txtHisPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnNewPathOK = new DevComponents.DotNetBar.ButtonX();
            this.txtNewPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnSourcePathOK = new DevComponents.DotNetBar.ButtonX();
            this.txtSourcePath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnHisPathOK
            // 
            this.btnHisPathOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHisPathOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHisPathOK.Location = new System.Drawing.Point(380, 137);
            this.btnHisPathOK.Name = "btnHisPathOK";
            this.btnHisPathOK.Size = new System.Drawing.Size(42, 23);
            this.btnHisPathOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnHisPathOK.TabIndex = 21;
            this.btnHisPathOK.Text = "...";
            this.btnHisPathOK.Click += new System.EventHandler(this.btnHisPathOK_Click);
            // 
            // txtHisPath
            // 
            // 
            // 
            // 
            this.txtHisPath.Border.Class = "TextBoxBorder";
            this.txtHisPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtHisPath.Location = new System.Drawing.Point(97, 139);
            this.txtHisPath.Name = "txtHisPath";
            this.txtHisPath.Size = new System.Drawing.Size(280, 21);
            this.txtHisPath.TabIndex = 20;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 139);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(79, 23);
            this.labelX3.TabIndex = 19;
            this.labelX3.Text = "历史数据路径";
            // 
            // btnNewPathOK
            // 
            this.btnNewPathOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNewPathOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNewPathOK.Location = new System.Drawing.Point(380, 85);
            this.btnNewPathOK.Name = "btnNewPathOK";
            this.btnNewPathOK.Size = new System.Drawing.Size(42, 23);
            this.btnNewPathOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNewPathOK.TabIndex = 18;
            this.btnNewPathOK.Text = "...";
            this.btnNewPathOK.Click += new System.EventHandler(this.btnNewPathOK_Click);
            // 
            // txtNewPath
            // 
            // 
            // 
            // 
            this.txtNewPath.Border.Class = "TextBoxBorder";
            this.txtNewPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewPath.Location = new System.Drawing.Point(97, 87);
            this.txtNewPath.Name = "txtNewPath";
            this.txtNewPath.Size = new System.Drawing.Size(280, 21);
            this.txtNewPath.TabIndex = 17;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 87);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(79, 23);
            this.labelX2.TabIndex = 16;
            this.labelX2.Text = "新路径";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(239, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(125, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSourcePathOK
            // 
            this.btnSourcePathOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSourcePathOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSourcePathOK.Location = new System.Drawing.Point(380, 29);
            this.btnSourcePathOK.Name = "btnSourcePathOK";
            this.btnSourcePathOK.Size = new System.Drawing.Size(42, 23);
            this.btnSourcePathOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSourcePathOK.TabIndex = 13;
            this.btnSourcePathOK.Text = "...";
            this.btnSourcePathOK.Click += new System.EventHandler(this.btnSourcePathOK_Click);
            // 
            // txtSourcePath
            // 
            // 
            // 
            // 
            this.txtSourcePath.Border.Class = "TextBoxBorder";
            this.txtSourcePath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSourcePath.Location = new System.Drawing.Point(97, 31);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(280, 21);
            this.txtSourcePath.TabIndex = 12;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 31);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(79, 23);
            this.labelX1.TabIndex = 11;
            this.labelX1.Text = "原始路径";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // PathSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 241);
            this.Controls.Add(this.btnHisPathOK);
            this.Controls.Add(this.txtHisPath);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.btnNewPathOK);
            this.Controls.Add(this.txtNewPath);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSourcePathOK);
            this.Controls.Add(this.txtSourcePath);
            this.Controls.Add(this.labelX1);
            this.MaximizeBox = false;
            this.Name = "PathSet";
            this.Text = "路径设置窗口";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnHisPathOK;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHisPath;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnNewPathOK;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewPath;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnSourcePathOK;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSourcePath;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}