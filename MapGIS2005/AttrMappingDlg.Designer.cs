namespace MapGIS2005
{
    partial class AttrMappingDlg
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCheckRelation = new DevComponents.DotNetBar.ButtonX();
            this.btnUpdateAttr = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(3, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 340);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段匹配";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(395, 320);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridView1_ColumnWidthChanged);
            // 
            // btnCheckRelation
            // 
            this.btnCheckRelation.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCheckRelation.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCheckRelation.Location = new System.Drawing.Point(220, 360);
            this.btnCheckRelation.Name = "btnCheckRelation";
            this.btnCheckRelation.Size = new System.Drawing.Size(77, 25);
            this.btnCheckRelation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCheckRelation.TabIndex = 1;
            this.btnCheckRelation.Text = "关系校验";
            this.btnCheckRelation.Click += new System.EventHandler(this.btnCheckRelation_Click);
            // 
            // btnUpdateAttr
            // 
            this.btnUpdateAttr.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdateAttr.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdateAttr.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUpdateAttr.Location = new System.Drawing.Point(327, 360);
            this.btnUpdateAttr.Name = "btnUpdateAttr";
            this.btnUpdateAttr.Size = new System.Drawing.Size(77, 25);
            this.btnUpdateAttr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdateAttr.TabIndex = 2;
            this.btnUpdateAttr.Text = "更新";
            this.btnUpdateAttr.Click += new System.EventHandler(this.btnUpdateAttr_Click);
            // 
            // AttrMappingDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 386);
            this.Controls.Add(this.btnUpdateAttr);
            this.Controls.Add(this.btnCheckRelation);
            this.Controls.Add(this.groupBox1);
            this.Name = "AttrMappingDlg";
            this.Text = "属性匹配窗口";
            this.Load += new System.EventHandler(this.AttrMappingDlg_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnCheckRelation;
        private DevComponents.DotNetBar.ButtonX btnUpdateAttr;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}