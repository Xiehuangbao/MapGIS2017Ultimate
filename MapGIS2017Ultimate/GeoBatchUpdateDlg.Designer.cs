namespace MapGIS2017Ultimate
{
    partial class GeoBatchUpdateDlg
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
            this.dgvONProj = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImportSProj = new DevComponents.DotNetBar.ButtonX();
            this.btnImportNProj = new DevComponents.DotNetBar.ButtonX();
            this.btnUpdate = new DevComponents.DotNetBar.ButtonX();
            this.btnProjMapping = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvONProj)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvONProj
            // 
            this.dgvONProj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvONProj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvONProj.Location = new System.Drawing.Point(3, 17);
            this.dgvONProj.Name = "dgvONProj";
            this.dgvONProj.RowTemplate.Height = 23;
            this.dgvONProj.Size = new System.Drawing.Size(428, 397);
            this.dgvONProj.TabIndex = 0;
            this.dgvONProj.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvONProj_Scroll);
            this.dgvONProj.CurrentCellChanged += new System.EventHandler(this.dgvONProj_CurrentCellChanged);
            this.dgvONProj.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvONProj_DataBindingComplete);
            this.dgvONProj.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvONProj_ColumnWidthChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvONProj);
            this.groupBox1.Location = new System.Drawing.Point(0, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 417);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新旧工程导入";
            // 
            // btnImportSProj
            // 
            this.btnImportSProj.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImportSProj.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImportSProj.Location = new System.Drawing.Point(90, 435);
            this.btnImportSProj.Name = "btnImportSProj";
            this.btnImportSProj.Size = new System.Drawing.Size(75, 23);
            this.btnImportSProj.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImportSProj.TabIndex = 2;
            this.btnImportSProj.Text = "导入源工程";
            this.btnImportSProj.Click += new System.EventHandler(this.btnImportSProj_Click);
            // 
            // btnImportNProj
            // 
            this.btnImportNProj.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImportNProj.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImportNProj.Location = new System.Drawing.Point(184, 435);
            this.btnImportNProj.Name = "btnImportNProj";
            this.btnImportNProj.Size = new System.Drawing.Size(75, 23);
            this.btnImportNProj.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImportNProj.TabIndex = 3;
            this.btnImportNProj.Text = "导入新工程";
            this.btnImportNProj.Click += new System.EventHandler(this.btnImportNProj_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUpdate.Location = new System.Drawing.Point(359, 435);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "批量更新";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnProjMapping
            // 
            this.btnProjMapping.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnProjMapping.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnProjMapping.Location = new System.Drawing.Point(271, 435);
            this.btnProjMapping.Name = "btnProjMapping";
            this.btnProjMapping.Size = new System.Drawing.Size(75, 23);
            this.btnProjMapping.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnProjMapping.TabIndex = 5;
            this.btnProjMapping.Text = "添加映射";
            this.btnProjMapping.Click += new System.EventHandler(this.btnProjMapping_Click);
            // 
            // GeoBatchUpdateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 459);
            this.Controls.Add(this.btnProjMapping);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnImportNProj);
            this.Controls.Add(this.btnImportSProj);
            this.Controls.Add(this.groupBox1);
            this.Name = "GeoBatchUpdateDlg";
            this.Text = "图形数据批量更新";
            ((System.ComponentModel.ISupportInitialize)(this.dgvONProj)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvONProj;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnImportSProj;
        private DevComponents.DotNetBar.ButtonX btnImportNProj;
        private DevComponents.DotNetBar.ButtonX btnUpdate;
        private DevComponents.DotNetBar.ButtonX btnProjMapping;
    }
}