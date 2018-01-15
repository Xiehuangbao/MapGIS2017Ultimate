namespace MapGIS2005
{
    partial class TopDialogNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopDialogNew));
            this.axMapXView1 = new AxMapXView.AxMapXView();
            this.axMxWorkSpace1 = new AxWorkSpace.AxMxWorkSpace();
            ((System.ComponentModel.ISupportInitialize)(this.axMapXView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMxWorkSpace1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMapXView1
            // 
            this.axMapXView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapXView1.Enabled = true;
            this.axMapXView1.Location = new System.Drawing.Point(0, 0);
            this.axMapXView1.Name = "axMapXView1";
            this.axMapXView1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapXView1.OcxState")));
            this.axMapXView1.Size = new System.Drawing.Size(284, 262);
            this.axMapXView1.TabIndex = 0;
            // 
            // axMxWorkSpace1
            // 
            this.axMxWorkSpace1.Enabled = true;
            this.axMxWorkSpace1.Location = new System.Drawing.Point(142, 86);
            this.axMxWorkSpace1.Name = "axMxWorkSpace1";
            this.axMxWorkSpace1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMxWorkSpace1.OcxState")));
            this.axMxWorkSpace1.Size = new System.Drawing.Size(32, 32);
            this.axMxWorkSpace1.TabIndex = 1;
            // 
            // TopDialogNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.axMxWorkSpace1);
            this.Controls.Add(this.axMapXView1);
            this.Name = "TopDialogNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "新参考图层";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TopDialogNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMapXView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMxWorkSpace1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public AxMapXView.AxMapXView axMapXView1;
        public AxWorkSpace.AxMxWorkSpace axMxWorkSpace1;
    }
}