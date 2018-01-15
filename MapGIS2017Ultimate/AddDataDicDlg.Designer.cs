namespace MapGIS2017Ultimate
{
    partial class AddDataDicDlg
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceAnno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newAnno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.updateToAccess = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAddAnno = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.sourceAnno,
            this.newAnno});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(355, 400);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Width = 30;
            // 
            // sourceAnno
            // 
            this.sourceAnno.DataPropertyName = "sourceAnno";
            this.sourceAnno.HeaderText = "原始标注";
            this.sourceAnno.Name = "sourceAnno";
            this.sourceAnno.Width = 150;
            // 
            // newAnno
            // 
            this.newAnno.DataPropertyName = "newAnno";
            this.newAnno.HeaderText = "新标注";
            this.newAnno.Name = "newAnno";
            this.newAnno.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(-1, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 428);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // updateToAccess
            // 
            this.updateToAccess.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.updateToAccess.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.updateToAccess.Location = new System.Drawing.Point(425, 453);
            this.updateToAccess.Name = "updateToAccess";
            this.updateToAccess.Size = new System.Drawing.Size(75, 23);
            this.updateToAccess.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.updateToAccess.TabIndex = 1;
            this.updateToAccess.Text = "更新字典";
            this.updateToAccess.Click += new System.EventHandler(this.updateToAccess_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAddAnno);
            this.groupBox2.Location = new System.Drawing.Point(372, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(128, 428);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // txtAddAnno
            // 
            // 
            // 
            // 
            this.txtAddAnno.Border.Class = "TextBoxBorder";
            this.txtAddAnno.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAddAnno.Location = new System.Drawing.Point(5, 18);
            this.txtAddAnno.Multiline = true;
            this.txtAddAnno.Name = "txtAddAnno";
            this.txtAddAnno.Size = new System.Drawing.Size(118, 402);
            this.txtAddAnno.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(-1, 453);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(361, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "<font color=\"#ED1C24\"><b>不同字典项之间以中文格式\'、\'隔开！</b><font color=\"#ED1C24\"></font></fon" +
                "t>";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // AddDataDicDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 479);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.updateToAccess);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddDataDicDlg";
            this.Text = "添加数据字典";
            this.Load += new System.EventHandler(this.AddDataDicDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX updateToAccess;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAddAnno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceAnno;
        private System.Windows.Forms.DataGridViewTextBoxColumn newAnno;
        private DevComponents.DotNetBar.LabelX labelX1;

    }
}