namespace MapGIS2005
{
    partial class GeoUpdateDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeoUpdateDlg));
            this.bar4 = new DevComponents.DotNetBar.Bar();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenSorcePrj = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenNewPrj = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.btnGeoUpdate = new DevComponents.DotNetBar.ButtonItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axMapXView1 = new AxMapXView.AxMapXView();
            this.axMxWorkSpace1 = new AxWorkSpace.AxMxWorkSpace();
            this.axMapXView2 = new AxMapXView.AxMapXView();
            this.axMxWorkSpace2 = new AxWorkSpace.AxMxWorkSpace();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapXView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMxWorkSpace1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapXView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMxWorkSpace2)).BeginInit();
            this.SuspendLayout();
            // 
            // bar4
            // 
            this.bar4.AccessibleDescription = "DotNetBar Bar (bar4)";
            this.bar4.AccessibleName = "DotNetBar Bar";
            this.bar4.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.bar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar4.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2});
            this.bar4.Location = new System.Drawing.Point(0, 0);
            this.bar4.MenuBar = true;
            this.bar4.Name = "bar4";
            this.bar4.Size = new System.Drawing.Size(818, 26);
            this.bar4.Stretch = true;
            this.bar4.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.bar4.TabIndex = 3;
            this.bar4.TabStop = false;
            this.bar4.Text = "bar4";
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpenSorcePrj,
            this.btnOpenNewPrj});
            this.buttonItem1.Text = "工程";
            // 
            // btnOpenSorcePrj
            // 
            this.btnOpenSorcePrj.Name = "btnOpenSorcePrj";
            this.btnOpenSorcePrj.Text = "打开原始工程";
            this.btnOpenSorcePrj.Click += new System.EventHandler(this.btnOpenSorcePrj_Click);
            // 
            // btnOpenNewPrj
            // 
            this.btnOpenNewPrj.Name = "btnOpenNewPrj";
            this.btnOpenNewPrj.Text = "打开新工程";
            this.btnOpenNewPrj.Click += new System.EventHandler(this.btnOpenNewPrj_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnGeoUpdate});
            this.buttonItem2.Text = "更新";
            // 
            // btnGeoUpdate
            // 
            this.btnGeoUpdate.Name = "btnGeoUpdate";
            this.btnGeoUpdate.Text = "几何更新";
            this.btnGeoUpdate.Click += new System.EventHandler(this.btnGeoUpdate_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 26);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axMxWorkSpace1);
            this.splitContainer1.Panel1.Controls.Add(this.axMapXView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.axMxWorkSpace2);
            this.splitContainer1.Panel2.Controls.Add(this.axMapXView2);
            this.splitContainer1.Size = new System.Drawing.Size(818, 431);
            this.splitContainer1.SplitterDistance = 364;
            this.splitContainer1.TabIndex = 4;
            // 
            // axMapXView1
            // 
            this.axMapXView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapXView1.Enabled = true;
            this.axMapXView1.Location = new System.Drawing.Point(0, 0);
            this.axMapXView1.Name = "axMapXView1";
            this.axMapXView1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapXView1.OcxState")));
            this.axMapXView1.Size = new System.Drawing.Size(364, 431);
            this.axMapXView1.TabIndex = 0;
            // 
            // axMxWorkSpace1
            // 
            this.axMxWorkSpace1.Enabled = true;
            this.axMxWorkSpace1.Location = new System.Drawing.Point(129, 187);
            this.axMxWorkSpace1.Name = "axMxWorkSpace1";
            this.axMxWorkSpace1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMxWorkSpace1.OcxState")));
            this.axMxWorkSpace1.Size = new System.Drawing.Size(32, 32);
            this.axMxWorkSpace1.TabIndex = 1;
            // 
            // axMapXView2
            // 
            this.axMapXView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapXView2.Enabled = true;
            this.axMapXView2.Location = new System.Drawing.Point(0, 0);
            this.axMapXView2.Name = "axMapXView2";
            this.axMapXView2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapXView2.OcxState")));
            this.axMapXView2.Size = new System.Drawing.Size(450, 431);
            this.axMapXView2.TabIndex = 0;
            // 
            // axMxWorkSpace2
            // 
            this.axMxWorkSpace2.Enabled = true;
            this.axMxWorkSpace2.Location = new System.Drawing.Point(187, 200);
            this.axMxWorkSpace2.Name = "axMxWorkSpace2";
            this.axMxWorkSpace2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMxWorkSpace2.OcxState")));
            this.axMxWorkSpace2.Size = new System.Drawing.Size(32, 32);
            this.axMxWorkSpace2.TabIndex = 1;
            // 
            // GeoUpdateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 457);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bar4);
            this.Name = "GeoUpdateDlg";
            this.Text = "几何数据自动更新";
            this.Load += new System.EventHandler(this.GeoUpdateDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapXView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMxWorkSpace1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapXView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMxWorkSpace2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar4;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btnOpenSorcePrj;
        private DevComponents.DotNetBar.ButtonItem btnOpenNewPrj;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem btnGeoUpdate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AxWorkSpace.AxMxWorkSpace axMxWorkSpace1;
        private AxMapXView.AxMapXView axMapXView1;
        private AxWorkSpace.AxMxWorkSpace axMxWorkSpace2;
        private AxMapXView.AxMapXView axMapXView2;

    }
}