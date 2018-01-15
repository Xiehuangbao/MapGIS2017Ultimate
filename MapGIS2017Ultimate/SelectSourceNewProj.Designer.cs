namespace MapGIS2017Ultimate
{
    partial class SelectSourceNewProj
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSource = new System.Windows.Forms.RadioButton();
            this.rbNew = new System.Windows.Forms.RadioButton();
            this.btnSelectType = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNew);
            this.groupBox1.Controls.Add(this.rbSource);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择工作空间";
            // 
            // rbSource
            // 
            this.rbSource.AutoSize = true;
            this.rbSource.Location = new System.Drawing.Point(6, 38);
            this.rbSource.Name = "rbSource";
            this.rbSource.Size = new System.Drawing.Size(71, 16);
            this.rbSource.TabIndex = 0;
            this.rbSource.TabStop = true;
            this.rbSource.Text = "原始工程";
            this.rbSource.UseVisualStyleBackColor = true;
            this.rbSource.CheckedChanged += new System.EventHandler(this.rbSource_CheckedChanged);
            // 
            // rbNew
            // 
            this.rbNew.AutoSize = true;
            this.rbNew.Location = new System.Drawing.Point(145, 38);
            this.rbNew.Name = "rbNew";
            this.rbNew.Size = new System.Drawing.Size(59, 16);
            this.rbNew.TabIndex = 1;
            this.rbNew.TabStop = true;
            this.rbNew.Text = "新工程";
            this.rbNew.UseVisualStyleBackColor = true;
            this.rbNew.CheckedChanged += new System.EventHandler(this.rbNew_CheckedChanged);
            // 
            // btnSelectType
            // 
            this.btnSelectType.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectType.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelectType.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSelectType.Location = new System.Drawing.Point(183, 107);
            this.btnSelectType.Name = "btnSelectType";
            this.btnSelectType.Size = new System.Drawing.Size(75, 23);
            this.btnSelectType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelectType.TabIndex = 1;
            this.btnSelectType.Text = "确定";
            this.btnSelectType.Click += new System.EventHandler(this.btnSelectType_Click);
            // 
            // SelectSourceNewProj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 130);
            this.Controls.Add(this.btnSelectType);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectSourceNewProj";
            this.Text = "选择编辑数据类型";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNew;
        private System.Windows.Forms.RadioButton rbSource;
        private DevComponents.DotNetBar.ButtonX btnSelectType;
    }
}